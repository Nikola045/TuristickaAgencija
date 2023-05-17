using System;
using System.Collections.Generic;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Guest2
{
    public partial class PastTours : Window
    {
        private readonly TourService tourService;
        User LogedUser = new User();
        private Tour selectedTour;

        public PastTours(User logedUser)
        {
            InitializeComponent();
            LogedUser = logedUser;
            tourService = new TourService();
        }


        private void LoadData(object sender, RoutedEventArgs e)
        {
            List<Tour> tour = new List<Tour>();
            tour = tourService.ReadMyPastToursCsv(LogedUser.Id);

            DataPanel.ItemsSource = tour;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MakeReview(object sender, RoutedEventArgs e)
        {
            if (DataPanel.SelectedItem != null)
            {
                selectedTour = (Tour)DataPanel.SelectedItem;
                TourReview createTourReview = new TourReview(LogedUser, selectedTour);
                createTourReview.Show();
            }
            else
            {
                MessageBox.Show("Please select a tour you want to review.");
            }
        }
    }
}
