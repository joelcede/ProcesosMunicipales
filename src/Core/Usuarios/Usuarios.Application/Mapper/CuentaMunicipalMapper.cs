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
    public class CuentaMunicipalMapper : ICuentaMunicipalMapper
    {
        private readonly IMapper _mapper;

        public CuentaMunicipalMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<CuentaMunicipalProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public CuentaMunicipalDomain CreateCuentaMunicipal(CuentaMunicipalDto cuentaMunicipalDto)
        {
            return _mapper.Map<CuentaMunicipalDomain>(cuentaMunicipalDto);
        }

        public CuentaMunicipalDto CreateCuentaMunicipalDto(CuentaMunicipalDomain cuentaMunicipal)
        {
            return _mapper.Map<CuentaMunicipalDto>(cuentaMunicipal);
        }
    }
    public class CuentaMunicipalProfile : Profile
    {
        public CuentaMunicipalProfile()
        {
            CreateMap<CuentaMunicipalDto, CuentaMunicipalDomain>();
            CreateMap<CuentaMunicipalDomain, CuentaMunicipalDto>();
        }
    }
}
