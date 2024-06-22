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
        public Dictionary<string, object> keyValuePairs(ICuentaMunicipalDomain cm, CrudType operacion = CrudType.None, UsuarioType usuario = UsuarioType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
                parameters.Add("@Trx", (int)operacion);

            if (usuario != UsuarioType.None)
                parameters.Add("@TipoUsuario", (int)usuario);

            if (operacion != CrudType.GetById && operacion != CrudType.ListAll && operacion != CrudType.None && cm.Id == Guid.Empty)
                parameters.Add("@IdCuenta", Guid.NewGuid());
            else if (cm.Id != Guid.Empty && operacion != CrudType.ListAll && operacion != CrudType.None && operacion != CrudType.GetById)
                parameters.Add("@IdCuenta", cm.Id);

            if(operacion != CrudType.None)
                parameters.Add("@IdUsuario", cm.IdUsuario);

            if (!string.IsNullOrEmpty(cm.CuentaMunicipal) && (operacion == CrudType.Create || operacion == CrudType.Update))
                parameters.Add("@CuentaMunicipal", cm.CuentaMunicipal);

            if (!string.IsNullOrEmpty(cm.ContrasenaMunicipal) && (operacion == CrudType.Create || operacion == CrudType.Update))
                parameters.Add("@ContrasenaMunicipal", EncryptionHelper.EncryptString(cm.ContrasenaMunicipal));

            return parameters;
        }

        public async Task<CuentaMunicipalDomain> AddCuentaMunicipalAsync(CuentaMunicipalDto cuentaMunicipal, UsuarioType usuario = UsuarioType.None)
        {
            _logger.LogInicio(_clase);
            if (cuentaMunicipal == null)
            {
                var error = $"El objeto Cuenta Municipal no puede ser nulo.";
                _logger.LogError(_clase, error);
                throw new ArgumentNullException(nameof(cuentaMunicipal), error);
            }

            var parameters = keyValuePairs(cuentaMunicipal, CrudType.Create, usuario);
            var cuenta = await new Database(_connectionString).ExecuteScalarAsync<CuentaMunicipalDomain>(SP_USUARIO, parameters);

            _logger.LogFin(_clase);
            return cuenta;
        }

        public async Task DeleteCuentaMunicipalAsync(Guid id, UsuarioType usuario = UsuarioType.None)
        {
            _logger.LogInicio(_clase);
            var cuenta = new CuentaMunicipalDomain();
            cuenta.IdUsuario = id;
            var parameters = keyValuePairs(cuenta, CrudType.Delete, usuario);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_USUARIO, parameters);
            _logger.LogFin(_clase);
        }

        public async Task<CuentaMunicipalDomain> GetCuentaByIdUsuarioAsync(Guid id, UsuarioType usuario = UsuarioType.None)
        {
            _logger.LogInicio(_clase);
            var cuenta = new CuentaMunicipalDomain();
            cuenta.IdUsuario = id;
            var parameters = keyValuePairs(cuenta, CrudType.GetById, usuario);
            var response = await new Database(_connectionString).ExecuteScalarAsync<CuentaMunicipalDomain>(SP_USUARIO, parameters);
            if(response != null && !response.CuentaMunicipal.Equals(""))
                response.ContrasenaMunicipal = EncryptionHelper.DecryptString(response.ContrasenaMunicipal);
            _logger.LogFin(_clase);
            return response;
        }

        public async Task UpdateCuentaMunicipalAsync(Guid identity, CuentaMunicipalDomain cuentaMunicipal, UsuarioType usuario = UsuarioType.None)
        {
            _logger.LogInicio(_clase);
            cuentaMunicipal.IdUsuario = identity;
            var parameters = keyValuePairs(cuentaMunicipal, CrudType.Update, usuario);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_USUARIO, parameters);
            _logger.LogFin(_clase);
        }
    }
}
