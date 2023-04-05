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
using TravelAgency.Model;
using TravelAgency.Repository;
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

        List<HotelImage> hotelImages;
        int i = 1;
        public HotelGalery()
        {
            hotelImageRepository = new HotelImageRepository();
            _serializer = new Serializer<HotelImage>();
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
