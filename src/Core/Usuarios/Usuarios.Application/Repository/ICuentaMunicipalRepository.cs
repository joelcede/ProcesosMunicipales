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
        Task<CuentaMunicipal> GetCuentaByIdUsuarioAsync(Guid id);
        Task<CuentaMunicipal> AddCuentaMunicipalAsync(CuentaMunicipalDto cuentaMunicipal);
        Task UpdateCuentaMunicipalAsync(Guid identity, CuentaMunicipal cuentaMunicipal);
        Task DeleteCuentaMunicipalAsync(Guid id);
    }
}
