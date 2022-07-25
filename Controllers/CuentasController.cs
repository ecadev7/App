using App.Dtos;
using App.Entities;
using App.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentasRepository repository;

        public CuentasController(ICuentasRepository repository)
        {
            this.repository = repository;
        }

        // Get /cuentas
        [HttpGet]
        public async Task<IEnumerable<CuentaDto>> GetCuentasAsync()
        {
            var cuentas = (await repository.GetCuentasAsync())
                            .Select(cuenta => cuenta.AsDto());
            return cuentas;
        }

        // GET /cuentas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaDto>> GetCuentaAsync(int id)
        {
            var item = await repository.GetCuentaAsync(id);
            if (item is null)
                return NotFound();

            return item.AsDto();
        }

        // POST /cuentas
        [HttpPost]
        public async Task<ActionResult<CuentaDto>> CreateCuenta(CreateCuentaDto cuentaDto)
        {
            Cuenta cuenta = new Cuenta()
            {
                NumeroCuenta = cuentaDto.NumeroCuenta,
                SaldoInicial = cuentaDto.SaldoInicial,
                TipoCuenta = cuentaDto.TipoCuenta,
                Estado = cuentaDto.Estado,
                ClienteId = cuentaDto.ClienteId
            };
            await repository.CreateCuentaAsync(cuenta);

            return CreatedAtAction(nameof(GetCuentaAsync), new { id = cuenta.NumeroCuenta }, cuenta.AsDto());
        }

        // PUT /cuentas/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCuenta(int id, UpdateCuentaDto cuentaDto)
        {
            if (id != cuentaDto.Id)
            {
                return BadRequest();
            }
            
            var cuentaExistente = await repository.GetCuentaAsync(id);
            if (cuentaExistente is null)
            {
                return NotFound();
            }

            cuentaExistente.TipoCuenta = cuentaDto.TipoCuenta;
            cuentaExistente.Estado = cuentaDto.Estado;

            await repository.UpdateCuentaAsync(cuentaExistente);
            return NoContent();
        }

        // DELETE /cuentas/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var cuentaExistente = await repository.GetCuentaAsync(id);
            if (cuentaExistente is null)
            {
                return NotFound();
            }

            await repository.DeleteCuentaAsync(id);
            return NoContent();
        }
    }
}