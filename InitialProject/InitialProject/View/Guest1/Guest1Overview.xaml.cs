using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TravelAgency.Domain.Model;
using TravelAgency.Forms;
using TravelAgency.Repository;
using TravelAgency.View.Guest1;
using TravelAgency.View.Guest2;

namespace TravelAgency.View
{
    public partial class Guest1Overview : Window
    {
        public User LoggedInUser { get; set; }
        public Guest1Overview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
        }

        private void OpenGuest1Form(object sender, RoutedEventArgs e)
        {
            Guest1Form page = new Guest1Form();
            ShowPage.Content = page;
        }

        private void OpenReserveForm(object sender, RoutedEventArgs e)
        {
            ReservationForm page = new ReservationForm(LoggedInUser);
            ShowPage.Content = page;
        }

        private void OpenAccount(object sender, RoutedEventArgs e)
        {
            Guest1AccountForm createAccountForm = new Guest1AccountForm(LoggedInUser);
            createAccountForm.Show();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            string language1 = "English";
            string language2 = "Serbian";
            cbLanguage.Items.Add(language1);
            cbLanguage.Items.Add(language2);
            cbLanguage.Text = language1;

        }
    }
}

