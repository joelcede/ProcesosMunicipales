using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viviendas.Application.Dtos;
using Viviendas.Domain.Entities;

namespace Viviendas.Application.Repository
{
    public interface IViviendaRepository
    {
        Task<Vivienda> GetViviendaByIdAsync(Guid id);
        Task<IEnumerable<Vivienda>> GetAllViviendasAsync();
        Task<Vivienda> AddViviendaAsync(ViviendaRequestDto vivienda);
        Task UpdateViviendaAsync(Guid identity, Vivienda vivienda);
        Task DeleteViviendaAsync(Guid id);
    }
}
