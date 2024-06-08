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
        public async Task Execute(IJobExecutionContext context)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string endpoint = ConfigurationManager.AppSettings["Endpoint"];
                    var content = new StringContent("", Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(endpoint, content);

                    response.EnsureSuccessStatusCode();

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error en la solicitud HTTP: {e.Message}");
                }
            }
        }
    }
}
