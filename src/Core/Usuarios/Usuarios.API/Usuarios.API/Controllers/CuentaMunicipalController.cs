using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Dtos;
using Usuarios.Application.Repository;
using Usuarios.Domain.Entities;
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

        [HttpPost("AddCuentaMunicipal", Name = "AddCuentaMunicipal")]
        public async Task<CuentaMunicipal> AddCuentaMunicipal(CuentaMunicipalDto cuentaMunicipal)
        {
            cuentaMunicipal.EsPropietario = true;
            return await _cuentaMunicipalRepository.AddCuentaMunicipalAsync(cuentaMunicipal);
        }
        [HttpGet("GetCuentaMunicipal/{id}")]
        public async Task<CuentaMunicipal> GetCuentaMunicipal(Guid id)
        {
            return await _cuentaMunicipalRepository.GetCuentaByIdUsuarioAsync(id);
        }

        [HttpPut("UpdateCuentaMunicipal/{id}", Name = "UpdateCuentaMunicipal")]
        public async Task UpdateCuentaMunicipal(Guid id, CuentaMunicipal cuentaMunicipal)
        {
            await _cuentaMunicipalRepository.UpdateCuentaMunicipalAsync(id, cuentaMunicipal);
        }
        [HttpDelete("DeleteCuentaMunicipal/{id}", Name = "DeleteCuentaMunicipal")]
        public async Task DeleteCuentaMunicipal(Guid id)
        {
            await _cuentaMunicipalRepository.DeleteCuentaMunicipalAsync(id);
        }
    }
}
