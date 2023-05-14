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
    /// Interaction logic for AccountSettingsPage.xaml
    /// </summary>
    public partial class AccountSettingsPage : Page
    {
        private User LoggedInUser { get; set; }
        public AccountSettingsPage(User user)
        {
            LoggedInUser = user;
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
    }
}
