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
using System.Windows.Shapes;
using TravelAgency.Forms;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for Guest2Form.xaml
    /// </summary>
    public partial class Guest2Form : Window
    {
        public Guest2Form()
        {
            InitializeComponent();
        }

        private void OpenGuest2Form(object sender, RoutedEventArgs e)
        {
            Forms.Guest2Form createGuest2Form = new Forms.Guest2Form();
            createGuest2Form.Show();
        }
    }
}
