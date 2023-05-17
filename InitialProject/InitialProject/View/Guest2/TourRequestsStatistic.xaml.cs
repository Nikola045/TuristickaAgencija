using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Repository;

namespace TravelAgency.View.Guest2
{
    public partial class TourRequestsStatistic : Window
    {
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
        }

    }
}
