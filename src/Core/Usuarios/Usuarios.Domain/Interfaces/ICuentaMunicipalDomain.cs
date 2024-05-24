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
        string CorreoElectronico { get; }
        string Password { get; }
        bool EsPropietario { get; }
        DateTime FechaCreacion { get; }
        DateTime FechaModificacion { get; }
    }
}
