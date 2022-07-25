using App.Dtos;
using App.Entities;
using App.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientosRepository repository;

        public MovimientosController(IMovimientosRepository repository)
        {
            this.repository = repository;
        }

        // Get /movimientos
        [HttpGet]
        public async Task<IEnumerable<MovimientoDto>> GetMovimientosAsync()
        {
            var movimientos = (await repository.GetMovimientosAsync())
                            .Select(movimiento => movimiento.AsDto());
            return movimientos;
        }

        // GET /movimientos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoDto>> GetMovimientoAsync(int id)
        {
            var movimiento = await repository.GetMovimientoAsync(id);
            if (movimiento is null)
                return NotFound();

            return movimiento.AsDto();
        }

        [HttpGet("reportes")]
        public async Task<IEnumerable<ReporteDto>> GetReporte(string identificacion, DateTime fechainicio, DateTime fechafin)
        {
            var movimientos = await repository.GetReporteMovimientosAsync(identificacion, fechainicio, fechafin);
            return movimientos;
        }

        // POST /movimientos
        [HttpPost]
        public async Task<ActionResult<MovimientoDto>> CreateMovimiento(CreateMovimientoDto movimientoDto)
        {
            Movimiento movimiento = new Movimiento()
            {
                TipoMovimiento = movimientoDto.TipoMovimiento,
                Valor = movimientoDto.Valor,
                CuentaId = movimientoDto.CuentaId,
                Fecha = movimientoDto.Fecha == null ? DateTime.Now : movimientoDto.Fecha
            };
            await repository.CreateMovimientoAsync(movimiento);

            return CreatedAtAction(nameof(GetMovimientoAsync), new { id = movimiento.Id }, movimiento.AsDto());
        }

        // PUT /movimientos/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovimiento(int id, UpdateMovimientoDto movimientoDto)
        {
            if (id != movimientoDto.Id)
            {
                return BadRequest();
            }

            var movimientoExistente = await repository.GetMovimientoAsync(id);
            if (movimientoExistente is null)
            {
                return NotFound();
            }

            movimientoExistente.Valor = movimientoDto.Valor;
            movimientoExistente.Estado = movimientoDto.Estado;
            movimientoExistente.TipoMovimiento = movimientoDto.TipoMovimiento;

            await repository.UpdateMovimientoAsync(movimientoExistente);
            return NoContent();
        }

        // DELETE /movimientos/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var existingItem = await repository.GetMovimientoAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            await repository.DeleteMovimientoAsync(id);
            return NoContent();
        }
    }
}