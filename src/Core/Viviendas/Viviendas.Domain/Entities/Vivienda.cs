using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viviendas.Domain.Enums;
using Viviendas.Domain.Interfaces;

namespace Viviendas.Domain.Entities
{
    public class Vivienda :IViviendaDomain
    {
        public Guid Id { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string CodigoCatastral { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Coordenadas { get; set; } = string.Empty;
        public byte[] Imagen { get; set; } = Array.Empty<byte>();
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
