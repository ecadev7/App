using App.Dtos;
using App.Entities;

namespace App
{
    public static class Extensions
    {
        public static CuentaDto AsDto(this Cuenta cuenta)
        {
            return new CuentaDto
            {
                Id = cuenta.Id,
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoInicial = cuenta.SaldoInicial,
                TipoCuenta = cuenta.TipoCuenta,
                Estado = cuenta.Estado,
                ClienteId = cuenta.ClienteId
            };
        }

        public static ClienteDto AsDto(this Cliente cliente)
        {
            return new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Genero = cliente.Genero,
                Edad = cliente.Edad,
                Identificacion = cliente.Identificacion,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono,
                Contrasenia = cliente.Contrasenia,
                Estado = cliente.Estado,
            };
        }

        public static MovimientoDto AsDto(this Movimiento movimiento)
        {
            return new MovimientoDto
            {
                Id = movimiento.Id,
                Fecha = movimiento.Fecha,
                TipoMovimiento = movimiento.TipoMovimiento,
                Valor = movimiento.Valor,
                Saldo = movimiento.Saldo,
                CuentaId = movimiento.CuentaId,
            };
        }

    }
}