using Regularizacion.Domain.Enums;
using Regularizacion.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Regularizacion.Application.Dtos
{
    public class RegularizacionDto : IRegularizacionDomain
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid IdVivienda { get; set; }
        public string NumeroExpediente { get; set; } = string.Empty;
        [Required]
        public decimal ValorRegularizacion { get; set; }
        [Required]
        public decimal Anticipo { get; set; }
        [JsonIgnore]
        public decimal ValorPendiente { get => ValorPendiente; private set => CalcularValorPendiente(); }
        [JsonIgnore]
        public bool Pagado { get; set; } = false;
        public EstadosType Estado { get; set; } = EstadosType.PorHacer;
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaInsercion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        private void CalcularValorPendiente()
        {
            ValorPendiente = ValorRegularizacion - Anticipo;
        }
    }
}
