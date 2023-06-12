using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using System.Xml.Linq;
using DevExpress.Xpo.Logger;
using DevExpress.XtraEditors.Filtering;
using Microsoft.Graph.Models.Security;
using Microsoft.Kiota.Abstractions;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for MoveReservationForm.xaml
    /// </summary>
    public partial class MoveReservationForm : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        ReservationService reservationService;
        MoveReservationRepository moveReservationRepository;
        ReservationRepository reservationRepository;
        HotelService hotelService;
        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation SelectedReservation { get; set; }
        User LogedUser { get; set; }
        public MoveReservationForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            reservationService = new ReservationService();
            moveReservationRepository = new(InjectorService.CreateInstance<IStorage<MoveReservation>>());
            reservationRepository = new(InjectorService.CreateInstance<IStorage<Reservation>>());
            hotelService = new HotelService();
            LogedUser = user;
            Reservations = new ObservableCollection<Reservation>(reservationService.FindReservationByGuestUsername(LogedUser.Username));
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null && AllFieldsValid())
            {
                MoveReservation newRequest = new MoveReservation(
                    SelectedReservation,
                    SelectedReservation.Hotel.Name,
                    LogedUser.Username,
                    SelectedReservation.StartDate,
                    SelectedReservation.EndDate,
                    Convert.ToDateTime(NewStartDate.Text),
                    Convert.ToDateTime(NewEndDate.Text)
                );
                moveReservationRepository.Save(newRequest);
                MoveReservationRequestList page = new MoveReservationRequestList(LogedUser);
                NavigationService.Navigate(page);
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            if (SelectedReservation != null)
            {
                Hotel hotel = hotelService.GetHotelByName(SelectedReservation.Hotel.Name);
                Reservation findedReservation = reservationService.FindReservationByID(SelectedReservation.Id);
                int daysUntilCheckin = (int)(findedReservation.StartDate - dateTime).TotalDays;

                if (hotel.NumberOfDaysToCancel <= daysUntilCheckin)
                {
                    findedReservation.GradeStatus = "Canceled";
                    reservationRepository.Update(findedReservation);
                    SucessfullyCanceled page = new SucessfullyCanceled(LogedUser);
                    NavigationService.Navigate(page);
                }
                else
                {
                    NotSucessfullyCanceled page = new NotSucessfullyCanceled(LogedUser);
                    NavigationService.Navigate(page);
                }
                List<Reservation> reservations = reservationService.FindReservationByGuestUsername(LogedUser.Username);
            }
        }
        private void NewStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime today = DateTime.Now.Date;
            if (NewStartDate.SelectedDate.HasValue && NewStartDate.SelectedDate.Value.Date < today)
            {
                if (NewStartDate.SelectedDate.Value.Date != today)
                {
                }
                NewStartDate.SelectedDate = today;
            }
            btnRequest.IsEnabled = AllFieldsValid();
        }
        private void NewEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime today = DateTime.Now.Date;
            if (NewEndDate.SelectedDate.HasValue && NewEndDate.SelectedDate.Value.Date < today)
            {
                if (NewEndDate.SelectedDate.Value.Date != today)
                {
                }
                NewEndDate.SelectedDate = today;
            }
            btnRequest.IsEnabled = AllFieldsValid();
        }
        private bool AllFieldsValid()
        {
            if (NewStartDate.SelectedDate != null && NewEndDate.SelectedDate != null && SelectedReservation != null)
            {
                if (NewStartDate.SelectedDate >= NewEndDate.SelectedDate)
                {
                    MessageBox.Show("Start date must be before end date.");
                    return false;
                }
                else
                {
                    if (SelectedReservation.StartDate <= Convert.ToDateTime(NewStartDate.Text) && SelectedReservation.EndDate >= Convert.ToDateTime(NewEndDate.Text))
                    {
                        MessageBox.Show("The selected date range is already booked.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        private void DataPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                btnRequest.IsEnabled = AllFieldsValid();
                CancelReservation.IsEnabled = true;
            }
            else
            {
                btnRequest.IsEnabled = false;
                CancelReservation.IsEnabled = false;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MoveReservationRequestList page = new MoveReservationRequestList(LogedUser);
            NavigationService.Navigate(page);
        }
    }
}
