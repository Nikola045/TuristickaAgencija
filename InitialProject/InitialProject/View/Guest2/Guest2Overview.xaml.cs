using System.Windows;
using TravelAgency.Domain.Model;

namespace TravelAgency.View.Guest2
{
    public partial class Guest2Overview : Window
    {
        public Guest2Overview() {}
        public User LoggedInUser { get; set; }

        public Guest2Overview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
        }

        private void OpenGuest2Form(object sender, RoutedEventArgs e)
        {
            Guest2Form createGuest2Form = new Guest2Form(LoggedInUser);
            createGuest2Form.Show();
        }

        private void OpenGuestOnTour(object sender, RoutedEventArgs e)
        {
            GuestOnTour createGuestOnTour = new GuestOnTour(LoggedInUser);
            createGuestOnTour.Show();
        }

        private void OpenVouchers(object sender, RoutedEventArgs e)
        {
            VouchersView createVouchers = new VouchersView();
            createVouchers.Show();
        }

        private void OpenTourReviews(object sender, RoutedEventArgs e)
        {
            PastTours createTourReview = new PastTours(LoggedInUser);
            createTourReview.Show();
        }
        private void OpenCreateTourRequest(object sender, RoutedEventArgs e)
        {
            CreatingTourRequest creatingTourRequest = new CreatingTourRequest(LoggedInUser);
            creatingTourRequest.Show();
        }

        private void OpenTourRequestsStatistic(object sender, RoutedEventArgs e)
        {
            TourRequestsStatistic tourRequestsStatistic = new TourRequestsStatistic(LoggedInUser);
            tourRequestsStatistic.Show();

        }

        private void OpenAllTourRequests(object sender, RoutedEventArgs e)
        {
            AllTourRequests tourRequestsStatistic = new AllTourRequests(LoggedInUser);
            tourRequestsStatistic.Show();

        }
    }
}
