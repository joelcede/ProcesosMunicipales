using Commons.Connection;
using Commons.logger;
using Microsoft.Extensions.Configuration;
using Regularizacion.Application.Mapper;
using Regularizacion.Application.Repository;
using Regularizacion.Domain.Entities;
using Regularizacion.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.IO.Image.Jpeg2000ImageData;

namespace Regularizacion.Infrastructure.Repository
{
    public class GraphicsRepository : IGraphicsRepository
    {
        private readonly IConfiguration _connectionString;
        private readonly RegularizacionMapper _regularizacionMapper;
        public static string _clase = string.Empty;
        public Logger _logger;

        #region Nombres de procedimientos almacenados
        public const string SP_GRAFICOS = "SP_GRAFICOS";
        #endregion

        public GraphicsRepository(IConfiguration configuration)
        {
            _connectionString = configuration;
            _logger = new Logger(configuration);
            _regularizacionMapper = new RegularizacionMapper();
            _clase = this.GetType().Name;
        }

        private Dictionary<string, object> GetResultGrpahip1(int param)
        {
            var parameters = new Dictionary<string, object>()
            {
                {"@Trx", param }
            };
            return parameters;
        }
        public async Task<IEnumerable<CantidadRegMesDomain>> obtenerGraficaRegMes()
        {
            _logger.LogInicio(_clase);
            var grap = GetResultGrpahip1(1);
            var response = await new Database(_connectionString).ExecuteReaderAsync<CantidadRegMesDomain>(SP_GRAFICOS, grap);
            _logger.LogFin(_clase);
            return response;
        }

        public async Task<IEnumerable<EstadosMesDomain>> obtenerGananciaRegMes()
        {
            _logger.LogInicio(_clase);
            var grap = GetResultGrpahip1(2);
            var response = await new Database(_connectionString).ExecuteReaderAsync<EstadosMesDomain>(SP_GRAFICOS, grap);
            _logger.LogFin(_clase);
            return response;
        }
    }
}
