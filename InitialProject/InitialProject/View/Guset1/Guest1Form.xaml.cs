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
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for Guest1Form.xaml
    /// </summary>
    public partial class Guest1Form : Window
    {
        private readonly HotelRepository _repository;

        const string FilePath = "../../../Resources/Data/hotels.csv";
        public Guest1Form()
        {
            InitializeComponent();
            Title = "Search hotel";
            DataContext = this;
            _repository = new HotelRepository();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            hotels = _repository.ReadFromHotelsCsv(FilePath);
            DataPanel.ItemsSource = hotels;
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            string RadioChoice = null;
            if (RadioHouse.IsChecked == true) { RadioChoice = "House"; }
            else if (RadioHotel.IsChecked == true) { RadioChoice = "Hotel"; }
            else if (RadioHut.IsChecked == true) { RadioChoice = "Hut"; }
            else if (RadioApartment.IsChecked == true) { RadioChoice = "Apartment"; }
            hotels = _repository.FindHotel(FilePath, txtName.Text, txtCity.Text, txtCountry.Text, RadioChoice, brMax.Text, brDaysLeft.Text);
            DataPanel.ItemsSource = hotels;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
