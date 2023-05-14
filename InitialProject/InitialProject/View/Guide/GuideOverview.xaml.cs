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
using TravelAgency.Repository;
using TravelAgency.Repository.UserRepo;
using TravelAgency.View.Guide;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for GuideOverview.xaml
    /// </summary>
    public partial class GuideOverview : Window
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";
        private readonly App app = (App)App.Current;
        private readonly UserRepository userRepository;
        private readonly TourRepository tourRepository;
        public GuideOverview()
        {
        }
        public User LoggedInUser { get; set; }

        public GuideOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            tourRepository = new TourRepository();
            userRepository = app.UserRepository;
        }

        private void OpenGuideForm(object sender, RoutedEventArgs e)
        {
            GuideForm createGuideForm = new GuideForm();
            createGuideForm.Show();
        }

        private void OpenLiveTourForm(object sender, RoutedEventArgs e)
        {
            GuideLiveTour createLiveTourForm = new GuideLiveTour(LoggedInUser);
            createLiveTourForm.Show();
        }

        private void OpenTourStatisticForm(object sender, RoutedEventArgs e)
        {
            TourStatistic createTourStatistic = new TourStatistic();
            createTourStatistic.Show();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            DataPanel.ItemsSource = tourRepository.ReadFromToursCsv(FilePath);

        }

        private void DeleteTour(object sender, RoutedEventArgs e)
        {
            Tour SelectedTour = DataPanel.SelectedItem as Tour;
            tourRepository.Delete(SelectedTour);
            DataPanel.ItemsSource = tourRepository.ReadFromToursCsv(FilePath);
        }
        private void GetFired(object sender, RoutedEventArgs e)
        {
            userRepository.Delete(LoggedInUser);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }
    }
}


