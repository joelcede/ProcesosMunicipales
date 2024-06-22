using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Interfaces;

namespace Usuarios.Domain.Entities
{
    public class CuentaMunicipalDomain : ICuentaMunicipalDomain
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public string CuentaMunicipal { get; set; } = string.Empty;
        public string ContrasenaMunicipal { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
