using Microsoft.Graph.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Domain.Model;
using TravelAgency.Forms;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerHome.xaml
    /// </summary>
    public partial class OwnerHome : Window
    {
        public User LoggedInUser { get; set; }
        public Hotel SelectedHotel { get; set; }
        private readonly HotelService hotelService;
        private ReservationService reservationService;
        static int ClickAccommodationCount = 1;
        static int ClickMediaCount = 1;
        public OwnerHome(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;    
            hotelService = new HotelService();
            reservationService = new ReservationService();
        }

        public OwnerHome() { }

        private void OpenOwnerForm(object sender, RoutedEventArgs e)
        {
            OwnerForm createOwnerForm = new OwnerForm(LoggedInUser);
            createOwnerForm.Show();
        }

        private void OpenGradeForm(object sender, RoutedEventArgs e)
        {
            GradePage page = new GradePage();
            ShowSmallPage.Content = page;
        }

        private void OpenMoveReservation(object sender, RoutedEventArgs e)
        {
            MoveReservationPage page = new MoveReservationPage();
            ShowBigPage.Content = page;
        }

        private void OpenReviewForm(object sender, RoutedEventArgs e)
        {
            ReviewPage page = new ReviewPage(LoggedInUser);
            ShowSmallPage.Content = page;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            reservationService.IsHotelRenovated();
            List<Hotel> hotels = new List<Hotel>();
            hotels = hotelService.GetHotelByOwner(LoggedInUser.Username);
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

        private void OpenGallery(object sender, RoutedEventArgs e)
        {
            SelectedHotel = (Hotel)DataPanel.SelectedItem;
            HotelGalery openHotelGalery = new HotelGalery(SelectedHotel);
            openHotelGalery.Show();
        }

        private void HomeButton(object sender, RoutedEventArgs e)
        {
            ShowBigPage.Content = null;
            ShowSmallPage.Content = null;
        }

        private void OpenStatistic(object sender, RoutedEventArgs e)
        {
            StatisticPage page = new StatisticPage();
            ShowSmallPage.Content = page;
        }

        private void OpenRenovation(object sender, RoutedEventArgs e)
        {
            RenovationPage page = new RenovationPage(LoggedInUser);
            ShowSmallPage.Content = page;
        }

        private void OpenRenovationReview(object sender, RoutedEventArgs e)
        {
            RenovationReview page = new RenovationReview();
            ShowSmallPage.Content = page;
        }
    }
}
