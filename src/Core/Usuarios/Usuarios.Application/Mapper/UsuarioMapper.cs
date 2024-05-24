using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.Dtos;
using Usuarios.Application.Interfaces;
using Usuarios.Domain.Entities;

namespace Usuarios.Application.Mapper
{
    public class UsuarioMapper : IUsuarioMapper
    {
        private readonly IMapper _mapper;

        public UsuarioMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<UsuarioProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public Usuario CreateVivienda(UsuarioRequestDto usuarioRequestDto)
        {
            return _mapper.Map<Usuario>(usuarioRequestDto);
        }

        public UsuarioRequestDto CreateViviendaDto(Usuario usuario)
        {
            return _mapper.Map<UsuarioRequestDto>(usuario);
        }

    }
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioRequestDto, Usuario>();
            CreateMap<Usuario, UsuarioRequestDto>();
        }
    }
}
