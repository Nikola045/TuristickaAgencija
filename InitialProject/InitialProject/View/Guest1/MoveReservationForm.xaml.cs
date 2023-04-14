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
using System.Xml.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Repository;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for MoveReservationForm.xaml
    /// </summary>
    public partial class MoveReservationForm : Window
    {
        ReservationService reservationService;
        MoveReservationRepository moveReservationRepository;
        ReservationRepository reservationRepository;
        HotelService hotelService;
        User LogedUser { get; set; }
        public MoveReservationForm(User user)
        {
            InitializeComponent();
            reservationService = new ReservationService();
            moveReservationRepository = new MoveReservationRepository();
            reservationRepository = new ReservationRepository();
            hotelService = new HotelService();
            LogedUser = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Reservation SelectedReservation = (Reservation) DataPanel.SelectedItem;
            if(SelectedReservation != null)
            {
                MoveReservation newRequest = new MoveReservation(
                SelectedReservation.Id,
                SelectedReservation.HotelName,
                LogedUser.Username,
                SelectedReservation.StartDate,
                SelectedReservation.EndDate,
                Convert.ToDateTime(NewStartDate.Text),
                Convert.ToDateTime(NewEndDate.Text));
                moveReservationRepository.Save(newRequest);
                MessageBox.Show("Request accepted");
            }          
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = reservationService.FindReservationByGuestUsername(LogedUser.Username);
            DataPanel.ItemsSource = reservations;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            Reservation SelectedReservation = (Reservation)DataPanel.SelectedItem;
            if(SelectedReservation != null)
            {
                Hotel hotel = hotelService.GetHotelByName(SelectedReservation.HotelName);
                Reservation findedReservation = reservationService.FindReservationByID(SelectedReservation.Id);
                if(hotel.NumberOfDaysToCancel <  findedReservation.StartDate.Day - dateTime.Day)
                {
                    findedReservation.GradeStatus = "Canceled";
                    reservationRepository.Update(findedReservation);
                }
                List<Reservation> reservations = reservationService.FindReservationByGuestUsername(LogedUser.Username);
                DataPanel.ItemsSource = reservations;
            }
            else { }
        }
    }
}
