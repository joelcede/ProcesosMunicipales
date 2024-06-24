using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Regularizacion.Application.Repository;
using Regularizacion.Infrastructure.Repository;

namespace Regularizacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class GraficosController : ControllerBase
    {
        private readonly IGraphicsRepository _GraphicsRepository;
        private readonly IConfiguration _configuration;

        public GraficosController(IConfiguration configuration)
        {
            _configuration = configuration;
            _GraphicsRepository = new GraphicsRepository(_configuration);
        }
        [HttpGet("GetGraficaCantMes", Name = "GetGraficaCantMes")]
        public async Task<IActionResult> GetGraficaCantMes()
        {
            var regularizacion = await _GraphicsRepository.obtenerGraficaRegMes();
            return Ok(regularizacion);
        }
        [HttpGet("GetGananciaRegMes", Name = "GetGananciaRegMes")]
        public async Task<IActionResult> GetGananciaRegMes()
        {
            var regularizacion = await _GraphicsRepository.obtenerGananciaRegMes();
            return Ok(regularizacion);
        }
    }
}
