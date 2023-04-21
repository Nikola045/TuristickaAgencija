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
    /// Interaction logic for MoveReservationPage.xaml
    /// </summary>
    public partial class MoveReservationPage : Page
    {
        private readonly App app = (App)App.Current;
        private readonly MoveReservationRepository moveReservationRepository;
        private readonly ReservationService reservationService;
        public MoveReservation SelectedReservation { get; set; }
        public MoveReservationPage()
        {
            moveReservationRepository = app.MoveReservationRepository;
            reservationService = new ReservationService();
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            DataPanel.ItemsSource = moveReservationRepository.GetAll();
        }

        private void AcceptMoveReservation(object sender, RoutedEventArgs e)
        {
                SelectedReservation = (MoveReservation)DataPanel.SelectedItem;
                reservationService.MoveReservation(SelectedReservation.ReservationId, SelectedReservation.NewStartDate, SelectedReservation.NewEndDate);
                DataPanel.ItemsSource = moveReservationRepository.GetAll();    
        }

        private void DeclineMoveReservation(object sender, RoutedEventArgs e)
        {
                SelectedReservation = (MoveReservation)DataPanel.SelectedItem;
                moveReservationRepository.Delete(SelectedReservation);
                DataPanel.ItemsSource = moveReservationRepository.GetAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedReservation = (MoveReservation)DataPanel.SelectedItem;
            ReservationInfoLabel.Content = reservationService.TextForReservationInfo(SelectedReservation.ReservationId, SelectedReservation.HotelName, SelectedReservation.NewStartDate, SelectedReservation.NewEndDate);
        }
    }
}

