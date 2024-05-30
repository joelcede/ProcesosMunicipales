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
    public class ViviendaImagenController : ControllerBase
    {
        private readonly IViviendaTIRepository _viviendaImagenRepository;
        private readonly IConfiguration _configuration;
        private readonly IntermediatTableType tabla = IntermediatTableType.ViviendaImagen;
        private readonly IntermediatTableType tablaS = IntermediatTableType.S_ViviendaImagen;

        public ViviendaImagenController(IConfiguration configuration)
        {
            _configuration = configuration;
            _viviendaImagenRepository = new ViviendaITRespository(_configuration);
        }


        [HttpPost("AddViviendaImagen", Name = "CreateViviendaImagen")]
        public async Task<ViviendaImagenes> CreateViviendaImagen(ViviendaImagenDto viviendaImagen)
        {
            return await _viviendaImagenRepository.AddViviendaImagenAsync(viviendaImagen, tabla);
            
        }
        [HttpGet("GetViviendaImagen/{id}", Name = "GetViviendaImagen")]
        public async Task<ViviendaImagenes> GetViviendaImagen(Guid id)
        {
            return await _viviendaImagenRepository.GetViviendaImagenByIdAsync(id, tablaS);
        }
    }
}
