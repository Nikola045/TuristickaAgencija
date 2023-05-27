using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.UserRepo;
using TravelAgency.Services;
using TravelAgency.View.Guide;

namespace TravelAgency.View
{
    public partial class GuideOverview : Window
    {
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

            tourRepository = new(InjectorService.CreateInstance<IStorage<Tour>>());

            userRepository = new(InjectorService.CreateInstance<IStorage<User>>());
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
            DataPanel.ItemsSource = tourRepository.GetAll();

        }

        private void DeleteTour(object sender, RoutedEventArgs e)
        {
            Tour SelectedTour = DataPanel.SelectedItem as Tour;
            tourRepository.Delete(SelectedTour);
            DataPanel.ItemsSource = tourRepository.GetAll();
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

        private void OpenReviews(object sender, RoutedEventArgs e)
        {
            Tour SelectedTour = DataPanel.SelectedItem as Tour;
            GuideReviews createGuideReviewsForm = new GuideReviews(SelectedTour);
            createGuideReviewsForm.Show();
        }

        private void OpenTourRequestsForm(object sender, RoutedEventArgs e)
        {
            GuideTourRequests createGuideTourRequestsForm = new GuideTourRequests(LoggedInUser);
            createGuideTourRequestsForm.Show();
        }

        private void OpenTourRequestsStatisticsForm(object sender, RoutedEventArgs e)
        {
            GuideTourRequestStatistic createTourRequestStatisticsForm = new GuideTourRequestStatistic();
            createTourRequestStatisticsForm.Show();
        }
    }
}


