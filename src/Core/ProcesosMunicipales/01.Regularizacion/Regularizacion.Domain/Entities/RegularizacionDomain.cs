using Regularizacion.Domain.Enums;
using Regularizacion.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Domain.Entities
{
    public class RegularizacionDomain : IRegularizacionDomain
    {
        public Guid Id { get; set; }
        public Guid IdVivienda { get; set; }
        public string NumeroExpediente { get; set; }
        public decimal ValorRegularizacion { get; set; }
        public decimal Anticipo { get; set; }
        public decimal ValorPendiente { get; set; }
        public bool Pagado { get; set; }
        public EstadosType Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaInsercion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
    }
}
