using Regularizacion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Domain.Entities
{
    public class TarjetaRegularizacionDomain
    {
        public Guid IdRegularizacion { get; set; }
        public Guid IdVivienda { get; set; }
        public string NombreTramite { get; set; }
        public string EstadoRegularizacion { get; set; }
        public decimal ValorRegularizacion { get; set; }
        public byte[] ImagenPrincipal { get; set; }
        public string NombrePropietario { get; set; }
        public string Celular { get; set; }
        public string CodigoCatastral { get; set; }
    }
}
