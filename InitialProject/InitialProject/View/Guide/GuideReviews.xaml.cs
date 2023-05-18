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
    /// <summary>
    /// Interaction logic for GuideReviews.xaml
    /// </summary>
    public partial class GuideReviews : Window
    {
        public Tour SelectedTour { get; set; }

        private readonly GuideReviewRepository guideReviewRepository;
        public GuideReviews(Tour tour)
        {
            guideReviewRepository = new(InjectorService.CreateInstance<IStorage<TourReview1>>());
            SelectedTour = tour;
            InitializeComponent();
        }

        private void Report(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have succesfully reported review");
        }

        private void DataPanel_Loaded(object sender, RoutedEventArgs e)
        {
            List<TourReview1> List = new List<TourReview1>();
            foreach (var item in guideReviewRepository.GetAll())
            {
                if (SelectedTour.Id == item.Tour.Id)
                List.Add(item);
               
            }
            DataPanel.ItemsSource = List;
        }
    }
}
