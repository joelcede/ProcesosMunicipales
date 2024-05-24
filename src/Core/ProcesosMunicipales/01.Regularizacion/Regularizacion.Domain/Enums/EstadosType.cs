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
        Aprobada    = 4,
        TerminadaPendiente = 5,
        Terminada = 6,
        Negada = 7,
    }
}
