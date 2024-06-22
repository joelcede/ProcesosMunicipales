using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.Dtos;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;

namespace Usuarios.Application.Repository
{
    public interface ICuentaMunicipalRepository
    {
        Task<CuentaMunicipalDomain> GetCuentaByIdUsuarioAsync(Guid id, UsuarioType usuario);
        Task<CuentaMunicipalDomain> AddCuentaMunicipalAsync(CuentaMunicipalDto cuentaMunicipal, UsuarioType usuario);
        Task UpdateCuentaMunicipalAsync(Guid identity, CuentaMunicipalDomain cuentaMunicipal, UsuarioType usuario);
        Task DeleteCuentaMunicipalAsync(Guid id, UsuarioType usuario);
    }
}
