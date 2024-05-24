using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.Dtos;
using Usuarios.Domain.Entities;

namespace Usuarios.Application.Interfaces
{
    public interface ICuentaMunicipalMapper
    {
        CuentaMunicipal CreateCuentaMunicipal(CuentaMunicipalDto cuentaMunicipalDto);
        CuentaMunicipalDto CreateCuentaMunicipalDto(CuentaMunicipal cuentaMunicipal);
    }
}
