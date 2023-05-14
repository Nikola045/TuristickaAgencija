using Cake.Core.Tooling;
using Microsoft.Graph.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
using TravelAgency.Domain.Model;
using TravelAgency.Repository;
using static Azure.Core.HttpHeader;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for CreatingTourRequest.xaml
    /// </summary>
    public partial class CreatingTourRequest : Window
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly TourRequestsRepository tourRequestsRepository;
        public User LoggedInUser { get; set; }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreatingTourRequest(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            tourRequestsRepository = new TourRequestsRepository();

        }

        public bool ButtonActivator()
        {
            if (
                CityV == "" &&
                CountryV == "" &&
                MaxV == "" &&
                DescriptionV == "" &&
                LanguageV == "" &&

                City != "" &&
                Country != "" &&
                Max.ToString() != "" &&
                Description != "" &&
                Language != ""

            {
                return true;
            }
            else
            { return false; }
        }

        private void CityValidation(object sender, TextChangedEventArgs e)
        {
            string city = City;
            if (Regex.IsMatch(city, @"^[a-zA-Z\s]+$"))
            {
                CityV = "";
            }
            else
            {
                CityV = "Please enter valid value";
            }
        }

        private void CountryValidation(object sender, TextChangedEventArgs e)
        {
            string country = Country;
            if (Regex.IsMatch(country, @"^[a-zA-Z\s]+$"))
            {
                CountryV = "";
            }
            else
            {
                CountryV = "Please enter valid value";
            }
        }
        private void SaveTourRequest(object sender, RoutedEventArgs e)
        {
            TourRequests newTourRequests = new TourRequests(
                tourRequestsRepository.NextId(),
                LoggedInUser.Id,
                txtCity.Text,
                txtCountry.Text,
                txtDescription.Text,
                txtLanguage.Text,
                Convert.ToInt32(txtMaxNumberOfGuests.Text),
                Convert.ToDateTime(FirstDateBox.Text),
                Convert.ToDateTime(SecondDateBox.Text));

                tourRequestsRepository.Save(ButtonActivator(), newTourRequests);
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Guest2Overview guest2Overview = new Guest2Overview(LoggedInUser);
            this.Close();
            guest2Overview.Show();

        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Guest2Overview guest2Overview = new Guest2Overview(LoggedInUser);
            this.Close();
            guest2Overview.Show();

        }

    }

}
