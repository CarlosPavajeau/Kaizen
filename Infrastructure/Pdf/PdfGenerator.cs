using System;
using System.Globalization;
using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Colorspace;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Kaizen.Core.Pdf;

namespace Kaizen.Infrastructure.Pdf
{
    public class PdfGenerator : IPdfGenerator
    {
        private static void MakeCertificateHeader(Document document)
        {
        }

        private static void MakeCertificateTitle(Document document, int certificateId)
        {
            var title = new Paragraph($"Certificado N° {certificateId:0000}");
            title.SetTextAlignment(TextAlignment.CENTER);
            title.SetFontSize(20);
            title.SetBold();

            document.Add(title);
        }

        private static void MakeCompanyInfo(Document document)
        {
            var companyInfo = new Paragraph("LA SUSCRITA GERENTE DE ECOLPLAG S.A.S.\nNIT: 900.996.037-0");
            companyInfo.SetTextAlignment(TextAlignment.CENTER);
            companyInfo.SetFontSize(18);
            companyInfo.SetMarginTop(30);
            companyInfo.SetMarginBottom(20);
            companyInfo.SetBold();

            document.Add(companyInfo);
        }

        private static void MakeCertificateInfo(Document document, string tradeName, string nit,
            DateTime applicationDate, string serviceNames)
        {
            var title = new Paragraph("CERTIFICA");
            title.SetTextAlignment(TextAlignment.CENTER);
            title.SetFontSize(18);
            title.SetBold();
            title.SetMarginTop(20);
            document.Add(title);

            var firstPart = new Paragraph();
            firstPart.SetMarginTop(20);
            firstPart.SetTextAlignment(TextAlignment.JUSTIFIED);
            firstPart.Add($"Que la entidad ");

            var tradeNameText = new Text(tradeName);
            tradeNameText.SetBold();
            firstPart.Add(tradeNameText);

            if (!string.IsNullOrEmpty(nit))
            {
                firstPart.Add(", identificada con ");
                var nitText = new Text($"NIT. {nit}");
                nitText.SetBold();
                firstPart.Add(nitText);
            }

            firstPart.Add($", en la fecha " +
                          $"{applicationDate.ToString("dd 'de' MMMM 'del' yyyy", CultureInfo.CreateSpecificCulture("es-co"))}");

            firstPart.Add($", realizó los servicios de: {serviceNames}.");
            document.Add(firstPart);

            var secondPart =
                new Paragraph(
                    "Nuestros servicios se hacen de acuerdo con la Ley 99 de 1993, REGLAMENTO TÉCNICO DEL SECTOR " +
                    "DE AGUA Y SANEAMIENTO BÁSICO RAS 2000, SECCIÓN II, TITULO E, Decreto 3930 y Resolución " +
                    "0631 de 2015.");
            secondPart.SetTextAlignment(TextAlignment.JUSTIFIED);
            document.Add(secondPart);

            var thirdPart = new Paragraph(
                "Esta certificación cumple con las exigencias de los entes de control y promotores de saneamiento, y," +
                " tiene validez por 180 días contados a partir de la fecha de ejecución del servicio.");
            thirdPart.SetTextAlignment(TextAlignment.JUSTIFIED);
            document.Add(thirdPart);


            var fourthPart =
                new Paragraph(
                    $"Dada en Valledupar, a los " +
                    $"{DateTime.Now.ToString("dd 'dias' 'del mes de ' MMMM 'de' yyyy.", CultureInfo.CreateSpecificCulture("es-co"))}");
            fourthPart.SetMarginTop(40);
            document.Add(fourthPart);
        }

        private class PdfFooter : IEventHandler
        {
            private const float Side = 20;
            private const float X = 300;
            private const float Y = 25;
            private const float Space = 4.5f;
            private const float Descent = 3;

            public void HandleEvent(Event @event)
            {
                var documentEvent = (PdfDocumentEvent) @event;
                var pageSize = documentEvent.GetPage().GetPageSize();

                var pdfCanvas = new PdfCanvas(documentEvent.GetPage());
                var canvas = new Canvas(pdfCanvas, pageSize);
                canvas.SetFontSize(10);
                canvas.SetFontColor(ColorConstants.GRAY);

                var text = new Paragraph("Calle 6C N° 19-46 Barrio Los Músicos. Télefonos: (5) 5711106 - 5842747 " +
                                         "Celulares: 3104538739 - 3168743205\nE-mail: valledupar@ecolplag.com.co - www.ecolplag.com.co\n" +
                                         "Valledupar - Cesar");

                canvas.ShowTextAligned(text, X, Y, TextAlignment.CENTER);
                canvas.Close();

                var placeholder = new PdfFormXObject(new Rectangle(0, 0, Side, Side));
                pdfCanvas.AddXObjectAt(placeholder, X + Space, Y - Descent);
                pdfCanvas.Release();
            }
        }

        public MemoryStream GenerateCertificate(int certificateId, string tradeName, string nit,
            DateTime applicationDate,
            string serviceNames)
        {
            var stream = new MemoryStream();
            var pdfWriter = new PdfWriter(stream);
            pdfWriter.SetCloseStream(false);
            var pdfDocument = new PdfDocument(pdfWriter);
            var document = new Document(pdfDocument);

            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new PdfFooter());

            MakeCertificateHeader(document);
            MakeCertificateTitle(document, certificateId);
            MakeCompanyInfo(document);
            MakeCertificateInfo(document, tradeName, nit, applicationDate, serviceNames);

            document.Close();

            return stream;
        }
    }
}
