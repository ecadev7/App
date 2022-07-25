using App.Dtos;
using App.Entities;

namespace App.Repositories
{
    public interface IMovimientosRepository
    {
        Task<Movimiento> GetMovimientoAsync(int id);
        Task<IEnumerable<Movimiento>> GetMovimientosAsync();
        Task<IEnumerable<ReporteDto>> GetReporteMovimientosAsync(string identificacion, DateTime fechainicio, DateTime fechafin);
        Task CreateMovimientoAsync(Movimiento item);
        Task UpdateMovimientoAsync(Movimiento item);
        Task DeleteMovimientoAsync(int id);
    }

}