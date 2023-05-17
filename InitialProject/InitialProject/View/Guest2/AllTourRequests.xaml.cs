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
using TravelAgency.Services;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for AllTourRequests.xaml
    /// </summary>
    public partial class AllTourRequests : Window
    {

        private readonly TourService tourService;
        User LogedUser = new User();
        private Tour selectedTour;


        public AllTourRequests(User user)
        {
            InitializeComponent();
            LogedUser = user;
            tourService = new TourService();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            List<TourRequests> requests  = tourService.MyRequests(LogedUser.Id);

            DataPanel.ItemsSource = requests;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }




}
