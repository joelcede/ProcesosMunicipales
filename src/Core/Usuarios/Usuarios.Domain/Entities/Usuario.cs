using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Usuarios.Application.Repository;

namespace Usuarios.Domain.Entities
{
    public class Usuario : IUsuarioDomain
    {
        public Guid Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string TelefonoCelular { get; set; } = string.Empty;
        public string TelefonoConvencional { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
