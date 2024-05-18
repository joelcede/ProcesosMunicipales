using AutoMapper;
using Commons.Connection;
using Commons.logger;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Usuarios.Domain.Enums;
using Viviendas.Application.Dtos;
using Viviendas.Application.Interfaces;
using Viviendas.Application.Mapper;
using Viviendas.Application.Repository;
using Viviendas.Domain.Entities;
using Viviendas.Domain.Enums;
using Viviendas.Domain.Interfaces;

namespace Viviendas.Infrastructure.Repository
{
    public class ViviendaRepository : IViviendaRepository
    {
        private readonly IConfiguration _connectionString;
        private readonly ViviendaMapper _viviendaMapper;
        public static string _clase = string.Empty;
        public Logger _logger;

        #region Nombres de procedimientos almacenados
        public const string SP_VIVIENDA = "SP_VIVIENDA_CRUD";
        #endregion

        public ViviendaRepository(IConfiguration configuration)
        {
            _connectionString = configuration;
            _logger = new Logger(configuration);
            _viviendaMapper = new ViviendaMapper();
        }

        public Dictionary<string, object> keyValuePairs(IViviendaDomain vivienda, CrudType operacion = CrudType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
                parameters.Add("@Trx", (int)operacion);

            if (operacion != CrudType.ListAll && operacion != CrudType.None && vivienda.Id == Guid.Empty)
                parameters.Add("@IdVivienda", Guid.NewGuid());
            else if (vivienda.Id != Guid.Empty && operacion != CrudType.ListAll && operacion != CrudType.None)
                parameters.Add("@IdVivienda", vivienda.Id);

            if (!string.IsNullOrEmpty(vivienda.Direccion))
                parameters.Add("@Direccion", vivienda.Direccion);

            if (!string.IsNullOrEmpty(vivienda.CodigoCatastral))
                parameters.Add("@CodigoCatastral", vivienda.CodigoCatastral);

            if (!string.IsNullOrEmpty(vivienda.Telefono) && IsNumeric(vivienda.Telefono))
                parameters.Add("@Telefono", vivienda.Telefono);
            else if (!string.IsNullOrEmpty(vivienda.Telefono))
                throw new ArgumentException("El campo Telefono debe contener solo números.");

            if (!string.IsNullOrEmpty(vivienda.Coordenadas))
                parameters.Add("@Coordenadas", vivienda.Coordenadas);

            if (vivienda.Imagen !=  Array.Empty<byte>())
                parameters.Add("@Imagen", vivienda.Coordenadas);

            if (vivienda.Ciudad != CiudadType.None)
                parameters.Add("@IdCiudad", (int)vivienda.Ciudad);

            if (vivienda.Provincia != ProvinciaType.None)
                parameters.Add("@IdProvincia", (int)vivienda.Provincia);

            if (vivienda.Pais != PaisType.None)
                parameters.Add("@IdPais", (int)vivienda.Pais);

          
            return parameters;
        }
        private bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^\d+$");
        }

        public async Task<Vivienda> AddViviendaAsync(ViviendaRequestDto vivienda)
        {
            _logger.LogInicio(_clase);
            if (vivienda == null)
            {
                var error = $"El objeto Vivienda no puede ser nulo.";
                _logger.LogError(_clase, error);
                throw new ArgumentNullException(nameof(Vivienda), error);
            }

            var parameters = keyValuePairs(vivienda, CrudType.Create);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_VIVIENDA, parameters);

            var viviendaM = _viviendaMapper.CreateVivienda(vivienda);
            _logger.LogFin(_clase);
            return viviendaM;
        }

        public async Task DeleteViviendaAsync(Guid id)
        {
            _logger.LogInicio(_clase);

            var cliente = new ViviendaRequestDto();
            cliente.Id = id;
            var parameters = keyValuePairs(cliente, CrudType.Delete);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_VIVIENDA, parameters);
            _logger.LogFin(_clase);
        }

        public async Task<IEnumerable<Vivienda>> GetAllViviendasAsync()
        {
            _logger.LogInicio(_clase);
            var cliente = new ViviendaRequestDto();
            var parameters = keyValuePairs(cliente, CrudType.ListAll);
            var response = await new Database(_connectionString).ExecuteReaderAsync<Vivienda>(SP_VIVIENDA, parameters);
            _logger.LogFin(_clase);
            return response;
        }

        public async Task<Vivienda> GetViviendaByIdAsync(Guid id)
        {
            _logger.LogInicio(_clase);
            var cliente = new ViviendaRequestDto();
            cliente.Id = id;
            var parameters = keyValuePairs(cliente, CrudType.GetById);
            var response = await new Database(_connectionString).ExecuteScalarAsync<Vivienda>(SP_VIVIENDA, parameters);
            _logger.LogFin(_clase);
            return response;
        }

        public async Task UpdateViviendaAsync(Guid id, Vivienda Vivienda)
        {
            _logger.LogInicio(_clase);
            Vivienda.Id = id;
            var parameters = keyValuePairs(Vivienda, CrudType.Update);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_VIVIENDA, parameters);
            _logger.LogFin(_clase);
        }
    }
}
