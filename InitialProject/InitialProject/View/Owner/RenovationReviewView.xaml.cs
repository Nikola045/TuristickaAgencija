using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Domain.Model;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Owner
{
    public partial class RenovationReview : Page, INotifyPropertyChanged
    {
        ReservationService reservationService;
        public RenovationRequest SelectedRenovation { get; set; }
        public static ObservableCollection<RenovationRequest> Renovations { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public RenovationReview()
        {
            InitializeComponent();
            DataContext = this;
            reservationService = new ReservationService();
            Renovations = new ObservableCollection<RenovationRequest>(reservationService.ShowAllRenovationForOwnerHotels());
        }

        private void CancelRenovation(object sender, RoutedEventArgs e)
        {
            if(SelectedRenovation != null)
            {
                reservationService.CancelRenovation(SelectedRenovation);
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
