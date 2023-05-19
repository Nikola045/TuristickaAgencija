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

        public VouchersView()
        {
            InitializeComponent();
            _repository = new(InjectorService.CreateInstance<IStorage<Voucher>>());
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            List<Voucher> vouchers = new List<Voucher>();
            vouchers = _repository.GetAll();
            DataPanel.ItemsSource = vouchers;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    
    }
}
