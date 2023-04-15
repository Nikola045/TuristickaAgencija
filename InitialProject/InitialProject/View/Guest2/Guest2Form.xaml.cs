using Cake.Core.IO;
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
using TravelAgency.Forms;
using TravelAgency.Repository;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for Guest2Form.xaml
    /// </summary>
    public partial class Guest2Form : Window
    {
        private readonly TourRepository _repository;

        const string FilePath = "../../../Resources/Data/tours.csv";

        public event PropertyChangedEventHandler PropertyChanged;

        public Tour selectedTour;

        User LogedUser = new Domain.Model.User();

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
            _repository = new TourRepository();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            List<Tour> tours = new List<Tour>();
            const string FilePath = "../../../Resources/Data/tours.csv";
            tours = _repository.ReadFromToursCsv(FilePath);
            DataPanel.ItemsSource = tours;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            List<Tour> tours = new List<Tour>();
            
            tours = _repository.FilterTours(FilePath, txtCity.Text, txtCountry.Text, txtLeng.Text, txtDuration.Text, txtNum.Text);
            DataPanel.ItemsSource = tours;
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            const string FilePath = "../../../Resources/Data/tours.csv";
            List<Tour> tours = new List<Tour>();
            tours = _repository.ReadFromToursCsv(FilePath);
            DataPanel.ItemsSource = tours;
        }

        private void AddPeopleOnSelectedTour(object sender, RoutedEventArgs e)
        {
            selectedTour = (Tour)DataPanel.SelectedItem;
            const string FilePath1 = "../../../Resources/Data/guestOnTour.csv";
            int numGuests = Convert.ToInt32(txtNumOfGuests.Text);

            if (DataPanel.SelectedItem != null)
            {
                if (numGuests <= 0)
                {
                    MessageBox.Show("Please select how many guests want to go on the tour.");
                }
                else
                {
                    if (selectedTour.MaxNumberOfGuests < selectedTour.CurentNumberOfGuests + numGuests)
                    {
                        MessageBox.Show("Selected tour doesn't have that many free places." +
                            "Here are some similar tours for that many people.");
                    }
                    else
                    {

                        if (_repository.ReserveTour(selectedTour.Id, LogedUser.Id, FilePath1, numGuests))
                        {
                            selectedTour.CurentNumberOfGuests = selectedTour.CurentNumberOfGuests + numGuests;
                            _repository.Update(selectedTour);

                            MessageBox.Show("Reserved.");
                        }
                        else
                        {
                            MessageBox.Show("Not reserved.");
                        }
                        
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a tour you want to go on.");
            }
        }
    }
}
