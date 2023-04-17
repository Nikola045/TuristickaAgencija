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
using TravelAgency.Domain.Model;
using TravelAgency.Repository;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for TourReview.xaml
    /// </summary>
    public partial class TourReview : Window
    {
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
