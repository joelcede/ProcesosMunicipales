using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viviendas.Domain.Interfaces
{
    public interface IViviendaImagenDomain
    {
        public Guid Id { get; set; }
        public Guid IdImagen { get; set; }
        public Guid IdVivienda { get; set; }
    }
}
