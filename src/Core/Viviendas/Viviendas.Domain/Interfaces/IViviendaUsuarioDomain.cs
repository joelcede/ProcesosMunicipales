using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viviendas.Domain.Interfaces
{
    public interface IViviendaUsuarioDomain
    {
        Guid Id { get; }
        Guid IdUsuario { get; }
        Guid IdVivienda { get; }
        Guid IdImagen { get; }
    }
}
