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
    public class ViviendaPropietarioController : ControllerBase
    {
        private readonly IViviendaTIRepository _viviendaRepository;
        private readonly IConfiguration _configuration;
        private readonly IntermediatTableType tabla = IntermediatTableType.ViviendaPropietario;
        private readonly IntermediatTableType tablaS = IntermediatTableType.S_ViviendaPropietario;

        public ViviendaPropietarioController(IConfiguration configuration)
        {
            _configuration = configuration;
            _viviendaRepository = new ViviendaITRespository(_configuration);
        }
        [HttpPost]
        public async Task<ViviendaUsuarios> CreateVivienda(ViviendaUsuarioDto vivienda)
        {
            return await _viviendaRepository.AddViviendaUsuarioAsync(vivienda, tabla);

        }
        [HttpGet("${id}")]
        public async Task<IEnumerable<ViviendaFamProp>> GetVivienda(Guid id)
        {
            return await _viviendaRepository.GetViviendaUsuarioByIdAsync(id, tablaS);
        }
    }
}
