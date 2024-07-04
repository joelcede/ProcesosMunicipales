using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuarios.Domain.Enums
{
    public enum CrudType
    {
        None = 0,
        ListAll = 1,
        GetById = 2,
        Create = 3,
        Update = 4,
        Delete = 5,
        DeleteAll = 6,
    }
}
