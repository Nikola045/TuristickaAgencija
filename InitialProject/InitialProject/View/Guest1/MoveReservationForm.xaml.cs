﻿using System;
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
using DevExpress.Xpo.Logger;
using DevExpress.XtraEditors.Filtering;
using Microsoft.Kiota.Abstractions;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for MoveReservationForm.xaml
    /// </summary>
    public partial class MoveReservationForm : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        ReservationService reservationService;
        MoveReservationRepository moveReservationRepository;
        ReservationRepository reservationRepository;
        HotelService hotelService;
        User LogedUser { get; set; }
        public MoveReservationForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            reservationService = new ReservationService();
            moveReservationRepository = new(InjectorService.CreateInstance<IStorage<MoveReservation>>());
            reservationRepository = new(InjectorService.CreateInstance<IStorage<Reservation>>());
            hotelService = new HotelService();
            LogedUser = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = reservationService.FindReservationByGuestUsername(LogedUser.Username);
            DataPanel.ItemsSource = reservations;
            btnRequest.IsEnabled = false;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Reservation SelectedReservation = (Reservation)DataPanel.SelectedItem;
            if (SelectedReservation != null && AllFieldsValid())
            {
                MoveReservation newRequest = new MoveReservation(
                    SelectedReservation,
                    SelectedReservation.HotelName,
                    LogedUser.Username,
                    SelectedReservation.StartDate,
                    SelectedReservation.EndDate,
                    Convert.ToDateTime(NewStartDate.Text),
                    Convert.ToDateTime(NewEndDate.Text)
                );
                moveReservationRepository.Save(newRequest);
                MessageBox.Show("Request sent");
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            Reservation SelectedReservation = (Reservation)DataPanel.SelectedItem;
            if (SelectedReservation != null)
            {
                Hotel hotel = hotelService.GetHotelByName(SelectedReservation.HotelName);
                Reservation findedReservation = reservationService.FindReservationByID(SelectedReservation.Id);
                int daysUntilCheckin = (int)(findedReservation.StartDate - dateTime).TotalDays;

                if (hotel.NumberOfDaysToCancel <= daysUntilCheckin)
                {
                    findedReservation.GradeStatus = "Canceled";
                    reservationRepository.Update(findedReservation);
                    MessageBox.Show("Reservation successfully canceled.");
                }
                else
                {
                    MessageBox.Show("This reservation cannot be canceled at this time.");
                }
                List<Reservation> reservations = reservationService.FindReservationByGuestUsername(LogedUser.Username);
                DataPanel.ItemsSource = reservations;
            }
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            btnRequest.IsEnabled = false;
        }
        private void NewStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime today = DateTime.Now.Date;
            if (NewStartDate.SelectedDate.HasValue && NewStartDate.SelectedDate.Value.Date < today)
            {
                if (NewStartDate.SelectedDate.Value.Date != today)
                {
                    MessageBox.Show("It is not possible to select a date before today.");
                }
                NewStartDate.SelectedDate = today;
            }
            btnRequest.IsEnabled = AllFieldsValid();
        }
        private void NewEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime today = DateTime.Now.Date;
            if (NewEndDate.SelectedDate.HasValue && NewEndDate.SelectedDate.Value.Date < today)
            {
                if (NewEndDate.SelectedDate.Value.Date != today)
                {
                    MessageBox.Show("It is not possible to select a date before today.");
                }
                NewEndDate.SelectedDate = today;
            }
            btnRequest.IsEnabled = AllFieldsValid();
        }
        private bool AllFieldsValid()
        {
            if (NewStartDate.SelectedDate != null && NewEndDate.SelectedDate != null && DataPanel.SelectedItem != null)
            {
                if (NewStartDate.SelectedDate >= NewEndDate.SelectedDate)
                {
                    MessageBox.Show("Start date must be before end date.");
                    return false;
                }
                else
                {
                    Reservation selectedReservation = (Reservation)DataPanel.SelectedItem;
                    if (selectedReservation.StartDate <= Convert.ToDateTime(NewStartDate.Text) && selectedReservation.EndDate >= Convert.ToDateTime(NewEndDate.Text))
                    {
                        MessageBox.Show("The selected date range is already booked.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }        
        private void DataPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataPanel.SelectedItem != null)
            {
                btnRequest.IsEnabled = AllFieldsValid();
            }
            else
            {
                btnRequest.IsEnabled = false;
            }
        }

    }
}
