
using Microsoft.Graph.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
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
using TravelAgency.Domain.Model;
using TravelAgency.Services;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for ComplexTourRequests.xaml
    /// </summary>
    public partial class ComplexTourRequests : Window
    {

        private readonly TourService tourService;
        User LoggedInUser = new User();

        public ComplexTourRequests(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            tourService = new TourService();
        }
        private void LoadData(object sender, RoutedEventArgs e)
        {
            //List<TourRequests> requests = tourService.MyComplexRequests(LogedUser.Id);

            //DataPanel.ItemsSource = requests;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Guest2Overview guest2Overview = new Guest2Overview(LoggedInUser);
            this.Close();
            guest2Overview.Show();

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
    }
}
