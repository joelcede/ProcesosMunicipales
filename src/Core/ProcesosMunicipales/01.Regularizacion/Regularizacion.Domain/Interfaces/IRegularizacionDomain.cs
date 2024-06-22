using Regularizacion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Domain.Interfaces
{
    public interface IRegularizacionDomain
    {
        Guid Id { get;  }
        Guid IdVivienda { get; }
        string NumeroExpediente { get;  }
        decimal ValorRegularizacion { get; }
        decimal Anticipo { get; }
        decimal ValorPendiente { get; }
        bool Pagado { get; }
        EstadosType Estado { get; }
        DateTime FechaRegistro { get; }
        DateTime FechaInsercion { get; }
        DateTime FechaActualizacion { get; }
        string Correo { get; }
        string Contrasena { get; }
        int numRegularizacion { get; }
    }
}
