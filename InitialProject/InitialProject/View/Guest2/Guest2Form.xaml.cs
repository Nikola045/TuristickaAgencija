﻿using Cake.Core.IO;
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
using TravelAgency.Forms;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for Guest2Form.xaml
    /// </summary>
    public partial class Guest2Form : Window
    {
        private readonly TourRepository _repository;



        Model.User LogedUser = new Model.User();

        const string FilePath = "../../../Resources/Data/tours.csv";

        public event PropertyChangedEventHandler PropertyChanged;

        public Tour selectedTour;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Guest2Form()
        {
            InitializeComponent();
            Title = "Search tours";
            DataContext = this;
            _repository = new TourRepository();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            List<Tour> tours = new List<Tour>();
            const string FilePath = "../../../Resources/Data/tours.csv";
            tours = _repository.ReadFromToursCsv(FilePath);
            DataPanel.ItemsSource = tours;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            List<Tour> tours = new List<Tour>();
            
            tours = _repository.FindTour(FilePath, txtCity.Text, txtCountry.Text, txtLeng.Text, txtDuration.Text, txtNum.Text);
            DataPanel.ItemsSource = tours;
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            const string FilePath = "../../../Resources/Data/tours.csv";
            List<Tour> tours = new List<Tour>();
            tours = _repository.ReadFromToursCsv(FilePath);
            DataPanel.ItemsSource = tours;
        }

        private void AddPeopleOnSelectedTour(object sender, RoutedEventArgs e)
        {
            selectedTour = (Tour)DataPanel.SelectedItem;
            int tourId = selectedTour.Id;
            const string FilePath = "../../../Resources/Data/guestOnTour.csv";
            string num = txtNumOfGuests.Text;
            if (/*_repository.ReserveTour(FilePath, tourId, LogedUser.Username) &&*/ _repository.UpdateNumberOfGuests(tourId, num) ){
                MessageBox.Show("Reserved.");
            }
            else
            {
                MessageBox.Show("Not reserved.");
            }
        }
    }
}
