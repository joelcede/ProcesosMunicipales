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
    public class ViviendaMapper : IViviendaMapper
    {
        private readonly IMapper _mapper;

        public ViviendaMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ViviendaProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public Vivienda CreateVivienda(ViviendaRequestDto viviendaRequestDto)
        {
            return _mapper.Map<Vivienda>(viviendaRequestDto);
        }

        public ViviendaRequestDto CreateViviendaDto(Vivienda vivienda)
        {
            return _mapper.Map<ViviendaRequestDto>(vivienda);
        }
    }
    public class ViviendaProfile : Profile
    {
        public ViviendaProfile()
        {
            CreateMap<ViviendaRequestDto, Vivienda>();
            CreateMap<Vivienda, ViviendaRequestDto>();
        }
    }
}
