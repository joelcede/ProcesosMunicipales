using Commons.Connection;
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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _connectionString;
        private readonly UsuarioMapper _viviendaMapper;
        public static string _clase = string.Empty;
        public Logger _logger;

        #region Nombres de procedimientos almacenados
        public const string SP_USUARIO = "SP_USUARIO_CRUD";
        #endregion

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration;
            _logger = new Logger(configuration);
            _clase = this.GetType().Name;
            _viviendaMapper = new UsuarioMapper();
        }

        public Dictionary<string, object> keyValuePairs(IUsuarioDomain usuario, CrudType operacion = CrudType.None, UsuarioType userType = UsuarioType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
                parameters.Add("@Trx", (int)operacion);

            if (userType != UsuarioType.None)
                parameters.Add("@TipoUsuario", (int)userType);

            if (operacion != CrudType.ListAll && operacion  != CrudType.None && usuario.Id == Guid.Empty)
                parameters.Add("@IdUsuario", Guid.NewGuid());
            else if (usuario.Id != Guid.Empty && operacion != CrudType.ListAll && operacion != CrudType.None)
                parameters.Add("@IdUsuario", usuario.Id);

            if (!string.IsNullOrEmpty(usuario.Nombres))
                parameters.Add("@Nombres", usuario.Nombres);
            if (!string.IsNullOrEmpty(usuario.Apellidos))
                parameters.Add("@Apellidos", usuario.Apellidos);

            if (!string.IsNullOrEmpty(usuario.DNI) && IsNumeric(usuario.DNI))
                parameters.Add("@DNI", usuario.DNI);
            else if (!string.IsNullOrEmpty(usuario.DNI))
                throw new ArgumentException("El campo DNI debe contener solo números.");

            // Validar que el Teléfono Celular solo contenga números
            if ((operacion == CrudType.Create || operacion == CrudType.Update) && IsNumeric(usuario.TelefonoCelular))
                parameters.Add("@Celular", usuario.TelefonoCelular);
            else if (!string.IsNullOrEmpty(usuario.TelefonoCelular))
                throw new ArgumentException("El campo Teléfono Celular debe contener solo números.");

            // Validar que el Teléfono Convencional solo contenga números
            if ((operacion == CrudType.Create || operacion == CrudType.Update) && IsNumeric(usuario.TelefonoConvencional))
                parameters.Add("@Telefono", usuario.TelefonoConvencional);
            else if (!string.IsNullOrEmpty(usuario.TelefonoConvencional))
                throw new ArgumentException("El campo Teléfono Convencional debe contener solo números.");

            if ( (operacion == CrudType.Create || operacion == CrudType.Update) && userType == UsuarioType.Propietario)
                parameters.Add("@EsPrincipal", Convert.ToBoolean(usuario.esPrincipal) );


            return parameters;
        }
        private bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^\d+$");
        }

        public async Task<Usuario> AddUsuarioAsync(UsuarioRequestDto usuario, UsuarioType userType)
        {
            _logger.LogInicio(_clase);
            if (usuario == null)
            {
                var error = $"El objeto {userType} no puede ser nulo.";
                _logger.LogError(_clase, error);
                throw new ArgumentNullException(nameof(usuario), error);
            }
                
            var parameters = keyValuePairs(usuario, CrudType.Create, userType);
            var response = await new Database(_connectionString).ExecuteScalarAsync<Usuario>(SP_USUARIO, parameters);

            //usuario.Id = (Guid)parameters["@IdUsuario"];
            //var usuarioM = _viviendaMapper.CreateVivienda(usuario);
            _logger.LogFin(_clase);
            return response;

        }

        public async Task DeleteUsuarioAsync(Guid id, UsuarioType userType)
        {
            _logger.LogInicio(_clase);

            var cliente = new UsuarioRequestDto();
            cliente.Id = id;
            var parameters = keyValuePairs(cliente, CrudType.Delete, userType);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_USUARIO, parameters);
            _logger.LogFin(_clase);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync(UsuarioType userType)
        {
            _logger.LogInicio(_clase);
            var cliente = new UsuarioRequestDto();
            var parameters = keyValuePairs(cliente, CrudType.ListAll, userType);
            var response = await new Database(_connectionString).ExecuteReaderAsync<Usuario>(SP_USUARIO, parameters);
            _logger.LogFin(_clase);
            return response;
        }

        public async Task<Usuario> GetUsuarioByIdAsync(Guid id, UsuarioType userType)
        {
            _logger.LogInicio(_clase);
            var cliente = new UsuarioRequestDto();
            cliente.Id = id;
            var parameters = keyValuePairs(cliente, CrudType.GetById, userType);
            var response = await new Database(_connectionString).ExecuteScalarAsync<Usuario>(SP_USUARIO, parameters);
            _logger.LogFin(_clase);
            return response;
        }

        public async Task UpdateUsuarioAsync(Guid id, Usuario usuario, UsuarioType userType)
        {
            _logger.LogInicio(_clase);
            usuario.Id = id;
            var parameters = keyValuePairs(usuario, CrudType.Update, userType);
            await new Database(_connectionString).ExecuteNonQueryAsync(SP_USUARIO, parameters);
            _logger.LogFin(_clase);
        }
    }
}
