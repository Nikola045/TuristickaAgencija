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
using Microsoft.Graph.Models.Security;
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for ForumSettingsPage.xaml
    /// </summary>
    public partial class ForumSettingsPage : Page
    {
        private User LoggedInUser { get; set; }
        public ForumSettingsPage(User user)
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
    }
}
