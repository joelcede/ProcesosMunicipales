using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viviendas.Application.Dtos;
using Viviendas.Domain.Entities;

namespace Viviendas.Application.Interfaces
{
    public interface IViviendaMapper
    {
        Vivienda CreateVivienda(ViviendaRequestDto viviendaRequestDto);
        ViviendaRequestDto CreateViviendaDto(Vivienda vivienda);

    }
}
