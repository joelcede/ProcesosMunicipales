using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Entities;

namespace Usuarios.Application.Repository
{
    public interface IPropietarioRepository
    {
        Task<Propietario> GetPropietarioByIdAsync(int id);
        Task<IEnumerable<Propietario>> GetAllPropietariosAsync();
        Task AddPropietarioAsync(Propietario propietario);
        Task UpdatePropietarioAsync(Propietario propietario);
        Task DeletePropietarioAsync(int id);
    }
}
