using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Dtos;
using Usuarios.Application.Repository;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;
using Usuarios.Infrastructure.Repository;

namespace Clientes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IUsuarioRepository _clienteRepository;
        private readonly IConfiguration _configuration;
        private readonly UsuarioType _userType = UsuarioType.Cliente;
        public ClientesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _clienteRepository = new UsuarioRepository(_configuration);
        }

        [HttpGet(Name = "GetClientes")]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await _clienteRepository.GetAllUsuariosAsync(_userType);
        }

        [HttpGet("{id}", Name = "GetCliente")]
        public async Task<Usuario> Get(Guid id)
        {
            return await _clienteRepository.GetUsuarioByIdAsync(id, _userType);
        }

        [HttpPost(Name = "AddCliente")]
        public async Task<Usuario> AddCliente(UsuarioRequestDto cliente)
        {
            return await _clienteRepository.AddUsuarioAsync(cliente, _userType);
        }

        [HttpPut("${id}", Name = "UpdateCliente")]
        public async Task UpdateCliente(Guid id, Usuario cliente)
        {
            await _clienteRepository.UpdateUsuarioAsync(id, cliente, _userType);
        }

        [HttpDelete("{id}", Name = "DeleteCliente")]
        public async Task DeleteCliente(Guid id)
        {
            await _clienteRepository.DeleteUsuarioAsync(id, _userType);
        }
    }
}
