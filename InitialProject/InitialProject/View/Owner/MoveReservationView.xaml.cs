using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Owner
{
    public partial class MoveReservationPage : Page, INotifyPropertyChanged
    {
        private readonly MoveReservationRepository moveReservationRepository;
        private readonly ReservationService reservationService;
        public static ObservableCollection<MoveReservation> Reservations { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MoveReservation SelectedReservation { get; set; }
        public MoveReservationPage()
        {
            InitializeComponent();
            DataContext = this;
            moveReservationRepository = new(InjectorService.CreateInstance<IStorage<MoveReservation>>());
            reservationService = new ReservationService();
            Reservations = new ObservableCollection<MoveReservation>(moveReservationRepository.GetAll());
        }

        private void AcceptMoveReservation(object sender, RoutedEventArgs e)
        {
            if(SelectedReservation != null)
            {
                reservationService.MoveReservation(SelectedReservation.Reservation.Id, SelectedReservation.NewStartDate, SelectedReservation.NewEndDate);
                OnPropertyChanged(nameof(Reservations));

            }
        }

        private void DeclineMoveReservation(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                SelectedReservation.Status = "Declined";
                moveReservationRepository.Update(SelectedReservation);
                OnPropertyChanged(nameof(Reservations));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                ReservationInfoLabel.Content = reservationService.TextForReservationInfo(SelectedReservation.Reservation.Id, SelectedReservation.HotelName, SelectedReservation.NewStartDate, SelectedReservation.NewEndDate);

            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

