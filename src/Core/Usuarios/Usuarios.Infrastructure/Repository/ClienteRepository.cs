using Commons.Connection;
using Commons.logger;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Application.Dtos;
using Usuarios.Application.Repository;
using Usuarios.Domain.Entities;
using Usuarios.Domain.Enums;

namespace Usuarios.Infrastructure.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConfiguration _connectionString;
        public static string _clase = string.Empty;
        public Logger _logger;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration;
            _logger = new Logger(configuration);
            _clase = this.GetType().Name;
        }

        public Dictionary<string, object> keyValuePairs(ClienteRequestDto cliente, CrudType operacion = CrudType.None)
        {
            var parameters = new Dictionary<string, object>();

            if (operacion != CrudType.None)
                parameters.Add("@Trx", (int)operacion);

            if (cliente.IdCliente != Guid.Empty)
                parameters.Add("@IdCliente", cliente.IdCliente);
            else
                parameters.Add("@IdCliente", Guid.NewGuid());

            if (!string.IsNullOrEmpty(cliente.Nombres))
                parameters.Add("@Nombres", cliente.Nombres);
            if (!string.IsNullOrEmpty(cliente.Apellidos))
                parameters.Add("@Apellidos", cliente.Apellidos);
            if (!string.IsNullOrEmpty(cliente.DNI))
                parameters.Add("@DNI", cliente.DNI);
            if (!string.IsNullOrEmpty(cliente.TelefonoCelular))
                parameters.Add("@Celular", cliente.TelefonoCelular);
            if (!string.IsNullOrEmpty(cliente.TelefonoConvencional))
                parameters.Add("@Telefono", cliente.TelefonoConvencional);

            return parameters;
        }

        public async Task AddClienteAsync(ClienteRequestDto cliente)
        {
            _logger.LogInicio(_clase);
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente), "El objeto cliente no puede ser nulo.");

            try
            {
                var parameters = keyValuePairs(cliente, CrudType.Create);
                await new Database(_connectionString).ExecuteNonQueryAsync("SP_USUARIO_CRUD", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(_clase, ex.Message);
                throw new ApplicationException("Ocurrió un error SQL al ejecutar el procedimiento almacenado.", ex);
            }
            finally
            {
                _logger.LogFin(_clase);
            }
        }

        public async Task DeleteClienteAsync(Guid id)
        {
            _logger.LogInicio(_clase);
            try
            {
                var cliente = new ClienteRequestDto();
                cliente.IdCliente = id;
                var parameters = keyValuePairs(cliente, CrudType.Delete);
                await new Database(_connectionString).ExecuteNonQueryAsync("SP_USUARIO_CRUD", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(_clase, ex.Message);
            }
            finally
            {
                _logger.LogFin(_clase);
            }
        }

        public Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> GetClienteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateClienteAsync(Guid identity, Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
