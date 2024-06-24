using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Domain.Entities
{
    public class CantidadRegMesDomain
    {
        public string Mes { get; set; } = string.Empty;
        public int CantidadRegularizaciones { get; set; }
        public decimal TotalGanado { get; set; }
        public decimal TotalPendiente { get; set; }
        public decimal Total { get; set;}
    }
    public class EstadosMesDomain
    {
        public string Tipo { get; set; }
        public int Cantidad { get; set; }
    }
}
