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
        private readonly ICuentaMunicipalRepository _cuentaMunicipalRepository;

        public FamiliaresController(IConfiguration configuration)
        {
            _configuration = configuration;
            _familiarRepository = new UsuarioRepository(_configuration);
            _cuentaMunicipalRepository = new CuentaMunicipalRepository(_configuration);
        }

        [HttpGet("GetFamiliares", Name = "GetFamiliares")]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await _familiarRepository.GetAllUsuariosAsync(_userType);
        }

        [HttpGet("GetFamiliar/{id}", Name = "GetFamiliar")]
        public async Task<Usuario> Get(Guid id)
        {
            return await _familiarRepository.GetUsuarioByIdAsync(id, _userType);
        }

        [HttpPost("AddFamiliar", Name = "AddFamiliar")]
        public async Task AddFamiliar(UsuarioRequestDto usuario)
        {
            await _familiarRepository.AddUsuarioAsync(usuario, _userType);
        }

        [HttpPut("UpdateFamiliar/${id}", Name = "UpdateFamiliar")]
        public async Task UpdateFamiliar(Guid id, Usuario usuario)
        {
            await _familiarRepository.UpdateUsuarioAsync(id, usuario, _userType);
        }

        [HttpDelete("DeleteFamiliar/{id}", Name = "DeleteFamiliar")]
        public async Task DeleteFamiliar(Guid id)
        {
            await _familiarRepository.DeleteUsuarioAsync(id, _userType);
        }

        [HttpPost("AddCMFamiliar", Name = "AddCuentaMunicipalFamiliar")]
        public async Task AddCuentaMunicipalFamiliar(CuentaMunicipalDto cuentaMunicipal)
        {
            cuentaMunicipal.EsPropietario = false;
            await _cuentaMunicipalRepository.AddCuentaMunicipalAsync(cuentaMunicipal);
        }
        [HttpGet("GetCMFamiliar/${id}", Name = "GetCuentaMunicipalFamiliar")]
        public async Task<CuentaMunicipal> GetCuentaMunicipalFamiliar(Guid id)
        {
            return await _cuentaMunicipalRepository.GetCuentaByIdUsuarioAsync(id);
        }

        [HttpPut("UpdateCMFamiliar/${id}", Name ="ActualizarCuenta")]
        public async Task UpdateCuentaMunicipalFamiliar(Guid id, CuentaMunicipal cuentaMunicipal)
        {
            await _cuentaMunicipalRepository.UpdateCuentaMunicipalAsync(id, cuentaMunicipal);
        }
        [HttpDelete("DeleteCMFamiliar/${id}", Name = "DeleteCuentaMunicipalFamiliar")]
        public async Task DeleteCuentaMunicipalFamiliar(Guid id)
        {
            await _cuentaMunicipalRepository.DeleteCuentaMunicipalAsync(id);
        }
    }
}
