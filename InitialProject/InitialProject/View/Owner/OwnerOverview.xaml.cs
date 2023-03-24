using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.View;
using TravelAgency.View.Owner;

namespace TravelAgency.Forms
{
    public partial class OwnerOverview : Window
    {
        public User LoggedInUser { get; set; }
        public OwnerOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
        }

        public OwnerOverview(){}

        private void OpenOwnerForm(object sender, RoutedEventArgs e)
        {
            OwnerForm createOwnerForm = new OwnerForm();
            createOwnerForm.Show();
        }

        private void OpenGradeForm(object sender, RoutedEventArgs e)
        {
            GradeForm createGradeForm = new GradeForm();
            createGradeForm.Show();
        }

        private void OpenMoveReservation(object sender, RoutedEventArgs e)
        {
            MoveReservation createMoveReservation = new MoveReservation();
            createMoveReservation.Show();
        }

        private void OpenReviewForm(object sender, RoutedEventArgs e)
        {
            ReviewForm createReviewForm = new ReviewForm();
            createReviewForm.Show();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }
    }
}
