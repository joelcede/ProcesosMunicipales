using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viviendas.Domain.Interfaces;

namespace Viviendas.Application.Dtos
{
    public class ViviendaUsuarioDto : IViviendaUsuarioDomain
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Key]
        [Required(ErrorMessage = "El Id del Usuario Es requerido")]
        public Guid IdUsuario { get; set; }
        [Key]
        [Required(ErrorMessage = "El Id de la vivienda es requerido")]
        public Guid IdVivienda { get; set; }
    }
}
