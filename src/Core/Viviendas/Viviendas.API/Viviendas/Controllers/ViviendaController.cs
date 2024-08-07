﻿using AutoMapper;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors]
    public class ViviendaController : ControllerBase
    {
        private readonly IViviendaRepository _viviendaRepository;
        private readonly IViviendaTIRepository _viviendaITRepository;
        private readonly IConfiguration _configuration;

        public ViviendaController(IConfiguration configuration)
        {
            _configuration = configuration;
            _viviendaRepository = new ViviendaRepository(_configuration);
            _viviendaITRepository = new ViviendaITRespository(_configuration);
        }

        [HttpGet("GetAllViviendas", Name = "GetViviendas")]
        public async Task<IActionResult> GetViviendas()
        {
            var viviendas = await _viviendaRepository.GetAllViviendasAsync();
            return Ok(viviendas);
        }

        [HttpGet("GetVivienda/{id}", Name = "GetVivienda")]
        public async Task<IActionResult> GetVivienda(Guid id)
        {
            var vivienda = await _viviendaRepository.GetViviendaByIdAsync(id);
    
            return Ok(vivienda);
        }

        [HttpPost("AddVivienda", Name = "CreateVivienda")]
        public async Task<Vivienda> CreateVivienda([FromBody] ViviendaRequestDto vivienda)
        {
            return await _viviendaRepository.AddViviendaAsync(vivienda);
            
        }

        [HttpPut("UpdateVivienda/{id}", Name = "UpdateVivienda")]
        public async Task<IActionResult> UpdateVivienda(Guid id, Vivienda vivienda)
        {
            await _viviendaRepository.UpdateViviendaAsync(id, vivienda);
            return Ok();
        } 

        [HttpDelete("DeleteVivienda/{id}", Name = "DeleteVivienda")]
        public async Task<IActionResult> DeleteVivienda(Guid id)
        {
            await _viviendaRepository.DeleteViviendaAsync(id);
            return Ok();
        }
        [HttpDelete("DeleteViviendaIT/{id}", Name = "DeleteViviendaIT")]
        public async Task<IActionResult> DeleteViviendaIT(Guid id)
        {
            await _viviendaITRepository.DeleteViviendaIT(id);
            return Ok();
        }
    }
}
