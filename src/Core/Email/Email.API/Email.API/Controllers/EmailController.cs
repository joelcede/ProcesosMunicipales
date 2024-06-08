using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Email.Application.Repository;
using Service.Email.Infrastructure.Repository;

namespace Email.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IPendientesRepository _pendientesRepository;
        private readonly IConfiguration _configuration;


        public EmailController(IConfiguration configuration)
        {
            _configuration = configuration;
            _pendientesRepository = new PendientesRepository(_configuration);
        }

        [HttpPost("Procesos", Name = "procesosDataRegularizacion")]
        public async Task<IActionResult> procesosDataRegularizacion()
        {
            await _pendientesRepository.CambiarEstadoRegularizacion();

            return Ok("Se proceso correctamente");
        }
    }
}
