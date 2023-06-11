using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for PdfReportCanceledReservation.xaml
    /// </summary>
    public partial class PdfReportCanceledReservation : Page
    {
        public string Text { get; set; }
        public PdfReportCanceledReservation(string text)
        {
            InitializeComponent();
            Text = text;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Label.Content = Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BtnSave.Visibility = System.Windows.Visibility.Hidden;
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "pdf");
            }
        }
    }
}
