using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viviendas.Application.Dtos;
using Viviendas.Domain.Entities;
using Viviendas.Domain.Enums;
using Viviendas.Domain.Interfaces;

namespace Viviendas.Application.Repository
{
    public interface IViviendaTIRepository
    {
        Dictionary<string, object> keyValuePairsImagen(IViviendaImagenDomain vivienda, IntermediatTableType tabla = IntermediatTableType.None);
        Dictionary<string, object> keyValuePairsUsuario(IViviendaUsuarioDomain vivienda, IntermediatTableType tabla = IntermediatTableType.None);
        Task<ViviendaImagenes> AddViviendaImagenAsync(ViviendaImagenDto vivienda, IntermediatTableType tabla);
        Task<ViviendaImagenes> GetViviendaImagenByIdAsync(Guid id, IntermediatTableType tabla);
        Task<ViviendaUsuarios> AddViviendaUsuarioAsync(ViviendaUsuarioDto vivienda, IntermediatTableType tabla);
        Task<IEnumerable<ViviendaFamProp>> GetViviendaUsuarioByIdAsync(Guid id, IntermediatTableType tabla);
        Task<IEnumerable<ViviendaFamProp>> GetViviendaPropFamByIdAsync(Guid id, IntermediatTableType tabla);
        Task DeleteViviendaIT(Guid idVivienda);
    }
}
