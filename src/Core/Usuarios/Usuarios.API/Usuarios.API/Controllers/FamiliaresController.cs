using Microsoft.AspNetCore.Http;
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
    public class FamiliaresController : ControllerBase
    {
        private readonly IUsuarioRepository _familiarRepository;
        private readonly IConfiguration _configuration;
        private readonly UsuarioType _userType = UsuarioType.Familiar;

        public FamiliaresController(IConfiguration configuration)
        {
            _configuration = configuration;
            _familiarRepository = new UsuarioRepository(_configuration);
        }

        [HttpGet(Name = "GetFamiliares")]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await _familiarRepository.GetAllUsuariosAsync(_userType);
        }

        [HttpGet("{id}", Name = "GetFamiliar")]
        public async Task<Usuario> Get(Guid id)
        {
            return await _familiarRepository.GetUsuarioByIdAsync(id, _userType);
        }

        [HttpPost(Name = "AddFamiliar")]
        public async Task AddFamiliar(UsuarioRequestDto usuario)
        {
            await _familiarRepository.AddUsuarioAsync(usuario, _userType);
        }

        [HttpPut("${id}", Name = "UpdateFamiliar")]
        public async Task UpdateFamiliar(Guid id, Usuario usuario)
        {
            await _familiarRepository.UpdateUsuarioAsync(id, usuario, _userType);
        }

        [HttpDelete("{id}", Name = "DeleteFamiliar")]
        public async Task DeleteFamiliar(Guid id)
        {
            await _familiarRepository.DeleteUsuarioAsync(id, _userType);
        }
    }
}
