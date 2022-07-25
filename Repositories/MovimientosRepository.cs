
using App.Dtos;
using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
    public class MovimientosRepository : IMovimientosRepository
    {
        private readonly AppDbContext _context;

        public MovimientosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientosAsync()
        {
            return await _context.Movimientos.ToListAsync();
        }

        public async Task<IEnumerable<ReporteDto>> GetReporteMovimientosAsync(string identificacion, DateTime fechainicio, DateTime fechafin)
        {
            var clienteExistente = await _context.Clientes.FirstOrDefaultAsync(cli => cli.Identificacion == identificacion);
            if (clienteExistente == null)
            {
                throw new Exception("Cliente no existe");
            }

            return _context.Movimientos.Where(mov => mov.Cuenta.ClienteId == clienteExistente.Id && mov.Fecha >= fechainicio && mov.Fecha <= fechafin)
                .Select(mov => new ReporteDto
                {
                    Fecha = mov.Fecha,
                    Cliente = mov.Cuenta.Cliente.Nombre,
                    NumeroCuenta = mov.Cuenta.NumeroCuenta,
                    TipoCuenta = mov.Cuenta.TipoCuenta,
                    Estado = mov.Estado,
                    SaldoInicial = mov.TipoMovimiento == "DEBITO" ? mov.Saldo + mov.Valor : mov.Saldo - mov.Valor,
                    Movimiento = mov.TipoMovimiento == "DEBITO" ? mov.Valor * -1 : mov.Valor,
                    SaldoDisponible = mov.Saldo
                });
        }

        public async Task<Movimiento> GetMovimientoAsync(int id)
        {
            return await _context.Movimientos.Where(movimiento => movimiento.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateMovimientoAsync(Movimiento movimiento)
        {
            if (movimiento.Fecha > DateTime.Now)
            {
                throw new Exception("La fecha debe ser menor a la fecha actual");
            }

            var cuentaExistente = await _context.Cuentas.Where(cuenta => cuenta.Id == movimiento.CuentaId).FirstOrDefaultAsync();
            if (cuentaExistente == null)
            {
                throw new Exception("Cuenta no existe");
            }

            var movimientoExistentes = await _context.Movimientos.Where(mov => mov.CuentaId == cuentaExistente.Id).OrderByDescending(m => m.Id).ToListAsync();
            var ultimoMovimiento = movimientoExistentes.FirstOrDefault();
            var saldoInicial = decimal.Zero;
            if (ultimoMovimiento == null)
            {
                saldoInicial = cuentaExistente.SaldoInicial;
            }
            else
            {
                if (movimiento.Fecha < ultimoMovimiento.Fecha)
                {
                    throw new Exception("La fecha debe ser mayor a la fecha del último movimiento");
                }
                saldoInicial = ultimoMovimiento.Saldo;
            }

            if (movimiento.TipoMovimiento == "DEBITO")
            {
                var valorRetiroDiario = movimientoExistentes.Where(mov => mov.Fecha.Date == DateTime.Now.Date && mov.TipoMovimiento == "DEBITO")
                                        .Sum(mov => mov.Valor);

                if (valorRetiroDiario + movimiento.Valor > 1000m)
                {
                    throw new Exception("Cupo diario Excedido");
                }
                if (saldoInicial - movimiento.Valor < decimal.Zero)
                {
                    throw new Exception("Saldo no disponible");
                }
                movimiento.Saldo = saldoInicial - movimiento.Valor;

            }
            else if (movimiento.TipoMovimiento == "CREDITO")
            {
                movimiento.Saldo = saldoInicial + movimiento.Valor;
            }
            else
            {
                throw new Exception("Operación no permitida");
            }

            //movimiento.Fecha = DateTime.Now;

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovimientoAsync(int id)
        {
            var movimiento = _context.Movimientos.FirstOrDefault(mov => mov.Id == id);
            if (movimiento == null)
            {
                throw new Exception("Movimiento No encontrado");
            }

            var ultimoMovimiento = await _context.Movimientos.Where(mov => mov.CuentaId == movimiento.CuentaId).OrderByDescending(m => m.Id).FirstOrDefaultAsync();
            if (ultimoMovimiento == null)
            {
                throw new Exception("Último Movimiento No encontrado");
            }

            if (ultimoMovimiento.Id != movimiento.Id)
            {
                throw new Exception("Solo se permite eliminar el último movimiento");
            }

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovimientoAsync(Movimiento movimiento)
        {
            throw new Exception("No se permite actualizar los movimientos, realice un reverso de la operación");
        }
    }
}