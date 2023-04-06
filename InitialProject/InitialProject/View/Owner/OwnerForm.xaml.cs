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
using static Azure.Core.HttpHeader;
using System.Text.RegularExpressions;
using Microsoft.Windows.Themes;
using static System.Net.WebRequestMethods;
using TravelAgency.Domain.Model;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;

namespace TravelAgency.Forms
{
    public partial class OwnerForm : Window
    {
        private readonly HotelRepository hotelRepository;
        private readonly HotelImageRepository hotelImageRepository;
        private readonly HotelService hotelService;
        public OwnerForm()
        {
            InitializeComponent();
            Title = "Create new hotel";
            DataContext = this;
            hotelRepository = new HotelRepository();
            hotelImageRepository = new HotelImageRepository();
            hotelService = new HotelService();
        }

        public bool ButtonActivator()
        {
            if(
                LabelNameValidator.Content == "" &&
                LabelCityValidator.Content == "" &&
                LabelCountryValidator.Content == ""&&
                LabelMaxGuestValidator.Content == ""&&
                LabelMinDaysValidator.Content == ""&&
                LabelCancelDaysValidator.Content == ""&&
                LabelTypeValidator.Content == ""&&
                LabelImgValidator.Content == "")
            {
                return true;
            }
            else
            { return false; }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (ButtonActivator())
            {
               Hotel newHotel = new Hotel(
                    hotelRepository.NextId(),
                    txtName.Text,
                    txtCity.Text,
                    txtCountry.Text,
                    Type.Text,
                    Convert.ToInt32(brMax.Text),
                    Convert.ToInt32(brMin.Text),
                    Convert.ToInt32(brDaysLeft.Text));
               Hotel savedHotel = hotelRepository.Save(newHotel);

                MessageBox.Show("Accommodation successfully created");

                txtName.Clear();
                txtCity.Clear();
                txtCountry.Clear();
                brMax.Clear();
                brMin.Clear();
                brDaysLeft.Clear();
            }
            else
            {
                MessageBox.Show("Please check your input datas");
            }
        }



        private void AddImage(object sender, RoutedEventArgs e)
        {
            HotelImage newImage = new HotelImage(
               hotelImageRepository.NextId(),
               txtImg.Text);

            HotelImage savedImage = hotelImageRepository.Save(newImage);
            ImageList.Items.Add(txtImg.Text);
            MessageBox.Show("You have successfully added an image");
            LabelImgValidator.Content = "";
            txtImg.Clear();
        }

        private void DeleteImage(object sender, RoutedEventArgs e)
        {
            object selectedItem = ImageList.SelectedItem;
            HotelImage hotelImage = hotelService.FindByUrl(selectedItem.ToString());

            ImageList.Items.Remove(selectedItem);
            hotelImageRepository.Delete(hotelImage);
            if(ImageList.Items.IsEmpty == true)
            {
                LabelImgValidator.Content = "Please add at least one image";
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            foreach(object item in ImageList.Items)
            {
                HotelImage hotelImage = hotelService.FindByUrl(item.ToString());
                hotelImageRepository.Delete(hotelImage);
            }
            this.Close();
        }

        //Validation 
        private void NameValidation(object sender, TextChangedEventArgs e)
        {
            string name = txtName.Text;
            if (Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                LabelNameValidator.Content = "";
            }
            else
            {
                LabelNameValidator.Content = "Please enter valid value";
            }
        }

        private void CityValidation(object sender, TextChangedEventArgs e)
        {
            string city = txtCity.Text;
            if (Regex.IsMatch(city, @"^[a-zA-Z\s]+$"))
            {
                LabelCityValidator.Content = "";
            }
            else
            {
                LabelCityValidator.Content = "Please enter valid value";
            }
        }

        private void CountryValidation(object sender, TextChangedEventArgs e)
        {
            string country = txtCountry.Text;
            if (Regex.IsMatch(country, @"^[a-zA-Z\s]+$"))
            {
                LabelCountryValidator.Content = "";
            }
            else
            {
                LabelCountryValidator.Content = "Please enter valid value";
            }
        }

        private void MaxGuestValidation(object sender, TextChangedEventArgs e)
        {
            string maxGuests = brMax.Text;
            if (Regex.IsMatch(maxGuests, @"^[0-9]+$"))
            {
                LabelMaxGuestValidator.Content = "";
            }
            else
            {
                LabelMaxGuestValidator.Content = "Please enter valid value";
            }
        }

        private void MinDaysValidation(object sender, TextChangedEventArgs e)
        {
            string minDays = brMin.Text;
            if (Regex.IsMatch(minDays, @"^[0-9]+$"))
            {
                LabelMinDaysValidator.Content = "";
            }
            else
            {
                LabelMinDaysValidator.Content = "Please enter valid value";
            }
        }
        private void CancelDaysValidation(object sender, TextChangedEventArgs e)
        {
            string cancelDays = brDaysLeft.Text;
            if (Regex.IsMatch(cancelDays, @"^[0-9]+$"))
            {
                LabelCancelDaysValidator.Content = "";
            }
            else
            {
                LabelCancelDaysValidator.Content = "Please enter valid value";
            }
        }


        private void UrlValidation(object sender, TextChangedEventArgs e)
        {
            string url = txtImg.Text;
            if (Regex.IsMatch(url, @"(?: ([^:/?#]+):)?(?://([^/?#]*))?([^?#]*\.(?:jpg|gif|png))(?:\?([^#]*))?(?:#(.*))?"))
            {
                LabelUrlValidator.Content = "";
                AddImgButton.IsEnabled = true;
            }
            else
            {
                LabelUrlValidator.Content = "Please input valid ulr address";
                AddImgButton.IsEnabled = false;
            }
        }

        private void DataFill(object sender, RoutedEventArgs e)
        {
            Type.Items.Add("Hotel");
            Type.Items.Add("Hut");
            Type.Items.Add("House");
            Type.Items.Add("Apartment");
        }

        private void ValidationType(object sender, SelectionChangedEventArgs e)
        {
            LabelTypeValidator.Content = "";
        }
    }
}
