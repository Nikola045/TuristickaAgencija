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
using TravelAgency.Domain.Model;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for RenovationReview.xaml
    /// </summary>
    public partial class RenovationReview : Page
    {
        ReservationService reservationService;
        public RenovationReview()
        {
            reservationService = new ReservationService();
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            List<RenovationRequest> renovationRequests = new List<RenovationRequest>();
            renovationRequests = reservationService.ShowAllRenovationForOwnerHotels();
            DataPanel.ItemsSource = renovationRequests;
        }

        private void CancelRenovation(object sender, RoutedEventArgs e)
        {
            RenovationRequest renovationRequest = (RenovationRequest)DataPanel.SelectedItem;
            reservationService.CancelRenovation(renovationRequest);
            DataPanel.ItemsSource = reservationService.ShowAllRenovationForOwnerHotels();
        }
    }
}
