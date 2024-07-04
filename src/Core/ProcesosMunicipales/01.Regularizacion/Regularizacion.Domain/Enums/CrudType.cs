using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Domain.Enums
{
    public enum CrudType
    {
        None = 0,
        ListAll = 1,
        GetById = 2,
        Create = 3,
        Update = 4,
        Delete = 5,
        GetCountAll = 6,
        GetAprobadas = 7,
        GetPendientes = 8,
        GetNegadas = 9,
        GetCorreosErroneos = 10,
        GetFichaExcel = 11,
        GetDataContrato = 1000
    }
}
