
using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly AppDbContext _context;

        public ClientesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
             return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            return await _context.Clientes.Where(Cliente => Cliente.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateClienteAsync(Cliente cliente)
        {
            var clienteExistente = await _context.Clientes.Where(cli => cli.Identificacion == cliente.Identificacion).FirstOrDefaultAsync();
            if (clienteExistente != null)
            {
                throw new Exception("Cliente ya existe");
            }
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClienteAsync(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                throw new Exception("Cliente No encontrado");
            }
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}