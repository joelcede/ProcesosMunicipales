using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Enums;

namespace Usuarios.Domain.Interfaces
{
    public interface IUsuarioDomain
    {
        Guid Id { get; }
        string Nombres { get; }
        string Apellidos { get; }
        string DNI { get; }
        string TelefonoCelular { get; }
        string TelefonoConvencional { get; }
        bool esPrincipal { get; }
        DateTime FechaCreacion { get; }
        DateTime FechaModificacion { get; }
    }
}
