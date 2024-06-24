using Regularizacion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Application.Repository
{
    public interface IGraphicsRepository
    {
        Task<IEnumerable<CantidadRegMesDomain>> obtenerGraficaRegMes();
        Task<IEnumerable<EstadosMesDomain>> obtenerGananciaRegMes();
    }
}
