using Commons.logger;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

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

        public async Task<T> ExecuteScalarAsync<T>(string storedProcedureName, Dictionary<string, object> parameters) where T : new()
        {
            _logger.LogInicio(_clase);
            if (string.IsNullOrEmpty(storedProcedureName))
                throw new ArgumentException("El nombre del procedimiento almacenado no puede ser nulo o vacío", nameof(storedProcedureName));

            try
            {
                var cadenaConexion = _configuration.GetConnectionString(conexion);
                using (SqlConnection conn = new SqlConnection(cadenaConexion))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                            }
                        }

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                T obj = new T();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    // Obtener el nombre de la columna del SqlDataReader
                                    string columnName = reader.GetName(i);

                                    // Buscar la propiedad en la clase T que coincide con el nombre de la columna
                                    PropertyInfo property = obj.GetType().GetProperty(columnName);

                                    // Si la propiedad existe y no es de solo lectura
                                    if (property != null && property.CanWrite)
                                    {
                                        // Asignar el valor a la propiedad del objeto
                                        var value = reader.GetValue(i);
                                        if (value == DBNull.Value || value == null)
                                        {
                                            // Asignar valor predeterminado basado en el tipo de la propiedad
                                            if (property.PropertyType == typeof(string))
                                            {
                                                property.SetValue(obj, string.Empty, null);
                                            }
                                            else if (property.PropertyType.IsValueType)
                                            {
                                                property.SetValue(obj, Activator.CreateInstance(property.PropertyType), null);
                                            }
                                            else
                                            {
                                                property.SetValue(obj, null, null);
                                            }
                                        }
                                        else if (value != DBNull.Value && property.PropertyType.IsEnum)
                                        {
                                            property.SetValue(obj, Enum.Parse(property.PropertyType, reader.GetValue(i)?.ToString()), null);
                                            //return Enum.ToObject(property.PropertyType, obj);
                                        }
                                        else if(value != DBNull.Value)
                                        {
                                            property.SetValue(obj, Convert.ChangeType(reader.GetValue(i), property.PropertyType), null);
                                        }
                                    }
                                }
                                return obj;
                            }
                        }
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
            return default(T); // Devuelve el valor predeterminado de T si no se encuentra ningún resultado
        }

        //public async Task<List<T>> ExecuteReaderAsync<T>(string storedProcedureName, Dictionary<string, object> parameters, Func<IDataReader, T> readFunc)
        //{
        //    _logger.LogInicio(_clase);
        //    if (string.IsNullOrEmpty(storedProcedureName))
        //        throw new ArgumentException("El nombre del procedimiento almacenado no puede ser nulo o vacío", nameof(storedProcedureName));

        //    var results = new List<T>();
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString(conexion)))
        //        {
        //            using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                foreach (var param in parameters)
        //                {
        //                    cmd.Parameters.AddWithValue(param.Key, param.Value);
        //                }

        //                await conn.OpenAsync();
        //                using (var reader = await cmd.ExecuteReaderAsync())
        //                {
        //                    while (await reader.ReadAsync())
        //                    {
        //                        results.Add(readFunc(reader));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch(SqlException ex)
        //    {
        //        _logger.LogErrorSQL(_clase, ex.Message);
        //        throw new ApplicationException("Ocurrió un error SQL al ejecutar el procedimiento almacenado.", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(_clase, ex.Message);
        //        throw new ApplicationException("Ocurrió un error al ejecutar el procedimiento almacenado.", ex);
        //    }
        //    finally
        //    {
        //        _logger.LogFin(_clase);
        //    }
        //    return results;
        //}
        public async Task<List<T>> ExecuteReaderAsync<T>(string storedProcedureName, Dictionary<string, object> parameters) where T : new()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            List<T> lista = new List<T>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros
                        foreach (var parametro in parameters)
                        {
                            command.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
                        }

                        // Ejecutar comando y mapear resultados
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                T obj = new T();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    // Obtener el nombre de la columna del SqlDataReader
                                    string columnName = reader.GetName(i);

                                    // Buscar la propiedad en la clase T que coincide con el nombre de la columna
                                    PropertyInfo property = obj.GetType().GetProperty(columnName);

                                    // Si la propiedad existe y no es de solo lectura
                                    if (property != null && property.CanWrite)
                                    {
                                        var value = reader.GetValue(i);
                                        if (value == DBNull.Value || value == null)
                                        {
                                            // Asignar valor predeterminado basado en el tipo de la propiedad
                                            if (property.PropertyType == typeof(string))
                                            {
                                                property.SetValue(obj, string.Empty, null);
                                            }
                                            else if (property.PropertyType.IsValueType)
                                            {
                                                property.SetValue(obj, Activator.CreateInstance(property.PropertyType), null);
                                            }
                                            else
                                            {
                                                property.SetValue(obj, null, null);
                                            }
                                        }
                                        else if (property.PropertyType.IsEnum)
                                        {
                                            //property.SetValue(obj, Enum.Parse(property.PropertyType, reader.GetValue(i)?.ToString()), null);
                                            var enumValue = reader.GetValue(i)?.ToString();
                                            if (Enum.TryParse(property.PropertyType, enumValue, out var parsedValue))
                                            {
                                                property.SetValue(obj, parsedValue, null);
                                            }
                                            else
                                            {
                                                // Manejo del error: el valor no es válido para la enumeración
                                                throw new ArgumentException($"El valor '{enumValue}' no es válido para la enumeración '{property.PropertyType.Name}'.");
                                            }
                                            //return Enum.ToObject(property.PropertyType, obj);
                                        }
                                        else
                                        {
                                            property.SetValue(obj, Convert.ChangeType(reader.GetValue(i), property.PropertyType), null);
                                        }
                                        // Asignar el valor a la propiedad del objeto
                                        
                                    }

                                }
                                lista.Add(obj);
                            }
                        }
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

            return lista;
        }
        #endregion
    }
}
