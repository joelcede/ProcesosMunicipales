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
    public class RegularizacionController : ControllerBase
    {
        private readonly IRegularizacionRepository _RegularizacionRepository;
        private readonly IConfiguration _configuration;

        public RegularizacionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _RegularizacionRepository = new RegularizacionRepository(_configuration);
        }

        [HttpGet]
        public async Task<IActionResult> GetRegularizaciones()
        {
            var Regularizacions = await _RegularizacionRepository.GetRegularizacionesAsync();
            return Ok(Regularizacions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegularizacion(Guid id)
        {
            var Regularizacion = await _RegularizacionRepository.GetRegularizacionByIdAsync(id);
            if (Regularizacion == null)
            {
                return NotFound();
            }
            return Ok(Regularizacion);
        }

        [HttpPost]
        public async Task<RegularizacionDomain> CreateRegularizacion([FromBody] RegularizacionDto regularizacion)
        {
            return await _RegularizacionRepository.AddRegularizacionAsync(regularizacion);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegularizacion(Guid id, RegularizacionDto regularizacion)
        {
            await _RegularizacionRepository.UpdateRegularizacionAsync(regularizacion, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegularizacion(Guid id)
        {
            await _RegularizacionRepository.DeleteRegularizacionAsync(id);
            return Ok();
        }
    }
}
