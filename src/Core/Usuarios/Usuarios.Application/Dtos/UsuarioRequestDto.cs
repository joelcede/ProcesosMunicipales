using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Usuarios.Domain.Enums;
using Usuarios.Domain.Interfaces;

namespace Usuarios.Application.Dtos
{
    public class UsuarioRequestDto : IUsuarioDomain
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50, MinimumLength =3 , ErrorMessage ="Los nombre debe de estar en el rango de 3 a 50 caracteres")]
        [Required(ErrorMessage = "Los nombres son requeridos")]
        public string Nombres { get; set; } = string.Empty;

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Los apellidos debe de estar en el rango de 3 a 50 caracteres")]
        [Required(ErrorMessage = "Los apellidos son requeridos")]
        public string Apellidos { get; set; } = string.Empty;

        [MaxLength(13, ErrorMessage ="El DNI debe tener maximo 13 caracteres")]
        [Required(ErrorMessage = "El DNI es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El DNI solo debe contener números.")]
        public string DNI { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono celular es requerido")]
        [StringLength(10, MinimumLength =10, ErrorMessage ="El telefono celular debe de tener 10 digitos")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El DNI solo debe contener números.")]
        public string TelefonoCelular { get; set; } = string.Empty;
        public BoolType esPrincipal { get; set; } = BoolType.None;

        [StringLength(15, MinimumLength = 10, ErrorMessage = "El telefono convencional debe estar en un rango de 10 a 15 digitos")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El DNI solo debe contener números.")]
        public string TelefonoConvencional { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
