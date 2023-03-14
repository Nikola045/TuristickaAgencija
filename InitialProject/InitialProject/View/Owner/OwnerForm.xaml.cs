using TravelAgency.Model;
using TravelAgency.Repository;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Linq;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper;
using System.Linq;
using TravelAgency.Serializer;
using System.Data;
using Cake.Core.IO;
using Microsoft.Graph.Models;
using System.Windows.Data;

namespace TravelAgency.Forms
{
    public partial class Guest2Form : Window
    {
        private const string FilePath = "../../../Resources/Data/hotels.csv";

        private readonly HotelRepository hotelRepository;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        

        public Guest2Form()
        {
            InitializeComponent();
            Title = "Create new hotel";
            DataContext = this;
            hotelRepository = new HotelRepository();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
                 string typeOfHotel = null;
                 if (RadioHouse.IsChecked == true) typeOfHotel = "House";
                 else if (RadioHotel.IsChecked == true) typeOfHotel = "Hotel";
                 else if (RadioHut.IsChecked == true) typeOfHotel = "Hut";
                 else if (RadioApartment.IsChecked == true) typeOfHotel = "Apartment";

                Hotel newHotel = new Hotel(
                    hotelRepository.NextId(),
                    txtName.Text,
                    txtCity.Text,
                    txtCountry.Text,
                    typeOfHotel,
                    Convert.ToInt32(brMax.Text),
                    Convert.ToInt32(brMin.Text),
                    Convert.ToInt32(brDaysLeft.Text));
                 Hotel savedHotel = hotelRepository.Save(newHotel);

                MessageBox.Show("Uspesno unet smestaj");

                txtName.Clear();
                txtCity.Clear();
                txtCountry.Clear();
                brMax.Clear();
                brMin.Clear();
                brDaysLeft.Clear();  
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {         
            List<Hotel> hotels = new List<Hotel>();
            hotels = hotelRepository.ReadFromHotelsCsv(FilePath);
            DataPanel.ItemsSource = hotels;
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            ListImg.Items.Add(txtImg.Text);
            MessageBox.Show("Slika dodata");
            txtImg.Clear();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
