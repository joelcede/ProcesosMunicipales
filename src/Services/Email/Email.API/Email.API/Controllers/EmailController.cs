using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Email.Application.Repository;

namespace Email.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IPendientesRepository _pendientesRepository;

        public EmailController(IPendientesRepository pendientesRepository)
        {
            _pendientesRepository = pendientesRepository;
        }

        [HttpPost("Procesos", Name = "procesosDataRegularizacion")]
        public async Task<IActionResult> procesosDataRegularizacion()
        {
            await _pendientesRepository.CambiarEstadoRegularizacion();

            return Ok("Se proceso correctamente");
        }
    }
}
