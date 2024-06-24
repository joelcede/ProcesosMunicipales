using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regularizacion.Application.Interfaces
{
    public interface IPdfProcessingService
    {
        Task<string> ConvertPdfToHtmlAsync(string filePath);
    }
}
