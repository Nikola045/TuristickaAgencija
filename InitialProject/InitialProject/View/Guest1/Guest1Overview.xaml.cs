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
using Azure;
using Microsoft.Graph.Models.Security;
using TravelAgency.Domain.Model;
using TravelAgency.Forms;
using TravelAgency.Repository;
using TravelAgency.View.Guest1;
using TravelAgency.View.Guest2;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TravelAgency.View
{
    public partial class Guest1Overview : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public User LoggedInUser { get; set; }
        public Guest1Overview(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            DataContext = this;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            Guest1AccountForm page = new Guest1AccountForm(LoggedInUser);
            ShowPage.Content = page;
        }
        private void OpenForum(object sender, RoutedEventArgs e)
        {
            ForumPage page = new ForumPage(LoggedInUser);
            ShowPage.Content = page;
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Guest1HomePage page = new Guest1HomePage(LoggedInUser);
            ShowPage.Content = page;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Guest1HomePage page = new Guest1HomePage(LoggedInUser);
            ShowPage.Content = page;
        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }
        private void Facebook_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string url = "https://www.facebook.com";

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Facebook: " + ex.Message);
            }
        }

        private void Instagram_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string url = "https://www.instagram.com";

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Facebook: " + ex.Message);
            }
        }

        private void Twitter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string url = "https://www.twitter.com";

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Facebook: " + ex.Message);
            }
        }

        private void Facebook_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Facebook_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        private void Twitter_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Twitter_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        private void Instagram_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Instagram_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }        
    }
}

