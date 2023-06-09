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
using System.Xml.Linq;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.Security;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Services;
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for RecommendationForRenovation.xaml
    /// </summary>
    public partial class RecommendationForRenovation : Page
    {
        private User LogedUser { get; set; }
        private readonly RenovationRecommendationRepository recommendationRepository;
        private readonly ReservationService reservationService;
        private readonly ReservationRepository reservationRepository;
        public string HotelName { get; set; }
        public int Id { get; set; }
        public RecommendationForRenovation(User user,string hotelName, int id)
        {
            InitializeComponent();
            LogedUser = user;
            HotelName = hotelName;
            Id = id;
            reservationService = new ReservationService();
            reservationRepository = new(InjectorService.CreateInstance<IStorage<Reservation>>());
            recommendationRepository = new(InjectorService.CreateInstance<IStorage<Recommendation>>());
        }
        private void Rate(object sender, RoutedEventArgs e)
        {
            int level = 0;
            if (rbOption1.IsChecked == true)
            {
                level = 1;
            }
            else if (rbOption2.IsChecked == true)
            {
                level = 2;
            }
            else if (rbOption3.IsChecked == true)
            {
                level = 3;
            }
            else if (rbOption4.IsChecked == true)
            {
                level = 4;
            }
            else if (rbOption5.IsChecked == true)
            {
                level = 5;
            }

            Recommendation newRecommendation = new Recommendation(
                recommendationRepository.NextId(),
                Id,
                HotelName,
                txtInfo.Text,
                level                
            );
            recommendationRepository.Save(newRecommendation);
            Reservation reservation = reservationService.FindReservationByID(Id);
            reservation.NumberOfRenovationRequest++;
            reservationRepository.Update(reservation);
            AccountSettingsPage page = new AccountSettingsPage(LogedUser);
            NavigationService.Navigate(page);
        }
    }
}
