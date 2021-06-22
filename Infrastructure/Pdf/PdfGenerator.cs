using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Kaizen.Core.Pdf;

namespace Kaizen.Infrastructure.Pdf
{
    public class PdfGenerator : IPdfGenerator
    {
        public MemoryStream GenerateCertificate(int certificateId, string tradeName, string nit,
            DateTime applicationDate,
            string serviceNames)
        {
            var stream = new MemoryStream();
            var pdfWriter = new PdfWriter(stream);
            pdfWriter.SetCloseStream(false);
            var pdfDocument = new PdfDocument(pdfWriter);
            var document = new Document(pdfDocument);

            document.Add(new Paragraph(certificateId.ToString()));
            document.Add(new Paragraph(tradeName));
            if (nit != null)
            {
                document.Add(new Paragraph(nit));
            }
            document.Add(new Paragraph(applicationDate.ToString("d")));
            document.Add(new Paragraph(serviceNames));

            document.Close();

            return stream;
        }
    }
}
