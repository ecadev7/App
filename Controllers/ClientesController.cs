using App.Dtos;
using App.Entities;
using App.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesRepository repository;

        public ClientesController(IClientesRepository repository)
        {
            this.repository = repository;
        }

        // Get /clientes
        [HttpGet]
        public async Task<IEnumerable<ClienteDto>> GetClientesAsync()
        {
            var clientes = (await repository.GetClientesAsync())
                            .Select(Cliente => Cliente.AsDto());
            return clientes;
        }

        // GET /clientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetClienteAsync(int id)
        {
            var cliente = await repository.GetClienteAsync(id);
            if (cliente is null)
                return NotFound();

            return cliente.AsDto();
        }

        // POST /clientes
        [HttpPost]
        public async Task<ActionResult<ClienteDto>> CreateItem(ClienteDto clienteDto){
            Cliente Cliente = new Cliente(){
                Nombre = clienteDto.Nombre,
                Genero = clienteDto.Genero,
                Edad = clienteDto.Edad,
                Identificacion = clienteDto.Identificacion,
                Direccion = clienteDto.Direccion,
                Telefono = clienteDto.Telefono,
                Contrasenia = clienteDto.Contrasenia,
                Estado = clienteDto.Estado,
            };
            await repository.CreateClienteAsync(Cliente);

            return CreatedAtAction(nameof(GetClienteAsync), new { id = Cliente.Id }, Cliente.AsDto());
        }

        // PUT /clientes/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(int id, UpdateClienteDto clienteDto)
        {
            if (id != clienteDto.Id)
            {
                return BadRequest();
            }
            
            var cliente = await repository.GetClienteAsync(id);
            if (cliente is null){
                return NotFound();
            }

            cliente.Nombre = clienteDto.Nombre;
            cliente.Genero = clienteDto.Genero;
            cliente.Edad = clienteDto.Edad;
            cliente.Direccion = clienteDto.Direccion;
            cliente.Telefono = clienteDto.Telefono;
            cliente.Contrasenia = clienteDto.Contrasenia;
            cliente.Estado = clienteDto.Estado;

            await repository.UpdateClienteAsync(cliente);
            return NoContent();
        }

        // DELETE /clientes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id){
            var clienteExistente = await repository.GetClienteAsync(id);
            if (clienteExistente is null){
                return NotFound();
            }

            await repository.DeleteClienteAsync(id);
            return NoContent();
        }
    }
}