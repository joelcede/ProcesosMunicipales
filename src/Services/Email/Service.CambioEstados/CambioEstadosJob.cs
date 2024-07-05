using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.CambioEstados
{
    public class CambioEstadosJob: IJob
    {
        public static string _clase = string.Empty;
        static readonly Serilog.ILogger _logger = Logger._log;
        public CambioEstadosJob() 
        {
            _clase = this.GetType().Name;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            Logger.LogInicio(_clase);
            //_logger.Error($"CambioEstadosJob - Execute {new DateTime()}");
            //_logger..LogInicio(_clase);
            using (HttpClient client = new HttpClient())
            {
                
                try
                {
                    Logger.LogInformation(_clase, message: "Comienzo del try Using HttpClient");
                    string endpoint = ConfigurationManager.AppSettings["Endpoint"];
                    var content = new StringContent("", Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(endpoint, content);

                    response.EnsureSuccessStatusCode();

                    //response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody);
                    Logger.LogInformation(_clase, message: "Fin del try Using HttpClient");
                }
                catch (HttpRequestException e)
                {
                    Logger.LogError(_clase, $"HttpRequestException:\n {e.Message}");
                    //_logger.Error($"CambioEstadosJob - Execute {new DateTime()} : {e.Message}");
                    //Console.WriteLine($"Error en la solicitud HTTP: {e.Message}");
                }
            }
            Logger.LogFin(_clase);
        }
    }
}
