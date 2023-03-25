using Microsoft.Graph.Models;
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
using TravelAgency.Forms;
using TravelAgency.Model;
using TravelAgency.Repository;
using User = TravelAgency.Model.User;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerHome.xaml
    /// </summary>
    public partial class OwnerHome : Window
    {
        public User LoggedInUser { get; set; }
        private readonly HotelRepository hotelRepository;
        static int ClickAccommodationCount = 1;
        static int ClickMediaCount = 1;
        public OwnerHome(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            hotelRepository = new HotelRepository();
        }

        public OwnerHome() { }

        private void OpenOwnerForm(object sender, RoutedEventArgs e)
        {
            OwnerForm createOwnerForm = new OwnerForm();
            createOwnerForm.ShowDialog();
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

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = new List<Hotel>();
            hotels = hotelRepository.ReadFromHotelsCsv();
            DataPanel.ItemsSource = hotels;
        }

        private void AccommodationDrop(object sender, RoutedEventArgs e)
        {
            GradeGuestBTN.Visibility = Visibility.Hidden;
            ReviewFormBTN.Visibility = Visibility.Hidden;
            ForumsBTN.Visibility = Visibility.Hidden;

            if (ClickAccommodationCount % 2 != 0)
            {
                CreateNewAccommodationBTN.Visibility = Visibility.Visible;
                StatisticOfAccommodationsBTN.Visibility = Visibility.Visible;
                MakeRenovationRequestBTN.Visibility = Visibility.Visible;
                ShowFutureRenovationBTN.Visibility = Visibility.Visible;
            }
            else
            {
                CreateNewAccommodationBTN.Visibility = Visibility.Hidden;
                StatisticOfAccommodationsBTN.Visibility = Visibility.Hidden;
                MakeRenovationRequestBTN.Visibility = Visibility.Hidden;
                ShowFutureRenovationBTN.Visibility = Visibility.Hidden;
            }
            ClickAccommodationCount++;
        }

        private void MediaDrop(object sender, RoutedEventArgs e)
        {
            CreateNewAccommodationBTN.Visibility = Visibility.Hidden;
            StatisticOfAccommodationsBTN.Visibility = Visibility.Hidden;
            MakeRenovationRequestBTN.Visibility = Visibility.Hidden;
            ShowFutureRenovationBTN.Visibility = Visibility.Hidden; 

            if (ClickMediaCount % 2 != 0)
            {
                GradeGuestBTN.Visibility = Visibility.Visible;
                ReviewFormBTN.Visibility = Visibility.Visible;
                ForumsBTN.Visibility = Visibility.Visible;
            }
            else
            {
                GradeGuestBTN.Visibility = Visibility.Hidden;
                ReviewFormBTN.Visibility = Visibility.Hidden;
                ForumsBTN.Visibility = Visibility.Hidden;
            }
            ClickMediaCount++;
        }

        private void OpenProfil(object sender, RoutedEventArgs e)
        {
            OwnerProfil profil = new OwnerProfil(LoggedInUser);
            profil.ShowDialog();
        }
    }
}
