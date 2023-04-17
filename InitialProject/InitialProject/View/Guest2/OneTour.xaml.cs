using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TravelAgency.Repository;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for OneTour.xaml
    /// </summary>
    public partial class OneTour : Window
    {
        User LogedUser = new Domain.Model.User();
        const string FilePath = "../../../Resources/Data/tours.csv";

        public event PropertyChangedEventHandler PropertyChanged;

        public Tour selectedTour;
        private readonly TourRepository _repository;

        public OneTour(User logedUser, Tour tour)
        {
            InitializeComponent();
            LogedUser = logedUser;
            selectedTour = tour;
            _repository = new TourRepository();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddPeopleOnSelectedTour(object sender, RoutedEventArgs e)
        {

            const string FilePath1 = "../../../Resources/Data/guestOnTour.csv";
            int numGuests = Convert.ToInt32(txtNumOfGuests.Text);

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

                        if (_repository.ReserveTour(selectedTour.Id, LogedUser, FilePath1, numGuests))
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
    }
}
