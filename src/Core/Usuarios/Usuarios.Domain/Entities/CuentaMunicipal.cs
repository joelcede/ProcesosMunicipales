using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Interfaces;

namespace Usuarios.Domain.Entities
{
    public class CuentaMunicipal : ICuentaMunicipalDomain
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EsPropietario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
