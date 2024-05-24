using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viviendas.Domain.Entities
{
    public class ViviendaFamProp
    {
        public Guid IdVivienda { get; set; }
        public Guid IdUsuario { get; set; }
        public string TelefonoCelular { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public bool esPropietario { get; set; }
        public bool PropietarioPrincipal { get; set; }

    }
}
