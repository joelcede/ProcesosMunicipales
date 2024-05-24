using Regularizacion.Application.Dtos;
using Regularizacion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Application.Interfaces
{
    public interface IRegularizacionMapper
    {
        RegularizacionDomain CreateRegularizacion (RegularizacionDto regularizacion);
        RegularizacionDto CreateRegularizacionDto(RegularizacionDomain regularizacion);
    }
}
 