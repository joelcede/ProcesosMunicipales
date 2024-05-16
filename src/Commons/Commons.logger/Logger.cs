using Microsoft.Extensions.Configuration;
using Serilog;
using System.Runtime.CompilerServices;

namespace Commons.logger
{
    public class Logger
    {
        private static bool _isConfigured = false;
        public Logger(IConfiguration configuration)
        {
            if (!_isConfigured && !Log.Logger.GetType().Equals(typeof(LoggerConfiguration)))
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                _isConfigured = true;
            }
        }

        public void LogError(string clase, string message = "", [CallerMemberName] string metodo = "") => Log.Error($"Exception | {clase} - {metodo}:\n{message}");

        public void LogErrorSQL(string clase, string message = "", [CallerMemberName] string metodo = "") => Log.Warning($"SqlException | {clase} - {metodo}:\n{message}");

        public void LogInicio(string clase, [CallerMemberName] string metodo = "") => Log.Information($"INICIO | {clase} - {metodo}");

        public void LogFin(string clase, [CallerMemberName] string metodo = "") => Log.Information($"FIN | {clase} - {metodo}");

        public void LogInformation(string clase, [CallerMemberName] string metodo = "", string message = "") => Log.Information($"INFORMATION | {clase} - {metodo}:\n{message}");

    }
}
