using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Repository;
using Usuarios.Domain.Entities;

namespace Usuarios.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamiliaresController : ControllerBase
    {

        private readonly IFamiliarRepository _familiarRepository;

        public FamiliaresController(IFamiliarRepository familiarRepository)
        {
            _familiarRepository = familiarRepository;
        }

        [HttpGet(Name = "GetFamiliares")]
        public async Task<IEnumerable<Familiar>> Get()
        {
            return await _familiarRepository.GetAllFamiliarsAsync();
        }

        [HttpGet("{id}", Name = "GetFamiliar")]
        public async Task<Familiar> Get(int id)
        {
            return await _familiarRepository.GetFamiliarByIdAsync(id);
        }

        [HttpPost(Name = "AddFamiliar")]
        public async Task AddFamiliar(Familiar Familiar)
        {
            await _familiarRepository.AddFamiliarAsync(Familiar);
        }

        [HttpPut("${id}", Name = "UpdateFamiliar")]
        public async Task UpdateFamiliar(int id, Familiar Familiar)
        {
            await _familiarRepository.UpdateFamiliarAsync(Familiar);
        }

        [HttpDelete("{id}", Name = "DeleteFamiliar")]
        public async Task DeleteFamiliar(int id)
        {
            await _familiarRepository.DeleteFamiliarAsync(id);
        }
    }
}
