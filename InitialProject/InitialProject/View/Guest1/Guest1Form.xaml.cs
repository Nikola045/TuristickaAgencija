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
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for Guest1Form.xaml
    /// </summary>
    public partial class Guest1Form : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public static ObservableCollection<Hotel> Hotels { get; set; }
        public HotelRepository hotelRepository { get; set; }
        private readonly HotelService hotelService;
        private readonly OwnerService ownerService;
        private readonly ReservationService reservationService;

        const string FilePath = "../../../Resources/Data/hotels.csv";
        public Guest1Form()
        {
            InitializeComponent();
            Title = "Search hotel";
            DataContext = this;
            hotelRepository = new(InjectorService.CreateInstance<IStorage<Hotel>>());
            Hotels = new ObservableCollection<Hotel>(hotelRepository.GetAll());
            hotelService = new HotelService();
            ownerService = new OwnerService();
            reservationService = new ReservationService();
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            reservationService.ChangeAllRenovatedStatus();
            List<Hotel> hotels = new List<Hotel>();
            hotels = hotelRepository.GetAll();
            List<User> superOwners = ownerService.GetAllSuperOwners();
            foreach (Hotel hotel in hotels)
            {
                foreach(User superOwner in superOwners)
                {
                    if (hotel.OwnerUsername == superOwner.Username)
                        hotel.OwnerUsername = hotel.OwnerUsername + " Super-Owner";
                }
                
            }
            DataPanel.ItemsSource = hotelService.SortBySuperOwner(hotels);
            ExpandColumns(DataPanel);
        }
        private void ExpandColumns(DataGrid dataGrid)
        {
            double totalWidth = dataGrid.ActualWidth;
            int columnCount = dataGrid.Columns.Count;

            if (columnCount > 0)
            {
                double columnWidth = totalWidth / columnCount;

                foreach (DataGridColumn column in dataGrid.Columns)
                {
                    column.Width = new DataGridLength(columnWidth);
                }
            }
        }
        private void Search(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            string RadioChoice = null;
            if (RadioHouse.IsChecked == true) { RadioChoice = "House"; }
            else if (RadioHotel.IsChecked == true) { RadioChoice = "Hotel"; }
            else if (RadioHut.IsChecked == true) { RadioChoice = "Hut"; }
            else if (RadioApartment.IsChecked == true) { RadioChoice = "Apartment"; }
            hotels = hotelService.FindHotel(txtName.Text, txtCity.Text, txtCountry.Text, RadioChoice, txtNoGuests.Text, txtNoDays.Text);
            DataPanel.ItemsSource = hotels;
        }
        private void DataPanel_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "NumberOfDaysToCancel" || e.PropertyName == "OwnerUsername" || e.PropertyName == "RenovationStatus")
            {
                e.Cancel = true;
            }
        }

    }
}
