using Commons.Connection;
using Commons.Cryptography;
using Commons.logger;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Usuarios.Application.Dtos;
using Usuarios.Application.Mapper;
using Usuarios.Application.Repository;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;
using Usuarios.Domain.Interfaces;

namespace Usuarios.Infrastructure.Repository
{
    public class CuentaMunicipalRepository : ICuentaMunicipalRepository
    {
        private readonly IConfiguration _connectionString;
        private readonly CuentaMunicipalMapper _cuentaMunicipalMapper;
        public static string _clase = string.Empty;
        public Logger _logger;

        #region Nombres de procedimientos almacenados
        public const string SP_USUARIO = "SP_CUENTA_MUNICIPAL";
        #endregion

        public CuentaMunicipalRepository(IConfiguration configuration)
        {
            _connectionString = configuration;
            _logger = new Logger(configuration);
            _clase = this.GetType().Name;
            _cuentaMunicipalMapper = new CuentaMunicipalMapper();
        }
        public Dictionary<string, object> keyValuePairs(ICuentaMunicipalDomain cm, CrudType operacion = CrudType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
                parameters.Add("@Trx", (int)operacion);

            if (operacion != CrudType.None && operacion != CrudType.None && cm.Id == Guid.Empty)
                parameters.Add("@IdCuenta", Guid.NewGuid());
            else if (cm.Id != Guid.Empty && operacion != CrudType.ListAll && operacion != CrudType.None)
                parameters.Add("@IdCuenta", cm.Id);

            if(operacion != CrudType.None)
                parameters.Add("@IdUsuario", cm.IdUsuario);

            if (!string.IsNullOrEmpty(cm.cuentaMunicipal) && (operacion == CrudType.Create || operacion == CrudType.Update))
                parameters.Add("@CuentaMunicipal", cm.cuentaMunicipal);

            if (!string.IsNullOrEmpty(cm.contrasenaMunicipal) && (operacion == CrudType.Create || operacion == CrudType.Update))
                parameters.Add("@ContrasenaMunicipal", EncryptionHelper.EncryptString(cm.contrasenaMunicipal));

            if (cm.EsPropietario && (operacion == CrudType.Create || operacion == CrudType.Update))
                parameters.Add("@EsPropietario", cm.EsPropietario);



            return parameters;
        }

        public async Task<CuentaMunicipal> AddCuentaMunicipalAsync(CuentaMunicipalDto cuentaMunicipal)
        {
            _logger.LogInicio(_clase);
            if (cuentaMunicipal == null)
            {
                var error = $"El objeto Cuenta Municipal no puede ser nulo.";
                _logger.LogError(_clase, error);
                throw new ArgumentNullException(nameof(cuentaMunicipal), error);
            }

            var parameters = keyValuePairs(cuentaMunicipal, CrudType.Create);
            var cuenta = await new Database(_connectionString).ExecuteScalarAsync<CuentaMunicipal>(SP_USUARIO, parameters);

            _logger.LogFin(_clase);
            return cuenta;
        }

        public async Task DeleteCuentaMunicipalAsync(Guid id)
        {
            _logger.LogInicio(_clase);
            var cuenta = new CuentaMunicipal();
            cuenta.IdUsuario = id;
            var parameters = keyValuePairs(cuenta, CrudType.Delete);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_USUARIO, parameters);
            _logger.LogFin(_clase);
        }

        public async Task<CuentaMunicipal> GetCuentaByIdUsuarioAsync(Guid id)
        {
            _logger.LogInicio(_clase);
            var cuenta = new CuentaMunicipal();
            cuenta.IdUsuario = id;
            var parameters = keyValuePairs(cuenta, CrudType.GetById);
            var response = await new Database(_connectionString).ExecuteScalarAsync<CuentaMunicipal>(SP_USUARIO, parameters);
            _logger.LogFin(_clase);
            return response;
        }

        public async Task UpdateCuentaMunicipalAsync(Guid identity, CuentaMunicipal cuentaMunicipal)
        {
            _logger.LogInicio(_clase);
            cuentaMunicipal.IdUsuario = identity;
            var parameters = keyValuePairs(cuentaMunicipal, CrudType.Update);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_USUARIO, parameters);
            _logger.LogFin(_clase);
        }
    }
}
