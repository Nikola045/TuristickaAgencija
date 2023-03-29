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
using TravelAgency.Model;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerProfil.xaml
    /// </summary>
    public partial class OwnerProfil : Window
    {
        private User LogedOwner { get; set; }
        public OwnerProfil(User user)
        {
            InitializeComponent();
            LogedOwner = user;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            App.Current.Windows[0].Close();
            this.Close();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            UserNameLabel.Content = LogedOwner.Username;
        }
    }
}
