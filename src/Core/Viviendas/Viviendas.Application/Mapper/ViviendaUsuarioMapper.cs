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
    public class ViviendaUsuarioMapper : IVivivendaUsuarioMapper
    {
        private readonly IMapper _mapper;

        public ViviendaUsuarioMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ViviendaUsuarioProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public ViviendaUsuarios CreateViviendaUsuario(ViviendaUsuarioDto viviendaRequestDto)
        {
            return _mapper.Map<ViviendaUsuarios>(viviendaRequestDto);
        }

        public ViviendaUsuarioDto CreateViviendaUsuarioDto(ViviendaUsuarios vivienda)
        {
            return _mapper.Map<ViviendaUsuarioDto>(vivienda);
        }
    }
    public class ViviendaUsuarioProfile : Profile
    {
        public ViviendaUsuarioProfile()
        {
            CreateMap<ViviendaUsuarioDto, ViviendaUsuarios>();
            CreateMap<ViviendaUsuarios, ViviendaUsuarioDto>();
        }
    }
}
