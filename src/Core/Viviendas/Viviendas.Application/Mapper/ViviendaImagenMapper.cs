using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viviendas.Application.Dtos;
using Viviendas.Application.Interfaces;
using Viviendas.Domain.Entities;

namespace Viviendas.Application.Mapper
{
    public class ViviendaImagenMapper : IViviendaImagenMapper
    {
        private readonly IMapper _mapper;

        public ViviendaImagenMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ViviendaProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public ViviendaImagenes CreateViviendaImagen(ViviendaImagenDto viviendaRequestDto)
        {
            return _mapper.Map<ViviendaImagenes>(viviendaRequestDto);
        }

        public ViviendaImagenDto CreateViviendaImagenDto(ViviendaImagenes vivienda)
        {
            return _mapper.Map<ViviendaImagenDto>(vivienda);
        }
    }
    public class ViviendaImagenProfile : Profile
    {
        public ViviendaImagenProfile()
        {
            CreateMap<ViviendaRequestDto, Vivienda>();
            CreateMap<Vivienda, ViviendaRequestDto>();
        }
    }
}
