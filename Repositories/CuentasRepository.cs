
using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
    public class CuentasRepository : ICuentasRepository
    {
        private readonly AppDbContext _context;

        public CuentasRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cuenta>> GetCuentasAsync()
        {
            return await _context.Cuentas.ToListAsync();
        }

        public async Task<Cuenta> GetCuentaAsync(int id)
        {
            return await _context.Cuentas.Where(cuenta => cuenta.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateCuentaAsync(Cuenta cuenta)
        {
            var clienteExistente = await _context.Clientes.Where(cliente => cliente.Id == cuenta.ClienteId).FirstOrDefaultAsync();
            if (clienteExistente == null)
            {
                throw new Exception("Cliente no existe");
            }

            var cuentaExistente = await _context.Cuentas.Where(cta => cta.NumeroCuenta == cuenta.NumeroCuenta).FirstOrDefaultAsync();
            if (cuentaExistente != null)
            {
                throw new Exception("Numero de Cuenta ya existe");
            }

            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCuentaAsync(int id)
        {
            var cuenta = _context.Cuentas.FirstOrDefault(c => c.Id == id);
            if (cuenta == null)
            {
                throw new Exception("Cuenta No encontrada");
            }
            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCuentaAsync(Cuenta cuenta)
        {
            _context.Entry(cuenta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}