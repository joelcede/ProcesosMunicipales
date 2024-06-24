using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Domain.Entities
{
    public class ContratoDefaultDomain
    {
        public string nombrePropietario { get; set; }
        public string dni {  get; set; }
        public string direcionVivienda { get; set; }
        public decimal valorReg {  get; set; }
        public decimal valorMitadReg { get; set; }
        public byte[] imagenV {  get; set; }


    }
}
