﻿using Microsoft.Graph.Models;
using Microsoft.IdentityModel.Tokens;
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

            bool requirementsMet = true;

            for (int i = 0; i < hotels.Count; i++)
            {
                if (HotelNameCB.SelectedItem.ToString() == hotels[i].Name)
                {
                    if( Convert.ToInt32(txtNumberOfDays.Text) <= 0 || Convert.ToInt32(txtNumberOfDays.Text) < hotels[i].MinNumberOfDays)
                    {
                        MessageBox.Show("Minimum numbers of days for " + hotels[i].Name + " must be greater than " + Convert.ToInt32(hotels[i].MinNumberOfDays));
                        requirementsMet = false;
                    }
                    if (Convert.ToInt32(txtNumberOfGuests.Text) <= 0 || Convert.ToInt32(txtNumberOfGuests.Text) > hotels[i].MaxNumberOfGuests)
                    {
                        MessageBox.Show("Maximum guests for " + hotels[i].Name + " must be lower than " + Convert.ToInt32(hotels[i].MaxNumberOfGuests));
                        requirementsMet = false;
                    }
                    if (Date2.SelectedDate <= Date1.SelectedDate)
                    {
                        MessageBox.Show("Return date must be greater than departure date");
                        requirementsMet = false;
                    }
                }
            }
             
            if (requirementsMet)
            {
                Reservation newReservation = new Reservation(
                    _repository.NextId(),
                    LogedUser.Username,
                    HotelNameCB.Text,
                    Convert.ToDateTime(Date1.Text),
                    Convert.ToDateTime(Date2.Text),
                    Convert.ToInt32(txtNumberOfDays.Text),
                    Convert.ToInt32(txtNumberOfGuests.Text)); ;
                _repository.Save(newReservation);
                MessageBox.Show("Reservation made succesfully!");

                HotelNameCB.SelectedItem = null;
                txtNumberOfDays.Clear();
                txtNumberOfGuests.Clear();
                Date1.SelectedDate = null;
                Date2.SelectedDate = null;
                btnReserve.IsEnabled = false;
            }
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
                if (HotelNameCB.SelectedItem != null && HotelNameCB.SelectedItem.ToString() == hotels[i].Name)
                {
                    txtNumberOfDays.Text = hotels[i].MinNumberOfDays.ToString();
                    txtNumberOfGuests.Text = hotels[i].MaxNumberOfGuests.ToString();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnReserve.IsEnabled = false;
        }

        private bool AllFieldsValid()
        {
            if (HotelNameCB.SelectedItem != null
                && !string.IsNullOrEmpty(txtNumberOfDays.Text)
                && !string.IsNullOrEmpty(txtNumberOfGuests.Text)
                && Date1.SelectedDate != null
                && Date2.SelectedDate != null)
            return true;
           
            else return false;
        }

        private void HotelNameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
        }

        private void txtNumberOfDays_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
        }

        private void txtNumberOfGuests_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
            if (string.IsNullOrEmpty(txtNumberOfDays.Text))
            {
                btnReserve.IsEnabled = false;
            }
        }

        private void Date1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
        }

        private void Date2_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
        }

        private void txtNumberOfDays_LostFocus(object sender, RoutedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
        }

        private void txtNumberOfGuests_LostFocus(object sender, RoutedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
        }
    }
}
