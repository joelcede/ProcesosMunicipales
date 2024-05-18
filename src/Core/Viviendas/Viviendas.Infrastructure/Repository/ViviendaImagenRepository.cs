using Commons.Connection;
using Commons.logger;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viviendas.Application.Dtos;
using Viviendas.Application.Mapper;
using Viviendas.Application.Repository;
using Viviendas.Domain.Entities;
using Viviendas.Domain.Interfaces;

namespace Viviendas.Infrastructure.Repository
{
    public class ViviendaImagenRepository : IViviendaImagenRepository
    {
        private readonly IConfiguration _connectionString;
        private readonly ViviendaImagenMapper _viviendaImagenMapper;
        public static string _clase = string.Empty;
        public Logger _logger;

        #region Nombres de procedimientos almacenados
        public const string SP_VIVIENDA_USUARIO = "SP_VIVIENDA_USUARIO";
        #endregion

        public ViviendaImagenRepository(IConfiguration configuration)
        {
            _connectionString = configuration;
            _logger = new Logger(configuration);
            _viviendaImagenMapper = new ViviendaImagenMapper();
        }


        public Dictionary<string, object> keyValuePairs(IViviendaImagenDomain vivienda)
        {
            var parameters = new Dictionary<string, object>();

            if (vivienda.Id != Guid.Empty)
                parameters.Add("@IdViviendaUsuario", vivienda.Id);
            else
                parameters.Add("@IdViviendaUsuario", Guid.NewGuid());

            if (vivienda.IdVivienda != Guid.Empty)
                parameters.Add("@IdVivienda", vivienda.IdVivienda);

            if (vivienda.IdImagen != Guid.Empty)
                parameters.Add("@IdImagen", vivienda.IdImagen);

            return parameters;
        }

        public async Task<ViviendaImagenes> AddViviendaImagenAsync(ViviendaImagenDto vivienda)
        {
            _logger.LogInicio(_clase);
            if (vivienda == null)
            {
                var error = $"El objeto Vivienda no puede ser nulo.";
                _logger.LogError(_clase, error);
                throw new ArgumentNullException(nameof(Vivienda), error);
            }

            var parameters = keyValuePairs(vivienda);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_VIVIENDA_USUARIO, parameters);

            var viviendaM = _viviendaImagenMapper.CreateViviendaImagen(vivienda);
            _logger.LogFin(_clase);
            return viviendaM;
        }
    }
}
