using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.View.Guide;

namespace TravelAgency.View
{
    public partial class GuideOverview : Window
    {
        public GuideOverview()
        {
        }
        public User LoggedInUser { get; set; }

        public GuideOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
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
    }
}


