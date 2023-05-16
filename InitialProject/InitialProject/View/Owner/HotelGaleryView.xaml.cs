using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TravelAgency.Domain.Model;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;
using Image = TravelAgency.Domain.Model.Image;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for HotelGalery.xaml
    /// </summary>
    public partial class HotelGalery : Window, INotifyPropertyChanged
    {
        private HotelService hotelService;
        public Hotel CurrentHotel { get; set; }
        public int indexer = 0;
        public ImageSource _imageSource;

        public event PropertyChangedEventHandler? PropertyChanged;

        public HotelGalery(Hotel hotel)
        {
            hotelService = new HotelService();
            CurrentHotel = hotel;
            InitializeComponent();
        }

        private void NextImage(object sender, RoutedEventArgs e)
        {
           List<Image> allHotelImages = hotelService.FindAllById(CurrentHotel.Id);
            indexer++;
            if (indexer > allHotelImages.Count-1)
                indexer = 0;
            Image.Source = new ImageSourceConverter().ConvertFromString(allHotelImages[indexer].Url) as ImageSource;
        }

        private void PreviewImage(object sender, RoutedEventArgs e)
        {
            List<Image> allHotelImages = hotelService.FindAllById(CurrentHotel.Id);
            indexer--;
            if (indexer < 0)
                indexer = allHotelImages.Count-1;
            Image.Source = new ImageSourceConverter().ConvertFromString(allHotelImages[indexer].Url) as ImageSource;
        }

        public ImageSource ImageSource
        {
            get => _imageSource;
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
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
