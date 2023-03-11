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



    public partial class OwnerForm : Window
    {
        public Hotel SelectedHotel { get; set; }

        private readonly HotelRepository _repository;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        

        public OwnerForm()
        {
            InitializeComponent();
            Title = "Create new hotel";
            DataContext = this;
            _repository = new HotelRepository();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            
             if (SelectedHotel != null)
             {
                 SelectedHotel.Id = _repository.NextId();
                 SelectedHotel.Name = txtName.Text;
                 SelectedHotel.City = txtCity.Text;
                 SelectedHotel.Country = txtCountry.Text;
                 if(RadioHouse.IsChecked == true) { SelectedHotel.TypeOfHotel = "House"; }
                 else if(RadioHotel.IsChecked == true) { SelectedHotel.TypeOfHotel = "Hotel"; }
                 else if(RadioHut.IsChecked == true) { SelectedHotel.TypeOfHotel = "Hut"; }
                 else if(RadioApartment.IsChecked == true) { SelectedHotel.TypeOfHotel = "Apartment"; }
                 SelectedHotel.MaxNumberOfGusets = Convert.ToInt32( brMax.Text);
                 SelectedHotel.MinNumberOfGusets = Convert.ToInt32(brMin.Text);
                 SelectedHotel.NumberOfDaysToCancel = Convert.ToInt32(brDaysLeft.Text);

                 Hotel updatedHotel = _repository.Update(SelectedHotel);
                 if (updatedHotel != null)
                 {
                     int index = OwnerOverview.Hotels.IndexOf(SelectedHotel);
                     OwnerOverview.Hotels.Remove(SelectedHotel);
                     OwnerOverview.Hotels.Insert(index, updatedHotel);
                 }
             }
             else
             {
                 string typeOfHotel = null;
                 if (RadioHouse.IsChecked == true) typeOfHotel = "House";
                 else if (RadioHotel.IsChecked == true) typeOfHotel = "Hotel";
                 else if (RadioHut.IsChecked == true) typeOfHotel = "Hut";
                 else if (RadioApartment.IsChecked == true) typeOfHotel = "Apartment";

                Hotel newHotel = new Hotel(
                    _repository.NextId(),
                    txtName.Text,
                    txtCity.Text,
                    txtCountry.Text,
                    typeOfHotel,
                    Convert.ToInt32(brMax.Text),
                    Convert.ToInt32(brMin.Text),
                    Convert.ToInt32(brDaysLeft.Text));
                 Hotel savedHotel = _repository.Save(newHotel);

                txtName.Clear();
                txtCity.Clear();
                txtCountry.Clear();
                brMax.Clear();
                brMin.Clear();
                brDaysLeft.Clear();
            }

            
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            hotels = _repository.ReadFromHotelsCsv("C:\\Users\\kojic\\Desktop\\TuristickaAgencija\\TuristickaAgencija\\InitialProject\\InitialProject\\Resources\\Data\\hotels.csv");
            DataPanel.ItemsSource = hotels;
        }
    }
}
