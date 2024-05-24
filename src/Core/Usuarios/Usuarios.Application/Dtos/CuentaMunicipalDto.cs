using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Interfaces;

namespace Usuarios.Application.Dtos
{
    public class CuentaMunicipalDto : ICuentaMunicipalDomain
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [Key]
        [Required]
        public Guid IdUsuario { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "El formato del Email no es válido.")]
        public string CorreoElectronico { get; set; } = string.Empty;
        [Required]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "La contraseña debe tener al menos una letra mayúscula, una letra minúscula, un dígito, un carácter especial y al menos 8 caracteres de longitud.")]
        public string Password { get; set; } = string.Empty;
        [Required]
        public bool EsPropietario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set;}
    }
}
