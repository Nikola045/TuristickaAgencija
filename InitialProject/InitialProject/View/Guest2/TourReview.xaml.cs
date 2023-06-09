using Microsoft.Graph.Models.Security;
using System;
using System.Collections.Generic;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Guest2
{
    public partial class TourReview : Window
    {
        private readonly GuideReviewRepository _repository;

        User LogedUser = new User();
        public Tour selectedTour;
        public TourReview(User logedUser, Tour tour)
        {
            InitializeComponent();
            LogedUser = logedUser;
            DataContext = this;
            selectedTour = tour;
            _repository = new(InjectorService.CreateInstance<IStorage<TourReview1>>());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {

            this.Close();
          
        }

        private void Fill1(object sender, RoutedEventArgs e)
        {
            CB1.Items.Add("1");
            CB1.Items.Add("2");
            CB1.Items.Add("3");
            CB1.Items.Add("4");
            CB1.Items.Add("5");
        }

        private void Fill2(object sender, RoutedEventArgs e)
        {
            CB2.Items.Add("1");
            CB2.Items.Add("2");
            CB2.Items.Add("3");
            CB2.Items.Add("4");
            CB2.Items.Add("5");
        }

        private void Fill3(object sender, RoutedEventArgs e)
        {
            CB3.Items.Add("1");
            CB3.Items.Add("2");
            CB3.Items.Add("3");
            CB3.Items.Add("4");
            CB3.Items.Add("5");
        }

        private void MakeReview(object sender, RoutedEventArgs e)
        {
            TourReview1 tourReview = new TourReview1();
            tourReview.Id = _repository.NextId();
            tourReview.Tour.Id = selectedTour.Id;
            tourReview.GuidesKnowlege = Convert.ToInt32(CB1.SelectedItem);
            tourReview.GuidesLenguage = Convert.ToInt32(CB2.SelectedItem);
            tourReview.Overall = Convert.ToInt32(CB3.SelectedItem);
            tourReview.Comment = txtComment.Text.ToString();
            _repository.Save(tourReview);
            MessageBox.Show("You have succesfully review this tour.");
        }
    }
}
