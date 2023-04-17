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
using TravelAgency.Repository;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for GuestOnTour.xaml
    /// </summary>
    public partial class GuestOnTour : Window
    {
        User LogedUser = new Domain.Model.User();

        const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly TourRepository _repository;
        public GuestOnTour(User logedUser)
        {
            InitializeComponent();
            LogedUser = logedUser;
            _repository = new TourRepository();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        { 
            List<Tour> tours = new List<Tour>();
            tours = _repository.GetMyTours(FilePath, LogedUser.Id);
            DataPanel.ItemsSource = tours;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
