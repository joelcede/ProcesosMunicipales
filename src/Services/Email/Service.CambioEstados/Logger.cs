using Microsoft.Extensions.Configuration;
using Serilog;
using System.Runtime.CompilerServices;

namespace Service.CambioEstados
{
    public static class Logger
    {
        public static readonly Serilog.ILogger _log;
        private static bool _isConfigured = false;
        static Logger()
        {
            if (!_isConfigured && !Log.Logger.GetType().Equals(typeof(LoggerConfiguration)))
            {
                _log = new LoggerConfiguration()
                    .ReadFrom.AppSettings()
                    .CreateLogger();

                _isConfigured = true;
            }
        }

        public static void LogError(string clase, string message = "", [CallerMemberName] string metodo = "") => _log.Error($"Exception | {clase} - {metodo}:\n{message}");

        public static void LogErrorSQL(string clase, string message = "", [CallerMemberName] string metodo = "") => _log.Warning($"SqlException | {clase} - {metodo}:\n{message}");

        public static void LogInicio(string clase, [CallerMemberName] string metodo = "") => _log.Information($"INICIO | {clase} - {metodo}");

        public static void LogFin(string clase, [CallerMemberName] string metodo = "") => _log.Information($"FIN | {clase} - {metodo}");

        public static void LogInformation(string clase, [CallerMemberName] string metodo = "", string message = "") => _log.Information($"INFORMATION | {clase} - {metodo}:\n{message}");

    }
}
