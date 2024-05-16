using Commons.logger;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Commons.Connection
{
    public class Database
    {
        public readonly string conexion = "DefaultConnection";
        private readonly IConfiguration _configuration;
        public static string _clase = string.Empty;
        public Logger _logger;

        public Database(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new Logger(configuration);
            _clase = this.GetType().Name;
        }

        #region SP
        public async Task ExecuteNonQueryAsync(string storedProcedureName, Dictionary<string, object> parameters)
        {
            _logger.LogInicio(_clase);
            if (string.IsNullOrEmpty(storedProcedureName))
                throw new ArgumentException("El nombre del procedimiento almacenado no puede ser nulo o vacío", nameof(storedProcedureName));

            try
            {
                var cadenaConexion = _configuration.GetConnectionString(conexion);
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch(SqlException ex)
            {
                _logger.LogErrorSQL(_clase, ex.Message);
                throw new ApplicationException("Ocurrió un error SQL al ejecutar el procedimiento almacenado.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(_clase, ex.Message);
                throw new ApplicationException("Ocurrió un error al ejecutar el procedimiento almacenado.", ex);
            }
            finally
            {
                _logger.LogFin(_clase);
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string storedProcedureName, Dictionary<string, object> parameters)
        {
            _logger.LogInicio(_clase);
            if (string.IsNullOrEmpty(storedProcedureName))
                throw new ArgumentException("El nombre del procedimiento almacenado no puede ser nulo o vacío", nameof(storedProcedureName));

            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString(conexion)))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        await conn.OpenAsync();
                        var result = await cmd.ExecuteScalarAsync();

                        if (result == null || result == DBNull.Value)
                        {
                            return default(T);
                        }

                        return (T)Convert.ChangeType(result, typeof(T));
                    }
                }
            }
            catch (SqlException ex)
            {
                _logger.LogErrorSQL(_clase, ex.Message);
                throw new ApplicationException("Ocurrió un error SQL al ejecutar el procedimiento almacenado.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(_clase, ex.Message);
                throw new ApplicationException("Ocurrió un error al ejecutar el procedimiento almacenado.", ex);
            }
            finally
            {
                _logger.LogFin(_clase);
            }
            
        }

        public async Task<List<T>> ExecuteReaderAsync<T>(string storedProcedureName, Dictionary<string, object> parameters, Func<IDataReader, T> readFunc)
        {
            _logger.LogInicio(_clase);
            if (string.IsNullOrEmpty(storedProcedureName))
                throw new ArgumentException("El nombre del procedimiento almacenado no puede ser nulo o vacío", nameof(storedProcedureName));

            var results = new List<T>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString(conexion)))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        await conn.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(readFunc(reader));
                            }
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                _logger.LogErrorSQL(_clase, ex.Message);
                throw new ApplicationException("Ocurrió un error SQL al ejecutar el procedimiento almacenado.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(_clase, ex.Message);
                throw new ApplicationException("Ocurrió un error al ejecutar el procedimiento almacenado.", ex);
            }
            finally
            {
                _logger.LogFin(_clase);
            }
            return results;
        }
        #endregion
    }
}
