using Microsoft.Graph.Models;
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
using System.Windows.Shapes;
using System.Xml.Linq;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for ReservationForm.xaml
    /// </summary>
    public partial class ReservationForm : Window
    {
        Reservation NewReservation = new Reservation();

        Model.User LogedUser = new Model.User();

        private readonly ReservationRepository _repository;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly HotelRepository hotelRepository;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public ReservationForm(Model.User user)
        {
            InitializeComponent();
            Title = "Create new reservation";
            DataContext = this;
            LogedUser = user;
            _repository = new ReservationRepository();
            hotelRepository = new HotelRepository();
        }
        

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            string FilePath = "../../../Resources/Data/hotels.csv";
            hotels = hotelRepository.ReadFromHotelsCsv(FilePath);

            for (int i = 0; i < hotels.Count; i++)
            {
                if (HotelNameCB.SelectedItem.ToString() == hotels[i].Name)
                {
                    if( Convert.ToInt32(txtNumberOfDays.Text) <= 0 || Convert.ToInt32(txtNumberOfDays.Text) > hotels[i].MinNumberOfDays)
                    {
                        MessageBox.Show("Minimum numbers of days for " + hotels[i].Name + " must be grater then " + Convert.ToInt32(hotels[i].MinNumberOfDays));
                    }
                    if (Convert.ToInt32(txtNumberOfGuests.Text) <= 0 || Convert.ToInt32(txtNumberOfGuests.Text) > hotels[i].MaxNumberOfGuests)
                    {
                        MessageBox.Show("Maximum guests for " + hotels[i].Name + " must be lower then " + Convert.ToInt32(hotels[i].MaxNumberOfGuests));
                    }
                }
            }

            Reservation newReservation = new Reservation(
                _repository.NextId(),
                LogedUser.Username,
                HotelNameCB.Text,
                Convert.ToDateTime(Date1.Text),
                Convert.ToDateTime(Date2.Text),
                Convert.ToInt32(txtNumberOfDays.Text),
                Convert.ToInt32(txtNumberOfGuests.Text)); ;
            _repository.Save(newReservation);
            MessageBox.Show("Uspesno rezervisano");

            txtNumberOfDays.Clear();
            txtNumberOfGuests.Clear();

        }

        private void LoadHotels(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            string FilePath = "../../../Resources/Data/hotels.csv";
            hotels = hotelRepository.ReadFromHotelsCsv(FilePath);

            for(int i = 0; i < hotels.Count; i++)
            {
                HotelNameCB.Items.Add(hotels[i].Name);
            }
        }

        private void DefaultValuesForTXT(object sender, SelectionChangedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            string FilePath = "../../../Resources/Data/hotels.csv";
            hotels = hotelRepository.ReadFromHotelsCsv(FilePath);

            txtNumberOfDays.IsEnabled = true;
            txtNumberOfGuests.IsEnabled = true;

            for (int i = 0; i < hotels.Count; i++)
            {
                if (HotelNameCB.SelectedItem.ToString() == hotels[i].Name)
                {
                    txtNumberOfDays.Text = hotels[i].MinNumberOfDays.ToString();
                    txtNumberOfGuests.Text = hotels[i].MaxNumberOfGuests.ToString();
                }
            }
        }
    }
}
