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
using System.Windows.Navigation;
using System.Windows.Shapes;
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for VisitedAccommodationsPage.xaml
    /// </summary>
    public partial class VisitedAccommodationsPage : Page
    {
        private User LoggedInUser { get; set; }
        public VisitedAccommodationsPage(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
        }
        private void AccountSettingsClick(object sender, RoutedEventArgs e)
        {
            Guest1AccountForm page = new Guest1AccountForm(LoggedInUser);
            NavigationService.Navigate(page);
        }

        private void ForumSettingsClick(object sender, RoutedEventArgs e)
        {
            ForumSettingsPage page = new ForumSettingsPage(LoggedInUser);
            NavigationService.Navigate(page);
        }

        private void VisitedAccommodationsClick(object sender, RoutedEventArgs e)
        {
            VisitedAccommodationsPage page = new VisitedAccommodationsPage(LoggedInUser);
            NavigationService.Navigate(page);
        }

        private void ActiveReservationsClick(object sender, RoutedEventArgs e)
        {
            ActiveReservationsPage page = new ActiveReservationsPage(LoggedInUser);
            NavigationService.Navigate(page);
        }

        private void ShowReview(object sender, RoutedEventArgs e)
        {
            Guest1ShowReview createShowReview = new Guest1ShowReview();
            createShowReview.Show();
        }

        private void GradeOwner(object sender, RoutedEventArgs e)
        {
            GradeOwnerForm gradeOwner = new GradeOwnerForm(LoggedInUser);
            gradeOwner.Show();
        }
    }
}
