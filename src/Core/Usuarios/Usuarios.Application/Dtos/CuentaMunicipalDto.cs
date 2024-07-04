using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Domain.Interfaces;
using Usuarios.Application.Validators;

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
        [RegularExpression("^[0-9]*$", ErrorMessage = "El Cuenta Municipal(DNI) solo debe contener números.")]
        public string CuentaMunicipal { get; set; } = string.Empty;
        [PasswordPropertyText]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#&()_+{}?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "La contraseña debe tener al menos una letra mayúscula, una letra minúscula, un dígito, un carácter especial y al menos 8 caracteres de longitud.")]
        //[CustomValidation(typeof(PasswordValidator), nameof(PasswordValidator.ValidateLowerCase))]
        //[CustomValidation(typeof(PasswordValidator), nameof(PasswordValidator.ValidateUpperCase))]
        //[CustomValidation(typeof(PasswordValidator), nameof(PasswordValidator.ValidateDigit))]
        //[CustomValidation(typeof(PasswordValidator), nameof(PasswordValidator.ValidateSpecialChar))]
        //[CustomValidation(typeof(PasswordValidator), nameof(PasswordValidator.ValidateLength))]

        public string ContrasenaMunicipal { get; set; } = string.Empty;
        
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set;}
    }
}
