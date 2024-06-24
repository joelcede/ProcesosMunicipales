using Aspose.Pdf.Text;
using Aspose.Pdf;
using System.IO;
using System;
using iText.Forms.Form.Element;
using iText.Kernel.Pdf;

namespace Regularizacion.Infrastructure.Services
{
    public class PdfProcessingService
    {

        private Document _pdfDocument;

        public PdfProcessingService(string filePath)
        {
            _pdfDocument = new Document(filePath);
        }
        public void AsignarTituloDocumento(string titulo)
        {
            _pdfDocument.Info.Title = titulo;
        }
        public void ReemplazarTexto(string textoAReemplazar, string nuevoTexto)
        {
            foreach (Page page in _pdfDocument.Pages)
            {
                TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(textoAReemplazar);
                page.Accept(textFragmentAbsorber);

                TextFragmentCollection textFragmentCollection = textFragmentAbsorber.TextFragments;
                foreach (TextFragment textFragment in textFragmentCollection)
                {
                    textFragment.Text = nuevoTexto;
                }
            }
        }

        public void ReemplazarTextoConImagen(string textoAReemplazar, string imagenBase64)
        {
            byte[] imageBytes = Convert.FromBase64String(imagenBase64.Replace("data:image/png;base64,", ""));
            MemoryStream imageStream = new MemoryStream(imageBytes);

            foreach (Page page in _pdfDocument.Pages)
            {
                TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(textoAReemplazar);
                page.Accept(textFragmentAbsorber);

                TextFragmentCollection textFragmentCollection = textFragmentAbsorber.TextFragments;
                foreach (TextFragment textFragment in textFragmentCollection)
                {
                    Rectangle rect = textFragment.Rectangle;

                    // Borrar el texto original
                    foreach (TextSegment segment in textFragment.Segments)
                    {
                        segment.Text = "";
                    }

                    // Crear la imagen y agregarla en la posición del texto original
                    Aspose.Pdf.Image image = new Aspose.Pdf.Image
                    {
                        ImageStream = imageStream,
                        FixHeight = 120,
                        FixWidth = 120,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                    };

                    // Ajustar la posición de la imagen usando matrix
                    page.Paragraphs.Add(image);
                    image.IsInLineParagraph = true;
                    image.Margin = new MarginInfo { Left = -25, Bottom = 0, Top = 45 };
                    //image.Margin = new MarginInfo(0, rect.URY, rect.LLX, rect.LLY);
                    //{ Left = rect.LLX, Bottom = page.Rect.Height - rect.URY };
                }
            }
        }

        public byte[] getPdftoBytes()
        {
            TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber("Evaluation Only. Created with Aspose.PDF. Copyright 2002-2024 Aspose Pty Ltd.");

            // Aceptar el visitante para todas las páginas del documento PDF
            _pdfDocument.Pages.Accept(textFragmentAbsorber);

            // Obtener la colección de fragmentos de texto encontrados
            TextFragmentCollection textFragmentCollection = textFragmentAbsorber.TextFragments;

            // Cambiar el color del texto encontrado
            foreach (TextFragment textFragment in textFragmentCollection)
            {
                // Cambiar el color de la fuente a rojo (o cualquier color que desees)
                textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black);
            }
            using (MemoryStream stream = new MemoryStream())
            {
                _pdfDocument.Save(stream);
                return stream.ToArray();
            }
        }
    }
}
