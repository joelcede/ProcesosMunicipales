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
    public class ViviendaITController : ControllerBase
    {
        private readonly IViviendaTIRepository _viviendaImagenRepository;
        private readonly IConfiguration _configuration;
        private readonly IntermediatTableType tablaS = IntermediatTableType.S_iviendaUsuarios;

        public ViviendaITController(IConfiguration configuration)
        {
            _configuration = configuration;
            _viviendaImagenRepository = new ViviendaITRespository(_configuration);
        }

        [HttpGet("GetViviendaUsuarios/{id}", Name = "GetViviendaUsuario")]
        public async Task<IEnumerable<ViviendaFamProp>> GetViviendaUsuario(Guid id)
        {
            return await _viviendaImagenRepository.GetViviendaPropFamByIdAsync(id, tablaS);
        }
    }
}
