using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Domain.Enums
{
    public enum EstadosType
    {
        None = 0,
        PorHacer = 1,
        EnEspera = 2,
        SubSanacion = 3,
        Negada = 4,
        EnEsperaVuelaASubir = 5,
        Aprobada = 6,
        TerminadaPendiente = 7,
        Terminada = 8,
    }
}
