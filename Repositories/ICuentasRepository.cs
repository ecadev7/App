using App.Entities;

namespace App.Repositories
{
    public interface ICuentasRepository
    {
        Task<Cuenta> GetCuentaAsync(int id);
        Task<IEnumerable<Cuenta>> GetCuentasAsync();
        Task CreateCuentaAsync(Cuenta item);
        Task UpdateCuentaAsync(Cuenta item);
        Task DeleteCuentaAsync(int id);
    }

}