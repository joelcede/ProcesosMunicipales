using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Application.Dtos
{
    public class PlantillaExcel
    {
        public int numRegularizacion { get; set; }
        public string NombreTramite { get; set; }
        public string EstadoRegularizacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string nombres { get; set; }
        public decimal ValorRegularizacion { get; set; }
        public decimal Anticipo { get; set; }
        public decimal ValorPendiente { get; set; }
        public string CodigoCatastral { get; set; }
    }
    public class PlantillaRecord
    {
        public List<CellExcel> cells { get; set; } = new List<CellExcel>();
        public record CellExcel(
            int numRegularizacion, 
            string NombreTramite, 
            string EstadoRegularizacion,
            string FechaRegistro,
            string nombres,
            decimal ValorRegularizacion,
            decimal Anticipo,
            decimal ValorPendiente,
            string CodigoCatastral);
    }
}
