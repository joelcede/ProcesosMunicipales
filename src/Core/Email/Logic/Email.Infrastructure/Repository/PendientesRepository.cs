﻿using Commons.Connection;
using Commons.Cryptography;
using Commons.EmailService;
using Commons.logger;
using Microsoft.Extensions.Configuration;
using Service.Email.Application.Repository;
using Service.Email.Domain.Entities;
using Service.Email.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Email.Infrastructure.Repository
{
    public class PendientesRepository: IPendientesRepository
    {
        private readonly IConfiguration _connectionString;
        public static string _clase = string.Empty;
        public static string SolicitudAprobada = "Solicitud de Regularización Preaprobada y pendiente de pago";
        public static string SolicitudIngresada = "Solicitud de Regularización Ingresada";
        public static string SolicitudSubsanacion = "INGRESO DE SUBSANACION";
        public static string SolicitudNegada = "Solicitud de Regularización  Negada";
        public Logger _logger;

        #region Nombres de procedimientos almacenados
        public const string SP_SERVICE_CORREO = "SP_SERVICE_CORREO";
        #endregion

        public PendientesRepository(IConfiguration configuration)
        {
            _connectionString = configuration;
            _logger = new Logger(configuration);
            _clase = this.GetType().Name;
        }

        private Dictionary<string, object> getCredenciales(int parametro, int estado )
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@Trx", parametro },
                { "@Estado", estado }
            };

            return parameters;
        }
        private Dictionary<string, object> getCredenciales2(int parametro, int estado , int estado2)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@Trx", parametro },
                { "@Estado", estado },
                { "@Estado2", estado2 }
            };

            return parameters;
        }
        private Dictionary<string, object> getCredenciales3(int parametro, int estado, int estado2, int estado3)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@Trx", parametro },
                { "@Estado", estado },
                { "@Estado2", estado2 },
                { "@Estado3", estado3 }
            };

            return parameters;
        }
        private Dictionary<string, object> updateEstado(int parametro, Guid idRegularizacion, int estado)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@Trx", parametro },
                { "@IdReg", idRegularizacion },
                { "@Estado", estado }
            };

            return parameters;
        }
        private Dictionary<string, object> updateCantIntentosR(int parametro, Guid idRegularizacion, int intentos)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@Trx", parametro },
                { "@IdReg", idRegularizacion },
                { "@IntentosSub", intentos }
            };

            return parameters;
        }
        private Dictionary<string, object> updateCredentialIncorect(int parametro, Guid idRegularizacion)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@Trx", parametro },
                { "@IdReg", idRegularizacion }
            };

            return parameters;
        }
        public async Task CambiarEstadoRegularizacion()
        {
            _logger.LogInicio(_clase);
            await CambiarEstadoAprobado();
            await CambiarEstadoAprobadoDeudor();
            await CambiarEstadoTerminada();
            await CambiarEstadoEnEspera();
            await cambiarEstadoEnSubsanacion();
            await cambiarEstadoNegado();
            await CambiarEstadoVueltaASubir();
            _logger.LogFin(_clase);
        }

        public async Task CambiarEstadoEnEspera()
        {
            //obtienes los correo por hacer y los cambia a estado pendiente
            _logger.LogInicio(_clase);
            var IdRegularizacion = Guid.NewGuid();
            try
            {
                var parametrosG = getCredenciales(1, (int)EstadoType.PorHacer);
                var correosPorHacer = await new Database(_connectionString).ExecuteReaderAsync<Credencial>(SP_SERVICE_CORREO, parametrosG);

                foreach (var reg in correosPorHacer)
                {
                    RevisarCorreo revisarCorreo = new RevisarCorreo();
                    IdRegularizacion = reg.Id;
                    var correoObtenido = await revisarCorreo.obtenerCorreosEnProceso(reg.Correo, EncryptionHelper.DecryptString(reg.Contrasena), SolicitudIngresada);

                    if (correoObtenido.Count() > 0)
                    {
                        var parametrosU = updateEstado(2, reg.Id, (int)EstadoType.EnEspera);
                        await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosU);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"REG:  {IdRegularizacion} - {ex.Message}");
                var parametrosUpd = updateCredentialIncorect(6, IdRegularizacion);
                await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosUpd);
            }


            _logger.LogFin(_clase);
        }

        public async Task cambiarEstadoEnSubsanacion()
        {
            _logger.LogInicio(_clase);
            var IdRegularizacion = Guid.NewGuid();
            try
            {
                var parametrosG = getCredenciales2(1, (int)EstadoType.EnEspera, (int)EstadoType.EnEsperaVueltaASubir);
                var correosPorHacer = await new Database(_connectionString).ExecuteReaderAsync<Credencial>(SP_SERVICE_CORREO, parametrosG);

                foreach (var reg in correosPorHacer)
                {
                    RevisarCorreo revisarCorreo = new RevisarCorreo();
                    IdRegularizacion = reg.Id;
                    var correoObtenido = await revisarCorreo.obtenerCorreosEnProceso(reg.Correo, EncryptionHelper.DecryptString(reg.Contrasena), SolicitudSubsanacion);

                    if (correoObtenido.Count() > 0)
                    {
                        var parametrosU = updateEstado(2, reg.Id, (int)EstadoType.SubSanacion);
                        await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosU);

                        int cantidadSubsanacion = reg.IntentosSubsanacion + 1;
                        var parametrosUS = updateCantIntentosR(3, reg.Id, cantidadSubsanacion);
                        await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosUS);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"REG:  {IdRegularizacion} - {ex.Message}");
                var parametrosUpd = updateCredentialIncorect(6, IdRegularizacion);
                await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosUpd);
            }

            _logger.LogFin(_clase);
        }

        public async Task cambiarEstadoNegado()
        {
            _logger.LogInicio(_clase);
            var IdRegularizacion = Guid.NewGuid();
            try
            {
                var parametrosG = getCredenciales3(1, (int)EstadoType.EnEspera, (int)EstadoType.SubSanacion, (int)EstadoType.EnEsperaVueltaASubir);
                var correosPorHacer = await new Database(_connectionString).ExecuteReaderAsync<Credencial>(SP_SERVICE_CORREO, parametrosG);

                foreach (var reg in correosPorHacer)
                {
                    RevisarCorreo revisarCorreo = new RevisarCorreo();
                    IdRegularizacion = reg.Id;
                    var correoObtenido = await revisarCorreo.obtenerCorreosEnProceso(reg.Correo, EncryptionHelper.DecryptString(reg.Contrasena), SolicitudNegada);

                    if (correoObtenido.Count() > 0)
                    {
                        var parametrosU = updateEstado(2, reg.Id, (int)EstadoType.Negada);
                        await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosU);

                        int cantidadNegada = reg.cantidadNegada + 1;
                        var parametrosUS = updateCantIntentosR(3, reg.Id, cantidadNegada);
                        await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosUS);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"REG: {IdRegularizacion} - {ex.Message}");
                var parametrosUpd = updateCredentialIncorect(6, IdRegularizacion);
                await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosUpd);
            }

            

            _logger.LogFin(_clase);
        }

        public async Task CambiarEstadoVueltaASubir()
        {
            _logger.LogInicio(_clase);
            var IdRegularizacion = Guid.NewGuid();
            try
            {
                var parametrosG = getCredenciales(1, (int)EstadoType.Negada);
                var correosPorHacer = await new Database(_connectionString).ExecuteReaderAsync<Credencial>(SP_SERVICE_CORREO, parametrosG);

                foreach (var reg in correosPorHacer)
                {
                    RevisarCorreo revisarCorreo = new RevisarCorreo();
                    IdRegularizacion = reg.Id;
                    var correoIngreso = await revisarCorreo.obtenerCorreosEnProceso(reg.Correo, EncryptionHelper.DecryptString(reg.Contrasena), SolicitudIngresada);
                    var correoNegado = await revisarCorreo.obtenerCorreosEnProceso(reg.Correo, EncryptionHelper.DecryptString(reg.Contrasena), SolicitudNegada);

                    if (correoIngreso.Count() >= 2 && correoNegado.Count() > 0)
                    {
                        var parametrosU = updateEstado(2, reg.Id, (int)EstadoType.Negada);
                        await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosU);

                        int cantidadIntentos = reg.IntentosSubsanacion + 1;
                        var parametrosUS = updateCantIntentosR(3, reg.Id, cantidadIntentos);
                        await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosUS);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"REG: {IdRegularizacion} - {ex.Message}");
                var parametrosUpd = updateCredentialIncorect(6, IdRegularizacion);
                await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosUpd);
            }
            

            _logger.LogFin(_clase);
        }

        public async Task CambiarEstadoAprobado()
        {
            _logger.LogInicio(_clase);
            var IdRegularizacion = Guid.NewGuid();
            try
            {
                var parametrosG = getCredenciales3(1, (int)EstadoType.EnEspera, (int)EstadoType.SubSanacion, (int)EstadoType.EnEsperaVueltaASubir);
                var correosPorAprobar = await new Database(_connectionString).ExecuteReaderAsync<Credencial>(SP_SERVICE_CORREO, parametrosG);

                foreach (var reg in correosPorAprobar)
                {
                    RevisarCorreo revisarCorreo = new RevisarCorreo();
                    IdRegularizacion = reg.Id;
                    var correoAprobado = await revisarCorreo.obtenerCorreosEnProceso(reg.Correo, EncryptionHelper.DecryptString(reg.Contrasena), SolicitudAprobada);

                    if (correoAprobado.Count() > 0)
                    {
                        var parametrosU = updateEstado(2, reg.Id, (int)EstadoType.Aprobada);
                        await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosU);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"REG: {IdRegularizacion} - {ex.Message}");
                var parametrosUpd = updateCredentialIncorect(6, IdRegularizacion);
                await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosUpd);
            }

            _logger.LogFin(_clase);
        }   

        public async Task CambiarEstadoAprobadoDeudor()
        {
            _logger.LogInicio(_clase);

            var parametrosG = getCredenciales(1, (int)EstadoType.Aprobada);
            var regularizaciones = await new Database(_connectionString).ExecuteReaderAsync<Credencial>(SP_SERVICE_CORREO, parametrosG);
            
            foreach (var reg in regularizaciones)
            {
                var parametrosU = updateEstado(4, reg.Id, (int)EstadoType.TerminadaPendiente);
                await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosU);
            }

            _logger.LogFin(_clase);
        }

        public async Task CambiarEstadoTerminada()
        {
            _logger.LogInicio(_clase);

            var parametrosG = getCredenciales2(1, (int)EstadoType.Aprobada, (int)EstadoType.TerminadaPendiente);
            var regularizacionesPP = await new Database(_connectionString).ExecuteReaderAsync<Credencial>(SP_SERVICE_CORREO, parametrosG);

            foreach (var reg in regularizacionesPP)
            {
                var parametrosU = updateEstado(5, reg.Id, (int)EstadoType.Terminada);
                await new Database(_connectionString).ExecuteNonQueryAsync(SP_SERVICE_CORREO, parametrosU);
            }

            _logger.LogFin(_clase);
        }
    }
}
