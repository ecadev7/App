using App.Entities;

namespace App.Repositories
{
    public interface IClientesRepository
    {
        Task<Cliente> GetClienteAsync(int id);
        Task<IEnumerable<Cliente>> GetClientesAsync();
        Task CreateClienteAsync(Cliente item);
        Task UpdateClienteAsync(Cliente item);
        Task DeleteClienteAsync(int id);
    }

}