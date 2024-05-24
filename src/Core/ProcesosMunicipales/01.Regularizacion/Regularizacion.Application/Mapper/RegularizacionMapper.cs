using AutoMapper;
using Regularizacion.Application.Dtos;
using Regularizacion.Application.Interfaces;
using Regularizacion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Regularizacion.Application.Mapper
{
    public class RegularizacionMapper : IRegularizacionMapper
    {
        private readonly IMapper _mapper;

        public RegularizacionMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<RegularizacionProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public RegularizacionDomain CreateRegularizacion(RegularizacionDto regularizacion)
        {
            return _mapper.Map<RegularizacionDomain>(regularizacion);
        }

        public RegularizacionDto CreateRegularizacionDto(RegularizacionDomain regularizacion)
        {
            return _mapper.Map<RegularizacionDto>(regularizacion);
        }
    }
    public class RegularizacionProfile : Profile
    {
        public RegularizacionProfile()
        {
            CreateMap<RegularizacionDto, RegularizacionDomain>();
            CreateMap<RegularizacionDomain, RegularizacionDto>();
        }
    }
}
