﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace TravelAgency.View.Owner
{
    public partial class StatisticPdf : Page
    {
        public StatisticPdf()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            /*string pdfPath = "../../../Resources/Data/statistic.pdf";
            PdfWriter writer = new PdfWriter(pdfPath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new paragraph("HEADER")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);

            document.Add(header);
            document.Close();*/
        }
    }
}
