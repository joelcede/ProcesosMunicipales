using Commons.Connection;
using Commons.Cryptography;
using Commons.logger;
using Microsoft.Extensions.Configuration;
using Regularizacion.Application.Dtos;
using Regularizacion.Application.Mapper;
using Regularizacion.Application.Repository;
using Regularizacion.Domain.Entities;
using Regularizacion.Domain.Enums;
using Regularizacion.Domain.Interfaces;
using Regularizacion.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Infrastructure.Repository
{
    public class RegularizacionRepository : IRegularizacionRepository
    {
        private readonly IConfiguration _connectionString;
        private readonly RegularizacionMapper _regularizacionMapper;
        public static string _clase = string.Empty;
        public Logger _logger;

        #region Nombres de procedimientos almacenados
        public const string SP_REGULARIZACION= "SP_REGULARIZACION";
        #endregion

        public RegularizacionRepository(IConfiguration configuration)
        {
            _connectionString = configuration;
            _logger = new Logger(configuration);
            _regularizacionMapper = new RegularizacionMapper();
            _clase = this.GetType().Name;
        }

        public Dictionary<string, object> CreateR(IRegularizacionDomain regularizacion, CrudType operacion = CrudType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
            {
                parameters.Add("@Trx", (int)operacion);

                if (regularizacion.Id == Guid.Empty)
                    parameters.Add("@IdRegularizacion", Guid.NewGuid());
                else
                    parameters.Add("@IdRegularizacion", regularizacion.Id);

                parameters.Add("@IdVivienda", regularizacion.IdVivienda);
                parameters.Add("@NumeroExpediente", regularizacion.NumeroExpediente);
                parameters.Add("@ValorRegularizacion", regularizacion.ValorRegularizacion);
                parameters.Add("@Anticipo", regularizacion.Anticipo);
                parameters.Add("@ValorPendiente", regularizacion.ValorPendiente);
                parameters.Add("@Pagado", regularizacion.Pagado); 
                parameters.Add("@Estado", regularizacion.Estado);
                parameters.Add("@FechaRegistro", regularizacion.FechaRegistro);
                //parameters.Add("@FechaInsercion", regularizacion.FechaInsercion);
                //parameters.Add("@FechaActualizacion", regularizacion.FechaActualizacion);
                parameters.Add("@Correo", regularizacion.Correo);
                parameters.Add("@Contrasena", EncryptionHelper.EncryptString(regularizacion.Contrasena));
                parameters.Add("@NumRegularizacion", regularizacion.numRegularizacion);
            }
            return parameters;
        }
        public async Task<RegularizacionDomain> AddRegularizacionAsync(RegularizacionDto regularizacion)
        {
            _logger.LogInicio(_clase);
            if (regularizacion == null)
            {
                var error = $"El objeto Regularizacion no puede ser nulo.";
                _logger.LogError(_clase, error);
                throw new ArgumentNullException(nameof(regularizacion), error);
            }

            var parameters = CreateR(regularizacion, CrudType.Create);
            var response = await new Database(_connectionString).ExecuteScalarAsync<RegularizacionDomain>(SP_REGULARIZACION, parameters);
            _logger.LogFin(_clase);
            return response;
        }
        
        public Dictionary<string, object> DeleteR(IRegularizacionDomain regularizacion, CrudType operacion = CrudType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
            {
                parameters.Add("@Trx", (int)operacion);
                parameters.Add("@IdRegularizacion", regularizacion.Id);
            }
            return parameters;
        }
        public async Task DeleteRegularizacionAsync(Guid id)
        {
            _logger.LogInicio(_clase);

            var regularizacion = new RegularizacionDomain();
            regularizacion.Id = id;
            var parameters = DeleteR(regularizacion, CrudType.Delete);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_REGULARIZACION, parameters);
            _logger.LogFin(_clase);
        }

        public Dictionary<string, object> GetR(Guid id, CrudType operacion = CrudType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
            {
                parameters.Add("@Trx", (int)operacion);
                parameters.Add("@IdRegularizacion", id);
            }
            return parameters;
        }
        public async Task<RegularizacionDomain> GetRegularizacionByIdAsync(Guid id)
        {
            _logger.LogInicio(_clase);
            var parameters = GetR(id, CrudType.GetById);
            var response = await new Database(_connectionString).ExecuteScalarAsync<RegularizacionDomain>(SP_REGULARIZACION, parameters);
            response.Contrasena = EncryptionHelper.DecryptString(response.Contrasena);
            _logger.LogFin(_clase);
            return response;
        }

        public Dictionary<string, object> GetRs(CrudType operacion = CrudType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
            {
                parameters.Add("@Trx", (int)operacion);
            }
            return parameters;
        }
        public async Task<IEnumerable<TarjetaRegularizacionDomain>> GetRegularizacionesAsync()
        {
            _logger.LogInicio(_clase);
            var parameters = GetRs(CrudType.ListAll);
            var response = await new Database(_connectionString).ExecuteReaderAsync<TarjetaRegularizacionDomain>(SP_REGULARIZACION, parameters);
            _logger.LogFin(_clase);
            return response;
        }

        public Dictionary<string, object> UpdateR(IRegularizacionDomain regularizacion, CrudType operacion = CrudType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
            {
                parameters.Add("@Trx", (int)operacion);
                parameters.Add("@IdRegularizacion", regularizacion.Id);
                parameters.Add("@NumeroExpediente", regularizacion.NumeroExpediente);
                parameters.Add("@ValorRegularizacion", regularizacion.ValorRegularizacion);
                parameters.Add("@Anticipo", regularizacion.Anticipo);
                parameters.Add("@ValorPendiente", regularizacion.ValorPendiente);
                parameters.Add("@Pagado", regularizacion.Pagado);
                parameters.Add("@Estado", regularizacion.Estado);
                parameters.Add("@FechaRegistro", regularizacion.FechaRegistro);
                parameters.Add("@NumRegularizacion", regularizacion.numRegularizacion);
            }
            return parameters;
        }
        public async Task UpdateRegularizacionAsync(RegularizacionDto regularizacion, Guid id)
        {
            _logger.LogInicio(_clase);
            regularizacion.Id = id;
            var parameters = UpdateR(regularizacion, CrudType.Update);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_REGULARIZACION, parameters);
            _logger.LogFin(_clase);
        }
        public Dictionary<string, object> GetCountReg(CrudType operacion = CrudType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
            {
                parameters.Add("@Trx", (int)operacion);
            }
            return parameters;
        }
        public async Task<NumRegDomain> ObtenerSecuenciaRegularizacion()
        {
            _logger.LogInicio(_clase);
            var parameters = GetCountReg(CrudType.GetCountAll);
            var result = await new Database(_connectionString).ExecuteScalarAsync<NumRegDomain>(SP_REGULARIZACION, parameters);
            _logger.LogFin(_clase);
            return result;
        }
        private Dictionary<string, object> GetContratoReg(Guid idReg, CrudType operacion = CrudType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
            {
                parameters.Add("@Trx", (int)operacion);
                if(idReg == Guid.Empty)
                    throw new ArgumentException("Debes ingresar el identificador de la regularizacion");
                else
                    parameters.Add("@IdRegularizacion", idReg);
            }
            return parameters;
        }

        public async Task<byte[]> GetContratoDefaultAsync(Guid idReg)
        {
            _logger.LogInicio(_clase);
            var parameters = GetContratoReg(idReg, CrudType.GetDataContrato);
            var result = await new Database(_connectionString).ExecuteScalarAsync<ContratoDefaultDomain>(SP_REGULARIZACION, parameters);
            //ContratoService service = new ContratoService();
            var contratoDefault = _connectionString.GetSection("contratos").GetValue<string>("contratoDefault") ?? "";
            var pdf = ContratoService.obtenerContratoByte(contratoDefault, result);
            _logger.LogFin(_clase);
            return pdf;
        }
    }
}
