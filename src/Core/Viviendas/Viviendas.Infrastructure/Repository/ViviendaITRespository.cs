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
using Viviendas.Domain.Enums;
using Viviendas.Domain.Interfaces;

namespace Viviendas.Infrastructure.Repository
{
    public class ViviendaITRespository : IViviendaTIRepository
    {
        private readonly IConfiguration _connectionString;
        private readonly ViviendaImagenMapper _viviendaImagenMapper;
        private readonly ViviendaUsuarioMapper _viviendaUserMapper;
        public static string _clase = string.Empty;
        public Logger _logger;

        #region Nombres de procedimientos almacenados
        public const string SP_VIVIENDA_INTERMEDIATE = "SP_VIVIENDA_INTERMEDIATE_TABLES";
        #endregion

        public ViviendaITRespository(IConfiguration configuration)
        {
            _connectionString = configuration;
            _logger = new Logger(configuration);
            _viviendaImagenMapper = new ViviendaImagenMapper();
            _viviendaUserMapper = new ViviendaUsuarioMapper();
            _clase = this.GetType().Name;
        }
        public Dictionary<string, object> keyValuePairsImagen(IViviendaImagenDomain vivienda, IntermediatTableType tabla = IntermediatTableType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (tabla != IntermediatTableType.None)
                parameters.Add("@Tabla", (int)tabla);

            if (vivienda.Id != Guid.Empty)
                parameters.Add("@IdTableInter", vivienda.Id);
            else
                parameters.Add("@IdTableInter", Guid.NewGuid());

            if (vivienda.IdVivienda != Guid.Empty)
                parameters.Add("@IdVivienda", vivienda.IdVivienda);
            else
                throw new ArgumentNullException(nameof(vivienda.IdVivienda), "El Id de la vivienda no puede ser nulo.");

            if (vivienda.IdImagen != Guid.Empty)
                parameters.Add("@IdImagen", vivienda.IdImagen);
            else
                throw new ArgumentNullException(nameof(vivienda.IdImagen), "El Id de la imagen no puede ser nulo.");

            return parameters;
        }
        public Dictionary<string, object> keyValuePairsUsuario(IViviendaUsuarioDomain vivienda, IntermediatTableType tabla = IntermediatTableType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (tabla != IntermediatTableType.None)
                parameters.Add("@Tabla", (int)tabla);

            if (vivienda.Id != Guid.Empty) 
                parameters.Add("@IdTableInter", vivienda.Id);
            else
                parameters.Add("@IdTableInter", Guid.NewGuid());

            if (vivienda.IdVivienda != Guid.Empty)
                parameters.Add("@IdVivienda", vivienda.IdVivienda);
            else
                throw new ArgumentNullException(nameof(vivienda.IdVivienda), "El Id de la vivienda no puede ser nulo.");

            if (vivienda.IdUsuario != Guid.Empty && (tabla == IntermediatTableType.ViviendaFamiliar || tabla == IntermediatTableType.ViviendaPropietario))
                parameters.Add("@IdUsuario", vivienda.IdUsuario);

            if (vivienda.IdImagen != Guid.Empty && tabla == IntermediatTableType.ViviendaImagen)
                parameters.Add("@IdImagen", vivienda.IdImagen);

            return parameters;
        }
        public async Task<ViviendaImagenes> AddViviendaImagenAsync(ViviendaImagenDto vivienda, IntermediatTableType tabla = IntermediatTableType.None)
        {
            _logger.LogInicio(_clase);
            if (vivienda == null)
            {
                var error = $"El objeto Vivienda no puede ser nulo.";
                _logger.LogError(_clase, error);
                throw new ArgumentNullException(nameof(Vivienda), error);
            }

            var parameters = keyValuePairsImagen(vivienda, tabla);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_VIVIENDA_INTERMEDIATE, parameters);
            vivienda.Id = (Guid)parameters["@IdTableInter"];
            var viviendaM = _viviendaImagenMapper.CreateViviendaImagen(vivienda);
            _logger.LogFin(_clase);
            return viviendaM;
        }
        public async Task<ViviendaImagenes> GetViviendaImagenByIdAsync(Guid id, IntermediatTableType tabla)
        {
            _logger.LogInicio(_clase);
            var vivienda = new ViviendaImagenDto();
            vivienda.IdVivienda = id;
            var parameters = keyValuePairsImagen(vivienda, tabla);
            var viviendaR = await new Database(_connectionString).ExecuteScalarAsync<ViviendaImagenes>(SP_VIVIENDA_INTERMEDIATE, parameters);
            //var viviendaM = _viviendaImagenMapper.CreateViviendaImagen(viviendaR);
            _logger.LogFin(_clase);
            return viviendaR;
        }
        public async Task<ViviendaUsuarios> AddViviendaUsuarioAsync(ViviendaUsuarioDto vivienda, IntermediatTableType tabla = IntermediatTableType.None)
        {
            _logger.LogInicio(_clase);
            if (vivienda == null)
            {
                var error = $"El objeto Vivienda no puede ser nulo.";
                _logger.LogError(_clase, error);
                throw new ArgumentNullException(nameof(Vivienda), error);
            }

            var parameters = keyValuePairsUsuario(vivienda, tabla);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_VIVIENDA_INTERMEDIATE, parameters);
            vivienda.Id = (Guid)parameters["@IdTableInter"];
            var viviendaM = _viviendaUserMapper.CreateViviendaUsuario(vivienda);
            _logger.LogFin(_clase);
            return viviendaM;
        }
        public async Task<IEnumerable<ViviendaFamProp>> GetViviendaUsuarioByIdAsync(Guid id, IntermediatTableType tabla = IntermediatTableType.None)
        {
            _logger.LogInicio(_clase);
            var cliente = new ViviendaUsuarioDto();
            cliente.IdVivienda = id;
            var parameters = keyValuePairsUsuario(cliente, tabla);
            var response = await new Database(_connectionString).ExecuteReaderAsync<ViviendaFamProp>(SP_VIVIENDA_INTERMEDIATE, parameters);
            _logger.LogFin(_clase);
            return response;
        }
        public async Task<IEnumerable<ViviendaFamProp>> GetViviendaPropFamByIdAsync(Guid id, IntermediatTableType tabla)
        {
            _logger.LogInicio(_clase);
            var vivienda = new ViviendaUsuarioDto();
            vivienda.IdVivienda = id;
            var parameters = keyValuePairsUsuario(vivienda, tabla);
            var viviendaR = await new Database(_connectionString).ExecuteReaderAsync<ViviendaFamProp>(SP_VIVIENDA_INTERMEDIATE, parameters);
            _logger.LogFin(_clase);
            return viviendaR;
        }
        public Dictionary<string, object> keyValuePairsDelete(Guid idVivienda, IntermediatTableType tabla = IntermediatTableType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (tabla != IntermediatTableType.None)
                parameters.Add("@Tabla", (int)tabla);

            if (idVivienda != Guid.Empty)
                parameters.Add("@IdVivienda", idVivienda);
            else
                throw new ArgumentNullException(nameof(idVivienda), "El Id de la vivienda no puede ser nulo.");

            return parameters;
        }
        public async Task DeleteViviendaIT(Guid idVivienda)
        {
            _logger.LogInicio(_clase);
            var parameters = keyValuePairsDelete(idVivienda, IntermediatTableType.S_DeleteViviendaIT);
            await new Database(_connectionString).ExecuteReaderAsync<ViviendaFamProp>(SP_VIVIENDA_INTERMEDIATE, parameters);
            _logger.LogFin(_clase);
        }
    }
}
  