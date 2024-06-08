using Microsoft.AspNetCore.Cors;
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
    [EnableCors]
    public class PropietariosController : ControllerBase
    {
        private readonly IUsuarioRepository _propietarioRepository;
        private readonly IConfiguration _configuration;
        private readonly UsuarioType _userType = UsuarioType.Propietario;
        private readonly ICuentaMunicipalRepository _cuentaMunicipalRepository;

        public PropietariosController(IConfiguration configuration)
        {
            _configuration = configuration;
            _propietarioRepository = new UsuarioRepository(_configuration);
            _cuentaMunicipalRepository = new CuentaMunicipalRepository(_configuration);
        }
        [HttpGet("GetAllPropietarios",Name = "GetPropietarios")]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await _propietarioRepository.GetAllUsuariosAsync(_userType);
        }

        [HttpGet("GetPropietario/{id}", Name = "GetPropietario")]
        public async Task<Usuario> Get(Guid id)
        {
            return await _propietarioRepository.GetUsuarioByIdAsync(id, _userType);
        }

        [HttpPost("AddPropietario", Name = "AddPropietario")]
        public async Task<Usuario> AddPropietario(UsuarioRequestDto usuario)
        {
            return await _propietarioRepository.AddUsuarioAsync(usuario, _userType);
        }

        [HttpPut("UpdatePropietario/{id}", Name = "UpdatePropietario")]
        public async Task UpdatePropietario(Guid id, Usuario usuario)
        {
            await _propietarioRepository.UpdateUsuarioAsync(id, usuario, _userType);
        }

        [HttpDelete("DeletePropietario/{id}", Name = "DeletePropietario")]
        public async Task DeletePropietario(Guid id)
        {
            await _propietarioRepository.DeleteUsuarioAsync(id, _userType);
        }
        [HttpPost("AddCMPropietario", Name = "AddCuentaMunicipalPropietario")]
        public async Task AddCuentaMunicipalPropietario(CuentaMunicipalDto cuentaMunicipal)
        {
            cuentaMunicipal.EsPropietario = true;
            await _cuentaMunicipalRepository.AddCuentaMunicipalAsync(cuentaMunicipal);
        }
        [HttpGet("GetCMPropietario/{id}")]
        public async Task<CuentaMunicipal> GetCuentaMunicipalPropietario(Guid id)
        {
            return await _cuentaMunicipalRepository.GetCuentaByIdUsuarioAsync(id);
        }

        [HttpPut("UpdateCMPropietario/{id}", Name = "UpdateCuentaMunicipalPropietario")]
        public async Task UpdateCuentaMunicipalPropietario(Guid id, CuentaMunicipal cuentaMunicipal)
        {
            await _cuentaMunicipalRepository.UpdateCuentaMunicipalAsync(id, cuentaMunicipal);
        }
        [HttpDelete("DeleteCMPropietario/{id}", Name = "DeleteCuentaMunicipalPropietario")]
        public async Task DeleteCuentaMunicipalPropietario(Guid id)
        {
            await _cuentaMunicipalRepository.DeleteCuentaMunicipalAsync(id);
        }
    }
}
