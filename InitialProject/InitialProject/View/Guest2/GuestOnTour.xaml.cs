using System.Collections.Generic;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Guest2
{
    public partial class GuestOnTour : Window
    {
        User LogedUser = new User();
        private readonly TourService tourService;
        public Tour selectedTour;
        public CheckPoint selectedCheckPoint;

        public GuestOnTour(User logedUser)
        {
            InitializeComponent();
            LogedUser = logedUser;
            tourService = new TourService();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        { 
            List<Tour> tours = new List<Tour>();
            tours = tourService.GetMyTours(LogedUser.Id);
            DataPanel.ItemsSource = tours;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void ConfirmArrival(object sender, RoutedEventArgs e)
        {
            selectedTour = (Tour)DataPanel.SelectedItem;
            selectedCheckPoint = (CheckPoint)KeyPoints.SelectedItem;

            if (selectedTour != null)
            {
                if(selectedCheckPoint != null)
                {
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            selectedTour = (Tour)DataPanel.SelectedItem;
            KeyPoints.ItemsSource = selectedTour.CheckPoints;
        }
    }
}
