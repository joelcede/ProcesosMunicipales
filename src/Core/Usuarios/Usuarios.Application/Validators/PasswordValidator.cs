using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Usuarios.Application.Validators
{
    public class PasswordValidator
    {
        public static ValidationResult ValidateLowerCase(string value, ValidationContext context)
        {
            if (!Regex.IsMatch(value, @"[a-z]"))
            {
                return new ValidationResult("La contraseña debe tener al menos una letra minúscula.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateUpperCase(string value, ValidationContext context)
        {
            if (!Regex.IsMatch(value, @"[A-Z]"))
            {
                return new ValidationResult("La contraseña debe tener al menos una letra mayúscula.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateDigit(string value, ValidationContext context)
        {
            if (!Regex.IsMatch(value, @"\d"))
            {
                return new ValidationResult("La contraseña debe tener al menos un dígito.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateSpecialChar(string value, ValidationContext context)
        {
            if (!Regex.IsMatch(value, @"[@$!%*#&()_+{}?&]"))
            {
                return new ValidationResult("La contraseña debe tener al menos un carácter especial.");
            }
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateLength(string value, ValidationContext context)
        {
            if (value.Length < 8)
            {
                return new ValidationResult("La contraseña debe tener al menos 8 caracteres de longitud.");
            }
            return ValidationResult.Success;
        }
    }
}
