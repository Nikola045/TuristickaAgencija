using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Serializer;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for HotelGalery.xaml
    /// </summary>
    public partial class HotelGalery : Window
    {
        private HotelImageRepository hotelImageRepository;
        private readonly Serializer<HotelImage> _serializer;
        public Hotel CurrentHutel { get; set; }

        public HotelGalery(Hotel hotel)
        {
            hotelImageRepository = new HotelImageRepository();
            _serializer = new Serializer<HotelImage>();
            CurrentHutel = hotel;
            InitializeComponent();
        }

        private void NextImage(object sender, RoutedEventArgs e)
        {

        }

        private void PreviewImage(object sender, RoutedEventArgs e)
        {

        }
    }
}
