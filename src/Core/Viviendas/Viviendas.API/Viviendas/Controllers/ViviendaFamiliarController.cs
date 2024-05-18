using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Viviendas.Application.Dtos;
using Viviendas.Application.Repository;
using Viviendas.Domain.Entities;
using Viviendas.Domain.Enums;
using Viviendas.Infrastructure.Repository;

namespace Viviendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViviendaFamiliarController : ControllerBase
    {
        private readonly IViviendaUsuarioRepository _viviendaRepository;
        private readonly IConfiguration _configuration;

        public ViviendaFamiliarController(IConfiguration configuration)
        {
            _configuration = configuration;
            _viviendaRepository = new ViviendaUsuarioRepository(_configuration);
        }
        [HttpPost]
        public async Task<ViviendaUsuarios> CreateVivienda(ViviendaUsuarioDto vivienda)
        {
            return await _viviendaRepository.AddViviendaUsuarioAsync(vivienda, UsuarioType.Familiar);

        }
    }
}
