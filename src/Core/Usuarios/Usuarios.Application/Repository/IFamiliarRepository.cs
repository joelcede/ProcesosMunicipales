using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Entities;

namespace Usuarios.Application.Repository
{
    public interface IFamiliarRepository
    {
        Task<Familiar> GetFamiliarByIdAsync(int id);
        Task<IEnumerable<Familiar>> GetAllFamiliarsAsync();
        Task AddFamiliarAsync(Familiar familiar);
        Task UpdateFamiliarAsync(Familiar familiar);
        Task DeleteFamiliarAsync(int id);
    }
}
