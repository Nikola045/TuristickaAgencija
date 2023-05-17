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

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for PastTours.xaml
    /// </summary>
    public partial class PastTours : Window
    {
        private readonly TourRepository _repository;
        private readonly TourService tourService;
        private const string FilePath = "../../../Resources/Data/tours.csv";
        User LogedUser = new User();
        private Tour selectedTour;

        public PastTours(User logedUser)
        {
            InitializeComponent();
            LogedUser = logedUser;
            _repository = new(InjectorService.CreateInstance<IStorage<Tour>>());
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
