﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Graph.Models.Security;
using TravelAgency.Domain.Model;
using TravelAgency.Repository;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;
using TravelAgency.View.Guest1;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for ReservationForm.xaml
    /// </summary>
    public partial class ReservationForm : Page
    {
        User LogedUser = new User();

        private readonly App app = (App)App.Current;
        private readonly ReservationRepository _repository;
        private readonly HotelRepository hotelRepository;
        private readonly ReservationService reservationService;
        public ReservationForm(User user)
        {
            InitializeComponent();
            Title = "Create new reservation";
            DataContext = this;
            LogedUser = user;
            _repository = app.ReservationRepository;
            hotelRepository = app.HotelRepository;
            reservationService = new ReservationService();
        }
        
        private void Reserve(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            List<Reservation> reservations = _repository.GetAll();
            hotels = hotelRepository.GetAll();

            bool requirementsMet = true;

            for (int i = 0; i < hotels.Count; i++)
            {
                if (HotelNameCB.SelectedItem.ToString() == hotels[i].Name)
                {
                    if (Convert.ToInt32(txtNumberOfGuests.Text) <= 0 || Convert.ToInt32(txtNumberOfGuests.Text) > hotels[i].MaxNumberOfGuests)
                    {
                        guestsValidation.Content = ("*Maximum guests for " + hotels[i].Name + " must be lower than " + Convert.ToInt32(hotels[i].MaxNumberOfGuests));
                        guestsValidation.Visibility = Visibility.Visible;
                        requirementsMet = false;
                    }
                    else
                    {
                        guestsValidation.Visibility = Visibility.Collapsed;
                    }

                    if (Convert.ToInt32(txtNumberOfDays.Text) < hotels[i].MinNumberOfDays)
                    {
                        daysValidation.Content = ("*Minimum number of days for " + hotels[i].Name + " must be greater than " + hotels[i].MinNumberOfDays);
                        daysValidation.Visibility = Visibility.Visible;
                        requirementsMet = false;
                    }
                    else
                    {
                        daysValidation.Visibility = Visibility.Collapsed;
                    }

                    if (!reservationService.IsAvailable(reservations, HotelNameCB.SelectedItem.ToString(), Date1.SelectedDate.Value, Date2.SelectedDate.Value))
                    {
                        List<DateTime> alternativeDates = reservationService.FindAlternativeDates(HotelNameCB.SelectedItem.ToString(), Date1.SelectedDate.Value, Date2.SelectedDate.Value, Convert.ToInt32(txtNumberOfDays.Text));

                        if (alternativeDates.Count > 0)
                        {
                            StringBuilder message = new StringBuilder();
                            message.AppendLine($"*{HotelNameCB.SelectedItem} is already reserved in that period of time.");
                            message.AppendLine();
                            message.AppendLine($"  A few other suggestions for staying in {HotelNameCB.SelectedItem} for {txtNumberOfDays.Text} days:");
                            message.AppendLine();
                            int maxAlternativeDates = Math.Min(alternativeDates.Count, 3);
                            for (int j = 0; j < maxAlternativeDates; j++)
                            {
                                message.AppendLine("- " + alternativeDates[j].ToShortDateString() + " to " + alternativeDates[j].AddDays(Convert.ToInt32(txtNumberOfDays.Text)).ToShortDateString());
                            }

                            suggestionLabel.Content = message.ToString();
                            suggestionLabel.Visibility = Visibility.Visible;
                            requirementsMet = false;
                            break;
                        }
                        else
                        {
                            suggestionLabel.Content = $"*No alternative dates available for staying in {HotelNameCB.SelectedItem} for {txtNumberOfDays.Text} days.";
                            suggestionLabel.Visibility = Visibility.Visible;
                            requirementsMet = false;
                            break;
                        }
                    }

                    suggestionLabel.Visibility = Visibility.Collapsed;



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
                    Convert.ToInt32(txtNumberOfGuests.Text));
                _repository.Save(newReservation);
                SucessfullReservation sucessfullReservationPage = new SucessfullReservation();
                NavigationService.Navigate(sucessfullReservationPage);
            }
        }


        private void LoadHotels(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            hotels = hotelRepository.GetAll();

            for(int i = 0; i < hotels.Count; i++)
            {
                HotelNameCB.Items.Add(hotels[i].Name);
            }
        }

        private void DefaultValuesForTXT(object sender, SelectionChangedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            hotels = hotelRepository.GetAll();

            txtNumberOfGuests.IsEnabled = true;
            txtNumberOfDays.IsEnabled = true;

            for (int i = 0; i < hotels.Count; i++)
            {
                if (HotelNameCB.SelectedItem != null && HotelNameCB.SelectedItem.ToString() == hotels[i].Name)
                {
                    txtNumberOfGuests.Text = hotels[i].MaxNumberOfGuests.ToString();
                    txtNumberOfDays.Text = hotels[i].MinNumberOfDays.ToString();
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
                && !string.IsNullOrEmpty(txtNumberOfGuests.Text)
                && !string.IsNullOrEmpty(txtNumberOfDays.Text)
                && Date1.SelectedDate != null
                && Date2.SelectedDate != null)
            return true;
           
            else return false;
        }
        private void CheckAllFields()
        {
            if (!string.IsNullOrEmpty(txtNumberOfGuests.Text) &&
                !string.IsNullOrEmpty(txtNumberOfDays.Text) &&
                Date1.SelectedDate.HasValue &&
                Date2.SelectedDate.HasValue &&
                HotelNameCB.SelectedItem != null)
            {
                validationLabel.Visibility = Visibility.Collapsed;
            }
            else
            {
                validationLabel.Visibility = Visibility.Visible;
            }
        }

        private void HotelNameCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
        }


        private void Date1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime today = DateTime.Now.Date;
            bool isDate1Valid = Date1.SelectedDate.HasValue && Date1.SelectedDate.Value.Date >= today;
            bool isDate2Valid = Date2.SelectedDate.HasValue && Date2.SelectedDate.Value.Date >= today;

            if (isDate1Valid && isDate2Valid)
            {
                dateValidationBefore.Visibility = Visibility.Collapsed;
            }
            else if (!isDate1Valid && !isDate2Valid)
            {
                dateValidationBefore.Visibility = Visibility.Visible;
            }
            else if (!isDate1Valid && isDate2Valid)
            {
                if (Date1.SelectedDate != null && Date1.SelectedDate.Value.Date < today)
                {
                    dateValidationBefore.Visibility = Visibility.Visible;
                }
                else
                {
                    dateValidationBefore.Visibility = Visibility.Collapsed;
                }
            }
            else if (isDate1Valid && !isDate2Valid)
            {
                if (Date2.SelectedDate != null && Date2.SelectedDate.Value.Date < today)
                {
                    dateValidationBefore.Visibility = Visibility.Visible;
                }
                else
                {
                    dateValidationBefore.Visibility = Visibility.Collapsed;
                }
            }

            btnReserve.IsEnabled = AllFieldsValid() && !dateValidationBefore.IsVisible;

            if (Date1.SelectedDate > Date2.SelectedDate)
            {
                dateValidation.Visibility = Visibility.Visible;
                btnReserve.IsEnabled = false;
            }
            else
            {
                dateValidation.Visibility = Visibility.Collapsed;
            }

            CheckAllFields();
        }

        private void Date2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime today = DateTime.Now.Date;
            bool isDate1Valid = Date1.SelectedDate.HasValue && Date1.SelectedDate.Value.Date >= today;
            bool isDate2Valid = Date2.SelectedDate.HasValue && Date2.SelectedDate.Value.Date >= today;

            if (isDate1Valid && isDate2Valid)
            {
                dateValidationBefore.Visibility = Visibility.Collapsed;
            }
            else if (!isDate1Valid && !isDate2Valid)
            {
                dateValidationBefore.Visibility = Visibility.Visible;
            }
            else if (!isDate1Valid && isDate2Valid)
            {
                if (Date1.SelectedDate != null && Date1.SelectedDate.Value.Date < today)
                {
                    dateValidationBefore.Visibility = Visibility.Visible;
                }
                else
                {
                    dateValidationBefore.Visibility = Visibility.Collapsed;
                }
            }
            else if (isDate1Valid && !isDate2Valid)
            {
                if (Date2.SelectedDate != null && Date2.SelectedDate.Value.Date < today)
                {
                    dateValidationBefore.Visibility = Visibility.Visible;
                }
                else
                {
                    dateValidationBefore.Visibility = Visibility.Collapsed;
                }
            }

            btnReserve.IsEnabled = AllFieldsValid() && !dateValidationBefore.IsVisible;

            if (Date1.SelectedDate > Date2.SelectedDate)
            {
                dateValidation.Visibility = Visibility.Visible;
                btnReserve.IsEnabled = false;
            }
            else
            {
                dateValidation.Visibility = Visibility.Collapsed;
            }

            CheckAllFields();
        }


        private void txtNumberOfGuests_LostFocus(object sender, RoutedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
            CheckAllFields();
        }

        private void txtNumberOfDays_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
            CheckAllFields();
        }

        private void txtNumberOfDays_LostFocus(object sender, RoutedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
            CheckAllFields();
        }

        private void txtNumberOfGuests_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnReserve.IsEnabled = AllFieldsValid();
            CheckAllFields();
        }
    }
}
