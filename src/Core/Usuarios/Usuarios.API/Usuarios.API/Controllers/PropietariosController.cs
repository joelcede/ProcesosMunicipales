using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Usuarios.Application.Dtos;
using Usuarios.Application.Repository;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;
using Usuarios.Infrastructure.Repository;

namespace Usuarios.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropietariosController : ControllerBase
    {
        private readonly IUsuarioRepository _propietarioRepository;
        private readonly IConfiguration _configuration;
        private readonly UsuarioType _userType = UsuarioType.Propietario;


        public PropietariosController(IConfiguration configuration)
        {
            _configuration = configuration;
            _propietarioRepository = new UsuarioRepository(_configuration);
        }
        [HttpGet(Name = "GetPropietarios")]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await _propietarioRepository.GetAllUsuariosAsync(_userType);
        }

        [HttpGet("{id}", Name = "GetPropietario")]
        public async Task<Usuario> Get(Guid id)
        {
            return await _propietarioRepository.GetUsuarioByIdAsync(id, _userType);
        }

        [HttpPost(Name = "AddPropietario")]
        public async Task AddPropietario(UsuarioRequestDto usuario)
        {
            await _propietarioRepository.AddUsuarioAsync(usuario, _userType);
        }

        [HttpPut("${id}", Name = "UpdatePropietario")]
        public async Task UpdatePropietario(Guid id, Usuario usuario)
        {
            await _propietarioRepository.UpdateUsuarioAsync(id, usuario, _userType);
        }

        [HttpDelete("{id}", Name = "DeletePropietario")]
        public async Task DeletePropietario(Guid id)
        {
            await _propietarioRepository.DeleteUsuarioAsync(id, _userType);
        }
    }
}
