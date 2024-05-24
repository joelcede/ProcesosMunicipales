using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Viviendas.Application.Dtos;
using Viviendas.Application.Mapper;
using Viviendas.Application.Repository;
using Viviendas.Domain.Entities;
using Viviendas.Infrastructure.Repository;

namespace Viviendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViviendaController : ControllerBase
    {
        private readonly IViviendaRepository _viviendaRepository;
        private readonly IConfiguration _configuration;

        public ViviendaController(IConfiguration configuration)
        {
            _configuration = configuration;
            _viviendaRepository = new ViviendaRepository(_configuration);
        }

        [HttpGet]
        public async Task<IActionResult> GetViviendas()
        {
            var viviendas = await _viviendaRepository.GetAllViviendasAsync();
            return Ok(viviendas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVivienda(Guid id)
        {
            var vivienda = await _viviendaRepository.GetViviendaByIdAsync(id);
            if (vivienda == null)
            {
                return NotFound();
            }
            return Ok(vivienda);
        }

        [HttpPost]
        public async Task<Vivienda> CreateVivienda([FromBody] ViviendaRequestDto vivienda)
        {
            return await _viviendaRepository.AddViviendaAsync(vivienda);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVivienda(Guid id, Vivienda vivienda)
        {
            await _viviendaRepository.UpdateViviendaAsync(id, vivienda);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVivienda(Guid id)
        {
            await _viviendaRepository.DeleteViviendaAsync(id);
            return Ok();
        }
    }
}
