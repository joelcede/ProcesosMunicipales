using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Viviendas.Application.Dtos;
using Viviendas.Application.Repository;
using Viviendas.Domain.Entities;
using Viviendas.Infrastructure.Repository;

namespace Viviendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViviendaImagenController : ControllerBase
    {
        private readonly IViviendaImagenRepository _viviendaImagenRepository;
        private readonly IConfiguration _configuration;

        public ViviendaImagenController(IConfiguration configuration)
        {
            _configuration = configuration;
            _viviendaImagenRepository = new ViviendaImagenRepository(_configuration);
        }


        [HttpPost]
        public async Task<ViviendaImagenes> CreateViviendaImagen(ViviendaImagenDto viviendaImagen)
        {
            return await _viviendaImagenRepository.AddViviendaImagenAsync(viviendaImagen);
            
        }

    }
}
