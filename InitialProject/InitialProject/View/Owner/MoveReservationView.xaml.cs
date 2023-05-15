using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class MoveReservationPage : Page, INotifyPropertyChanged
    {
        private readonly App app = (App)App.Current;
        private readonly MoveReservationRepository moveReservationRepository;
        private readonly ReservationService reservationService;

        private string _hotelName;
        private string _guestUsername;
        private DateTime _oldStartDate;
        private DateTime _newStartDate;
        private DateTime _oldEndDate;
        private DateTime _newEndDate;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MoveReservation SelectedReservation { get; set; }
        public MoveReservationPage()
        {
            InitializeComponent();
            DataContext = this;
            moveReservationRepository = app.MoveReservationRepository;
            reservationService = new ReservationService();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            DataPanel.ItemsSource = moveReservationRepository.GetAll();
        }

        private void AcceptMoveReservation(object sender, RoutedEventArgs e)
        {
            SelectedReservation = (MoveReservation)DataPanel.SelectedItem;
            reservationService.MoveReservation(SelectedReservation.Reservation.Id, SelectedReservation.NewStartDate, SelectedReservation.NewEndDate);
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
            ReservationInfoLabel.Content = reservationService.TextForReservationInfo(SelectedReservation.Reservation.Id, SelectedReservation.HotelName, SelectedReservation.NewStartDate, SelectedReservation.NewEndDate);
        }

        public string HotelName
        {
            get => _hotelName;
            set
            {
                if (_hotelName != value)
                {
                    _hotelName = value;
                    OnPropertyChanged();
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

