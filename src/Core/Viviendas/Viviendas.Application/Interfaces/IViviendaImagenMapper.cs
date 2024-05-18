using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viviendas.Application.Dtos;
using Viviendas.Domain.Entities;

namespace Viviendas.Application.Interfaces
{
    public interface IViviendaImagenMapper
    {
        ViviendaImagenes CreateViviendaImagen(ViviendaImagenDto viviendaRequestDto);
        ViviendaImagenDto CreateViviendaImagenDto(ViviendaImagenes vivienda);
    }
}
