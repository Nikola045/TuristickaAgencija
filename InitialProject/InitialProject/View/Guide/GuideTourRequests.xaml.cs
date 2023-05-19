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
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Guide
{
    public partial class GuideTourRequests : Window
    {
        private readonly TourService tourService;
        private readonly TourRequestsRepository tourRequestsRepository;
        TourRequests CurrentTourRequest = new TourRequests();
        User LogedUser = new User();
        public GuideTourRequests(User user)
        {
            InitializeComponent();
            LogedUser = user;
            tourService = new TourService();
            InitializeComponent();
            tourRequestsRepository = new(InjectorService.CreateInstance<IStorage<TourRequests>>());
        }

        private void DataPanel_Loaded(object sender, RoutedEventArgs e)
        {
            List<TourRequests> requests = tourService.MyRequests(LogedUser.Id);

            DataPanel.ItemsSource = requests;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            DataPanel.ItemsSource = tourService.FindTourRequest(DateStart.Text,DateEnd.Text,City.Text,Country.Text,NumberOfGuests.Text,Language.Text);
        }

        private void AcceptTour_Click(object sender, RoutedEventArgs e)
        {
            TourRequests tour = new TourRequests();
            tour = DataPanel.SelectedItem as TourRequests;
            CurrentTourRequest = tour;
            CurrentTourRequest.Status = "Accepted";
            tourRequestsRepository.Update(CurrentTourRequest);
            MessageBox.Show("Tour Accepted");
        }
    }
}
