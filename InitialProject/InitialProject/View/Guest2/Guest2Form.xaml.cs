using Cake.Core.IO;
using Microsoft.Graph.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Forms;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for Guest2Form.xaml
    /// </summary>
    public partial class Guest2Form : Window
    {
        private readonly TourRepository _repository;
        private readonly TourService tourService;

        public event PropertyChangedEventHandler PropertyChanged;

        public Tour selectedTour;

        User LogedUser = new User();

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Guest2Form(User logedUser)
        {
            InitializeComponent();
            LogedUser = logedUser;
            Title = "Search tours";
            DataContext = this;
            _repository = new(InjectorService.CreateInstance<IStorage<Tour>>());
            tourService = new TourService();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            List<Tour> tours = new List<Tour>();
            tours = _repository.GetAll();
            DataPanel.ItemsSource = tours;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            List<Tour> tours = new List<Tour>();
            
            tours = tourService.FilterTours(txtCity.Text, txtCountry.Text, txtLeng.Text, txtDuration.Text, txtNum.Text);
            DataPanel.ItemsSource = tours;
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            List<Tour> tours = new List<Tour>();
            tours = _repository.GetAll();
            DataPanel.ItemsSource = tours;
        }



        private void ShowSelectedTour(object sender, RoutedEventArgs e)
        {
            if (DataPanel.SelectedItem != null)
            {
                selectedTour = (Tour)DataPanel.SelectedItem;
                OneTour createOneTour = new OneTour(LogedUser, selectedTour);
                createOneTour.Show();
            }
            else
            {
                MessageBox.Show("Please select a tour you want to see.");
            }
        }
    }
}
