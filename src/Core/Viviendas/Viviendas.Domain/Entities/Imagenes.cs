using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viviendas.Domain.Entities
{
    public class Imagenes
    {
        public Guid Id { get; set; }
        public byte[] Imagen1 { get; set; } = Array.Empty<byte>();
        public byte[] Imagen2 { get; set; } = Array.Empty<byte>();
        public byte[] Imagen3 { get; set; } = Array.Empty<byte>();
        public byte[] Imagen4 { get; set; } = Array.Empty<byte>();
        public byte[] Imagen5 { get; set; } = Array.Empty<byte>();
        public byte[] Imagen6 { get; set; } = Array.Empty<byte>();
        public byte[] Imagen7 { get; set; } = Array.Empty<byte>();
        public byte[] Imagen8 { get; set; } = Array.Empty<byte>();
        public byte[] Imagen9 { get; set; } = Array.Empty<byte>();
        public byte[] Imagen10 { get; set; } = Array.Empty<byte>();
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }   
}
