using System.Collections.Generic;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Guest2
{
    public partial class VouchersView : Window
    {

        private readonly VoucherRepository _repository;
        private readonly TourService _tourService;
        private User LogedUser { get; set; }

        public VouchersView(User user)
        {
            InitializeComponent();
            _repository = new(InjectorService.CreateInstance<IStorage<Voucher>>());
            LogedUser = user;
            _tourService = new TourService();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            List<Voucher> vouchers = new List<Voucher>();
            vouchers = _repository.GetAll();
            DataPanel.ItemsSource = vouchers;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Guest2Overview guest2Overview = new Guest2Overview(LogedUser);
            this.Close();
            guest2Overview.Show();
        }

        private void AddVouchers(object sender, RoutedEventArgs e)
        {
            _tourService.Refresh(LogedUser.Id);
            List<Voucher> vouchers = new List<Voucher>();
            vouchers = _repository.GetAll();
            DataPanel.ItemsSource = vouchers;
        }
        private void OpenGuestOverview(object sender, RoutedEventArgs e)
        {
            Guest2Overview createGuest2Form = new Guest2Overview(LogedUser);
            Close();
            createGuest2Form.Show();
        }
        private void OpenGuest2Form(object sender, RoutedEventArgs e)
        {
            Guest2Form createGuest2Form = new Guest2Form(LogedUser);
            Close();
            createGuest2Form.Show();
        }
        private void OpenComplexTourRequests(object sender, RoutedEventArgs e)
        {
            ComplexTourRequests createGuest2Form = new ComplexTourRequests(LogedUser);
            Close();
            createGuest2Form.Show();
        }

        private void OpenGuestOnTour(object sender, RoutedEventArgs e)
        {
            GuestOnTour createGuestOnTour = new GuestOnTour(LogedUser);
            Close();
            createGuestOnTour.Show();
        }

        private void OpenVouchers(object sender, RoutedEventArgs e)
        {
            VouchersView createVouchers = new VouchersView(LogedUser);
            Close();
            createVouchers.Show();
        }

        private void OpenTourReviews(object sender, RoutedEventArgs e)
        {
            PastTours pastTours = new PastTours(LogedUser);
            Close();
            pastTours.Show();
        }
        private void OpenCreateTourRequest(object sender, RoutedEventArgs e)
        {
            CreatingTourRequest creatingTourRequest = new CreatingTourRequest(LogedUser);
            Close();
            creatingTourRequest.Show();
        }

        private void OpenTourRequestsStatistic(object sender, RoutedEventArgs e)
        {
            TourRequestsStatistic tourRequestsStatistic = new TourRequestsStatistic(LogedUser);
            Close();
            tourRequestsStatistic.Show();
        }

        private void OpenAllTourRequests(object sender, RoutedEventArgs e)
        {
            AllTourRequests tourRequestsStatistic = new AllTourRequests(LogedUser);
            Close();
            tourRequestsStatistic.Show();
        }

        private void OpenNotifications(object sender, RoutedEventArgs e)
        {
            Notifications notifications = new Notifications(LogedUser);
            notifications.Show();
        }
    }
}
