using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viviendas.Application.Dtos;
using Viviendas.Domain.Entities;
using Viviendas.Domain.Enums;

namespace Viviendas.Application.Repository
{
    public interface IViviendaUsuarioRepository
    {
        Task<ViviendaUsuarios> AddViviendaUsuarioAsync(ViviendaUsuarioDto vivienda, UsuarioType operacion);
    }
}
