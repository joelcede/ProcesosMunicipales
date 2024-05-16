using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Repository;
using Usuarios.Domain.Entities;

namespace Usuarios.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropietariosController : ControllerBase
    {
        private readonly IPropietarioRepository _propietarioRepository;

        public PropietariosController(IPropietarioRepository propietarioRepository)
        {
            _propietarioRepository = propietarioRepository;
        }
        [HttpGet(Name = "GetPropietarios")]
        public async Task<IEnumerable<Propietario>> Get()
        {
            return await _propietarioRepository.GetAllPropietariosAsync();
        }

        [HttpGet("{id}", Name = "GetPropietario")]
        public async Task<Propietario> Get(int id)
        {
            return await _propietarioRepository.GetPropietarioByIdAsync(id);
        }

        [HttpPost(Name = "AddPropietario")]
        public async Task AddPropietario(Propietario Propietario)
        {
            await _propietarioRepository.AddPropietarioAsync(Propietario);
        }

        [HttpPut("${id}", Name = "UpdatePropietario")]
        public async Task UpdatePropietario(int id, Propietario Propietario)
        {
            await _propietarioRepository.UpdatePropietarioAsync(Propietario);
        }

        [HttpDelete("{id}", Name = "DeletePropietario")]
        public async Task DeletePropietario(int id)
        {
            await _propietarioRepository.DeletePropietarioAsync(id);
        }
    }
}
