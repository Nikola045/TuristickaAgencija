using Microsoft.Graph.Models.Security;
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
        User LoggedInUser = new User();
        private readonly TourService tourService;
        public Tour selectedTour;
        public CheckPoint selectedCheckPoint;

        public GuestOnTour(User logedUser)
        {
            InitializeComponent();
            LoggedInUser = logedUser;
            tourService = new TourService();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        { 
            List<Tour> tours = new List<Tour>();
            tours = tourService.GetMyTours(LoggedInUser.Id);
            DataPanel.ItemsSource = tours;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Guest2Overview guest2Overview = new Guest2Overview(LoggedInUser);
            this.Close();
            guest2Overview.Show();
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

        private void OpenGuestOverview(object sender, RoutedEventArgs e)
        {
            Guest2Overview createGuest2Form = new Guest2Overview(LoggedInUser);
            createGuest2Form.Show();
            Close();
        }

        private void OpenGuest2Form(object sender, RoutedEventArgs e)
        {
            Guest2Form createGuest2Form = new Guest2Form(LoggedInUser);
            Close();
            createGuest2Form.Show();
        }
        private void OpenComplexTourRequests(object sender, RoutedEventArgs e)
        {
            ComplexTourRequests createGuest2Form = new ComplexTourRequests(LoggedInUser);
            Close();
            createGuest2Form.Show();
        }

        private void OpenGuestOnTour(object sender, RoutedEventArgs e)
        {
            GuestOnTour createGuestOnTour = new GuestOnTour(LoggedInUser);
            Close();
            createGuestOnTour.Show();
        }

        private void OpenVouchers(object sender, RoutedEventArgs e)
        {
            VouchersView createVouchers = new VouchersView(LoggedInUser);
            Close();
            createVouchers.Show();
        }

        private void OpenTourReviews(object sender, RoutedEventArgs e)
        {
            PastTours pastTours = new PastTours(LoggedInUser);
            Close();
            pastTours.Show();
        }
        private void OpenCreateTourRequest(object sender, RoutedEventArgs e)
        {
            CreatingTourRequest creatingTourRequest = new CreatingTourRequest(LoggedInUser);
            Close();
            creatingTourRequest.Show();
        }

        private void OpenTourRequestsStatistic(object sender, RoutedEventArgs e)
        {
            TourRequestsStatistic tourRequestsStatistic = new TourRequestsStatistic(LoggedInUser);
            Close();
            tourRequestsStatistic.Show();
        }

        private void OpenAllTourRequests(object sender, RoutedEventArgs e)
        {
            AllTourRequests tourRequestsStatistic = new AllTourRequests(LoggedInUser);
            Close();
            tourRequestsStatistic.Show();
        }

        private void OpenNotifications(object sender, RoutedEventArgs e)
        {
            Notifications notifications = new Notifications(LoggedInUser);
            Close();
            notifications.Show();

        }
        private void ShowReport(object sender, RoutedEventArgs e)
        {
            Report notifications = new Report();
            notifications.Show();

        }

    }
}
