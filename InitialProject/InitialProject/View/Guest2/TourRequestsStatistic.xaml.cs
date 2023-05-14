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
    /// Interaction logic for TourRequestsStatistic.xaml
    /// </summary>
    public partial class TourRequestsStatistic : Window
    {

        private readonly TourRequestsRepository _tourRequestsRepository;

        private const string FilePathTour = "../../../Resources/Data/tourRequests.csv";

        // public TourRequests selectedTourRequest { get; set; }

        public User LoggedInUser { get; set; }


        public TourRequestsStatistic(User loggedInUser)
        {
            InitializeComponent();
            LoggedInUser = loggedInUser;
        }


        private void PerYearRB_Checked(object sender, RoutedEventArgs e)
        {
            CB.IsEnabled = true;
        }



        private void AllTimeRB_Checked(object sender, RoutedEventArgs e)
        {
            CB.IsEnabled = false;

        }

        private void FillComboBox(object sender, RoutedEventArgs e)
        {
            CB.Items.Add("2023");
            CB.Items.Add("2022");
            CB.Items.Add("2021");
            CB.Items.Add("2020");
            CB.Items.Add("2019");
        }


        private void ShowStatisticButton_Click(object sender, RoutedEventArgs e)
        {
            /*selectedTour = (Tour)DataPanel.SelectedItem;
            int[] Info = new int[4];
            Info = tourRepository.ShowStatistic(selectedTour.Id);
            txt1.Text = Info[0].ToString();
            txt2.Text = Info[1].ToString();
            txt3.Text = Info[2].ToString();
            txt4.Text = Info[3].ToString();*/
        }

    }
}
