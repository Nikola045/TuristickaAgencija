using System;
using System.Collections.Generic;
using System.IO;
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
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for RenovationPage.xaml
    /// </summary>
    public partial class RenovationPage : Page
    {
        private readonly HotelService hotelService;
        private readonly ReservationService reservationService;
        private User LogedUser { get; }
        public RenovationPage(User user)
        {
            LogedUser = user;
            hotelService = new HotelService();
            reservationService = new ReservationService();
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            HotelCB.ItemsSource = hotelService.FillForComboBoxHotels(LogedUser);
        }

        private void ShowDates(object sender, RoutedEventArgs e)
        {
            ComboBoxItem comboBoxItem = HotelCB.SelectedItem as ComboBoxItem;
            object selectedItem = HotelCB.SelectedItem;
            List<DateTime> alternativeDates = reservationService.FindAlternativeDates(comboBoxItem.Content.ToString(), Convert.ToDateTime(StartDate.Text), Convert.ToDateTime(EndDate.Text), Convert.ToInt32(NumberOfDays.Text));
            List<string> alternatives = new List<string>();
            foreach(DateTime date in alternativeDates)
            {
                alternatives.Add(date.ToShortDateString() + " to " + (date.AddDays(Convert.ToInt32(NumberOfDays.Text))).ToShortDateString() );
            }
            ListDates.ItemsSource = alternatives;
        }

        private void AcceptAlternative(object sender, RoutedEventArgs e)
        {
            object selectedItem = ListDates.SelectedItem;
            string newStart;
            string newEnd;
            string line = selectedItem.ToString();
            string[] fields = line.Split(" to ");
            newStart = fields[0];
            newEnd = fields[1];

            StartDate.Text = newStart;
            EndDate.Text = newEnd;
        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            reservationService.ReserveRenovation(HotelCB,Convert.ToDateTime(StartDate.Text), Convert.ToDateTime(EndDate.Text));
        }
    }
}
