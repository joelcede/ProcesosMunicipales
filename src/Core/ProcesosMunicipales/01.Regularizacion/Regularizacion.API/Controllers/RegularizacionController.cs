using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Regularizacion.Application.Dtos;
using Regularizacion.Application.Repository;
using Regularizacion.Domain.Entities;
using Regularizacion.Infrastructure.Repository;

namespace Regularizacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class RegularizacionController : ControllerBase
    {
        private readonly IRegularizacionRepository _RegularizacionRepository;
        private readonly IConfiguration _configuration;

        public RegularizacionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _RegularizacionRepository = new RegularizacionRepository(_configuration);
        }

        [HttpGet("GetAllRegularizaciones", Name = "GetRegularizaciones")]
        public async Task<IActionResult> GetRegularizaciones()
        {
            var Regularizacions = await _RegularizacionRepository.GetRegularizacionesAsync();
            return Ok(Regularizacions);
        }

        [HttpGet("GetRegularizacion/{id}", Name = "GetRegularizacion")]
        public async Task<IActionResult> GetRegularizacion(Guid id)
        {
            var Regularizacion = await _RegularizacionRepository.GetRegularizacionByIdAsync(id);
            if (Regularizacion == null)
            {
                return NotFound();
            }
            return Ok(Regularizacion);
        }

        [HttpPost("AddRegularizacion", Name = "CreateRegularizacion")]
        public async Task<RegularizacionDomain> CreateRegularizacion([FromBody] RegularizacionDto regularizacion)
        {
            return await _RegularizacionRepository.AddRegularizacionAsync(regularizacion);

        }

        [HttpPut("UpdateRegularizacion/{id}", Name = "UpdateRegularizacion")]
        public async Task<IActionResult> UpdateRegularizacion(Guid id, RegularizacionDto regularizacion)
        {
            await _RegularizacionRepository.UpdateRegularizacionAsync(regularizacion, id);
            return Ok();
        }

        [HttpDelete("DeleteRegularizacion/{id}", Name = "DeleteRegularizacion")]
        public async Task<IActionResult> DeleteRegularizacion(Guid id)
        {
            await _RegularizacionRepository.DeleteRegularizacionAsync(id);
            return Ok();
        }
        [HttpGet("GetSecuenciaReg", Name = "GetSecuenciaReg")]
        public async Task<NumRegDomain> GetSecuenciaReg()
        {
            return await _RegularizacionRepository.ObtenerSecuenciaRegularizacion();
        }
        [HttpGet("GetContrato/{idRegularizacion}", Name ="GetContrato")]
        public async Task<byte[]> GetContrato(Guid idRegularizacion)
        {
            return await _RegularizacionRepository.GetContratoDefaultAsync(idRegularizacion);
        }
    }
}
