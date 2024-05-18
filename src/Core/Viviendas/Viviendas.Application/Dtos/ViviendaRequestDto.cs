using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Viviendas.Domain.Enums;
using Viviendas.Domain.Interfaces;

namespace Viviendas.Application.Dtos
{
    public class ViviendaRequestDto : IViviendaDomain
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "La Direccion es requerida")]
        public string Direccion { get; set; } = string.Empty;

        [StringLength(25, MinimumLength = 15, ErrorMessage = "El Codigo Catastral debe de estar en el rango de 15 a 25 caracteres")]
        [Required(ErrorMessage = "El Codigo Catastral es requerido")]
        public string CodigoCatastral { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;
        public string Coordenadas { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Imagen es requerida")]
        public byte[] Imagen { get; set; } = Array.Empty<byte>();

        public CiudadType Ciudad { get; set; } = CiudadType.Guayaquil;
        public ProvinciaType Provincia { get; set; } = ProvinciaType.Guayas;
        public PaisType Pais { get; set; } = PaisType.Ecuador;

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
