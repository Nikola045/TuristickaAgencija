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
        private User LoggedUser { get; set; }

        public VouchersView(User user)
        {
            InitializeComponent();
            _repository = new(InjectorService.CreateInstance<IStorage<Voucher>>());
            LoggedUser = user;
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
            Guest2Overview guest2Overview = new Guest2Overview(LoggedUser);
            this.Close();
            guest2Overview.Show();
        }

        private void AddVouchers(object sender, RoutedEventArgs e)
        {
            _tourService.Refresh(LoggedUser.Id);
            List<Voucher> vouchers = new List<Voucher>();
            vouchers = _repository.GetAll();
            DataPanel.ItemsSource = vouchers;
        }

    }
}
