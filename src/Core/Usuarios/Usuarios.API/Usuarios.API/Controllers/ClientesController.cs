using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Dtos;
using Usuarios.Application.Repository;
using Usuarios.Domain.Entities;
using Usuarios.Infrastructure.Repository;

namespace Clientes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IConfiguration _configuration;

        public ClientesController(IClienteRepository clienteRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _clienteRepository = new ClienteRepository(_configuration);
        }

        [HttpGet(Name = "GetClientes")]
        public async Task<IEnumerable<Cliente>> Get()
        {
            return await _clienteRepository.GetAllClientesAsync();
        }

        [HttpGet("{id}", Name = "GetCliente")]
        public async Task<Cliente> Get(Guid id)
        {
            return await _clienteRepository.GetClienteByIdAsync(id);
        }

        [HttpPost(Name = "AddCliente")]
        public async Task AddCliente(ClienteRequestDto cliente)
        {
            await _clienteRepository.AddClienteAsync(cliente);
        }

        [HttpPut("${id}", Name = "UpdateCliente")]
        public async Task UpdateCliente(Guid id, Cliente cliente)
        {
            await _clienteRepository.UpdateClienteAsync(id, cliente);
        }

        [HttpDelete("{id}", Name = "DeleteCliente")]
        public async Task DeleteCliente(Guid id)
        {
            await _clienteRepository.DeleteClienteAsync(id);
        }
    }
}
