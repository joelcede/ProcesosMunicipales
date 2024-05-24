 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viviendas.Domain.Interfaces;

namespace Viviendas.Domain.Entities
{
    public class ViviendaUsuarios : IViviendaUsuarioDomain
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdVivienda { get; set; }
        public Guid IdImagen { get; set; }
    }
}
