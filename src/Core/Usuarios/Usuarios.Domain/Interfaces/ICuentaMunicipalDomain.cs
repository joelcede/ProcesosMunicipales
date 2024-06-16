using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Domain.Interfaces
{
    public interface ICuentaMunicipalDomain
    {
        Guid Id { get; }
        Guid IdUsuario { get; }
        string cuentaMunicipal { get; }
        string contrasenaMunicipal { get; }
        bool EsPropietario { get; }
        DateTime FechaCreacion { get; }
        DateTime FechaModificacion { get; }
    }
}
