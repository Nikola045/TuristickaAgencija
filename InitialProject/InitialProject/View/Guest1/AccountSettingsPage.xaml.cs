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
using TravelAgency.Services;
using User = TravelAgency.Domain.Model.User;
namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccountSettingsPage.xaml
    /// </summary>
    public partial class AccountSettingsPage : Page
    {
        private readonly Guest1Service guest1Service;
        private User LoggedInUser { get; set; }

        public AccountSettingsPage(User user)
        {
            LoggedInUser = user;
            guest1Service = new Guest1Service();
            InitializeComponent();
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            App.Current.Windows[0].Close();
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            Date1.SelectedDate = new DateTime(2000, 5, 15);
        }

        private void SuperGuestLabel_Loaded(object sender, RoutedEventArgs e)
        {
            string guestStatus = guest1Service.GetGuestStatus(LoggedInUser.Username);
            SuperGuestLabel.Content = guestStatus;
        }

        private void BonusPointsLabel_Loaded(object sender, RoutedEventArgs e)
        {
            int bonusPoints = guest1Service.GetBonusPoints(LoggedInUser.Username);
            BonusPointsLabel.Content = bonusPoints.ToString();
        }
    }
}

