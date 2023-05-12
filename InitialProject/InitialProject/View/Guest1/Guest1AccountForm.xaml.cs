using Microsoft.Graph.Models;
using Microsoft.Graph.Models.Security;
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
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for Guest1AccountForm.xaml
    /// </summary>
    public partial class Guest1AccountForm : Page
    {
        private User LoggedInUser { get; set; }
        public Guest1AccountForm(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MoveReservationForm moveReservationForm = new MoveReservationForm(LoggedInUser);
            moveReservationForm.Show();
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            App.Current.Windows[0].Close();
        }

        private void AccountSettingsClick(object sender, RoutedEventArgs e)
        {
            Guest1AccountForm page = new Guest1AccountForm(LoggedInUser);
            NavigationService.Navigate(page);
        }
    }
}
