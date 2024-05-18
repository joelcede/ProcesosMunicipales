using AutoMapper;
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
using Viviendas.Domain.Enums;

namespace Viviendas.Infrastructure.Repository
{
    public class ViviendaUsuarioRepository : IViviendaUsuarioRepository
    {
        private readonly IConfiguration _connectionString;
        private readonly ViviendaUsuarioMapper _viviendaUserMapper;
        public static string _clase = string.Empty;
        public Logger _logger;

        #region Nombres de procedimientos almacenados
        public const string SP_VIVIENDA_USUARIO = "SP_VIVIENDA_USUARIO";
        #endregion

        public ViviendaUsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration;
            _logger = new Logger(configuration);
            _viviendaUserMapper = new ViviendaUsuarioMapper();
        }


        public Dictionary<string, object> keyValuePairs(IViviendaUsuarioDomain vivienda, UsuarioType operacion = UsuarioType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != UsuarioType.None)
                parameters.Add("@TipoUsuario", (int)operacion);

            if (vivienda.Id != Guid.Empty)
                parameters.Add("@IdViviendaUsuario", vivienda.Id);
            else
                parameters.Add("@IdViviendaUsuario", Guid.NewGuid());

            if (vivienda.IdVivienda != Guid.Empty)
                parameters.Add("@IdVivienda", vivienda.IdVivienda);

            if (vivienda.IdUsuario != Guid.Empty)
                parameters.Add("@IdUsuario", vivienda.IdUsuario);
            return parameters;
        }

        public async Task<ViviendaUsuarios> AddViviendaUsuarioAsync(ViviendaUsuarioDto vivienda, UsuarioType operacion = UsuarioType.None)
        {
            _logger.LogInicio(_clase);
            if (vivienda == null)
            {
                var error = $"El objeto Vivienda no puede ser nulo.";
                _logger.LogError(_clase, error);
                throw new ArgumentNullException(nameof(Vivienda), error);
            }

            var parameters = keyValuePairs(vivienda, operacion);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_VIVIENDA_USUARIO, parameters);

            var viviendaM = _viviendaUserMapper.CreateViviendaUsuario(vivienda);
            _logger.LogFin(_clase);
            return viviendaM;
        }
    }
}
