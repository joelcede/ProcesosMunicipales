using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viviendas.Domain.Enums;

namespace Viviendas.Domain.Interfaces
{
    public interface IViviendaDomain
    {
        Guid Id { get; }
        string Direccion { get; }
        string CodigoCatastral { get; }
        string Telefono { get; }
        string Coordenadas { get; }
        byte[] Imagen { get; }
        CiudadType Ciudad { get; }
        ProvinciaType Provincia { get; }
        PaisType Pais { get; }
        DateTime FechaCreacion { get; }
        DateTime FechaActualizacion { get; }
    }
}
