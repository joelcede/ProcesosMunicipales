using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Dtos;
using Usuarios.Application.Repository;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;
using Usuarios.Infrastructure.Repository;

namespace Usuarios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class CuentaMunicipalController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICuentaMunicipalRepository _cuentaMunicipalRepository;
        public CuentaMunicipalController(IConfiguration configuration)
        {
            _configuration = configuration;
            _cuentaMunicipalRepository = new CuentaMunicipalRepository(_configuration);
        }
        #region Cuenta Municipal
        [HttpPost("AddCuentaMunicipal/{usuario}", Name = "AddCuentaMunicipal")]
        public async Task<CuentaMunicipalDomain> AddCuentaMunicipal([FromBody] CuentaMunicipalDto cuentaMunicipal, UsuarioType usuario)
        {
            return await _cuentaMunicipalRepository.AddCuentaMunicipalAsync(cuentaMunicipal, usuario);
        }
        [HttpGet("GetCuentaMunicipal/{id}/{usuario}", Name = "GetCuentaMunicipal")]
        public async Task<CuentaMunicipalDomain> GetCuentaMunicipal(Guid id, UsuarioType usuario)
        {
            return await _cuentaMunicipalRepository.GetCuentaByIdUsuarioAsync(id, usuario);
        }

        [HttpPut("UpdateCuentaMunicipal/{id}/{usuario}", Name = "UpdateCuentaMunicipal")]
        public async Task UpdateCuentaMunicipal(Guid id, CuentaMunicipalDomain cuentaMunicipal, UsuarioType usuario)
        {
            await _cuentaMunicipalRepository.UpdateCuentaMunicipalAsync(id, cuentaMunicipal, usuario);
        }
        [HttpDelete("DeleteCuentaMunicipal/{id}/{usuario}", Name = "DeleteCuentaMunicipal")]
        public async Task DeleteCuentaMunicipal(Guid id, UsuarioType usuario)
        {
            await _cuentaMunicipalRepository.DeleteCuentaMunicipalAsync(id, usuario);
        }
        #endregion
    }
}
