﻿using Microsoft.Graph.Models;
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
        private void AccountSettingsClick(object sender, RoutedEventArgs e)
        {
            AccountSettingsPage page = new AccountSettingsPage(LoggedInUser);
            ShowSmallPage.Content = page;
            Image.Visibility = Visibility.Visible;
            Label.Visibility = Visibility.Visible;
            UsernameLabel.Visibility = Visibility.Collapsed;
        }

        private void ForumSettingsClick(object sender, RoutedEventArgs e)
        {
            ForumSettingsPage page = new ForumSettingsPage(LoggedInUser);
            ShowSmallPage.Content = page;
            Image.Visibility = Visibility.Collapsed;
            Label.Visibility = Visibility.Collapsed;
            UsernameLabel.Visibility = Visibility.Visible;
        }

        private void VisitedAccommodationsClick(object sender, RoutedEventArgs e)
        {
            VisitedAccommodationsPage page = new VisitedAccommodationsPage(LoggedInUser);
            ShowSmallPage.Content = page;
            Image.Visibility = Visibility.Collapsed;
            Label.Visibility = Visibility.Collapsed;
            UsernameLabel.Visibility = Visibility.Visible;
        }

        private void ActiveReservationsClick(object sender, RoutedEventArgs e)
        {
            ActiveReservationsPage page = new ActiveReservationsPage(LoggedInUser);
            ShowSmallPage.Content = page;
            Image.Visibility = Visibility.Collapsed;
            Label.Visibility = Visibility.Collapsed;
            UsernameLabel.Visibility = Visibility.Visible;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            AccountSettingsPage page = new AccountSettingsPage(LoggedInUser);
            ShowSmallPage.Content = page;
            Image.Visibility = Visibility.Visible;
            Label.Visibility = Visibility.Visible;
            UsernameLabel.Visibility = Visibility.Collapsed;
        }
    }
}
