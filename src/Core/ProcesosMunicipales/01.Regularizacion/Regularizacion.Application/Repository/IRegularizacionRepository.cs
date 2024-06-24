using Regularizacion.Application.Dtos;
using Regularizacion.Domain.Entities;
using Regularizacion.Domain.Enums;
using Regularizacion.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Application.Repository
{
    public interface IRegularizacionRepository
    {
        Dictionary<string, object> CreateR(IRegularizacionDomain regularizacion, CrudType operacion);
        Task<RegularizacionDomain> AddRegularizacionAsync(RegularizacionDto regularizacion);
        Dictionary<string, object> DeleteR(IRegularizacionDomain regularizacion, CrudType operacion);
        Task DeleteRegularizacionAsync(Guid id);
        Dictionary<string, object> GetR(Guid id, CrudType operacion);
        Task<RegularizacionDomain> GetRegularizacionByIdAsync(Guid id);
        Dictionary<string, object> GetRs(CrudType operacion = CrudType.None);
        Task<IEnumerable<TarjetaRegularizacionDomain>> GetRegularizacionesAsync();
        Dictionary<string, object> UpdateR(IRegularizacionDomain regularizacion, CrudType operacion);
        Task UpdateRegularizacionAsync(RegularizacionDto regularizacion, Guid id);
        Task<NumRegDomain> ObtenerSecuenciaRegularizacion();
        Task<byte[]> GetContratoDefaultAsync(Guid idReg);
    }
}
