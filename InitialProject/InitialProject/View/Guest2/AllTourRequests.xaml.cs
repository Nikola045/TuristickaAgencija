using System.Collections.Generic;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Services;

namespace TravelAgency.View.Guest2
{
    public partial class AllTourRequests : Window
    {

        private readonly TourService tourService;
        User LogedUser = new User();

        public AllTourRequests(User user)
        {
            InitializeComponent();
            LogedUser = user;
            tourService = new TourService();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            List<TourRequests> requests  = tourService.MyRequests(LogedUser.Id);

            DataPanel.ItemsSource = requests;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }




}
