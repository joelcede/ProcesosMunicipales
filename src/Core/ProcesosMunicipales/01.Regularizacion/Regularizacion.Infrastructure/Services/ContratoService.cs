using iText.Kernel.Pdf.Function;
using Regularizacion.Application.Interfaces;
using Regularizacion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Infrastructure.Services
{
    public static class ContratoService
    {
     
        public static byte[] obtenerContratoByte(string filePath, ContratoDefaultDomain value)
        {
            PdfProcessingService pdf = new PdfProcessingService(filePath);
            pdf.AsignarTituloDocumento($"CONTRATO-{value.nombrePropietario}-{value.dni}");
            pdf.ReemplazarTextoConImagen("@Foto", Convert.ToBase64String(value.imagenV) );
            pdf.ReemplazarTexto("@FechaActual", obtenerFechaActual());
            pdf.ReemplazarTexto("@NombrePropietario", value.nombrePropietario);
            pdf.ReemplazarTexto("@DireccionPropiedad", value.direcionVivienda);
            pdf.ReemplazarTexto("@ValorReg ", convertMonedaUS(value.valorReg));
            pdf.ReemplazarTexto("@ValorMitadReg", convertMonedaUS(value.valorMitadReg));
            pdf.ReemplazarTexto("@DNI", value.dni); 
            var response = pdf.getPdftoBytes();
            return response;
        }
        public static byte[] ObtenerContratoItext7(string filePath, ContratoDefaultDomain value)
        {
            string htmlContent = File.ReadAllText(filePath);
            if (htmlContent.Contains("@NombrePropietario"))
            {
                var xd = htmlContent.Length;
            }
            htmlContent.Replace("@Foto", $"data:image/png;base64,{value.imagenV}");
            htmlContent.Replace("@NombrePropietario", "putademierdaxd");

            var nuevo = htmlContent;
            return new byte[0];
        }
        private static string convertMonedaUS(decimal valor)
        {
            var response = valor.ToString("C", new CultureInfo("en-US"));
            return response;
        }
        private static string obtenerFechaActual()
        {
            DateTime fechaActual = DateTime.Now;
            CultureInfo culturaEspañol = new CultureInfo("es-ES");
            string formatoPersonalizado = "dddd dd 'de' MMMM 'del' yyyy";

            string fechaActualFormateada = fechaActual.ToString(formatoPersonalizado, culturaEspañol);

            // Capitaliza la primera letra del día de la semana
            fechaActualFormateada = char.ToUpper(fechaActualFormateada[0]) + fechaActualFormateada.Substring(1);
            return fechaActualFormateada;
        }
    }
}
