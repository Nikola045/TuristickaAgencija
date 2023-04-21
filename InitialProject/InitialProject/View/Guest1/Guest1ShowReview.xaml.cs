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
using TravelAgency.Domain.Model;
using TravelAgency.Repository;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for Guest1ShowReview.xaml
    /// </summary>
    public partial class Guest1ShowReview : Window
    {
        private readonly App app = (App)App.Current;
        private GradeGuest1Repository gradeGuest1Repository;
        private ReservationRepository reservationRepository;
        private readonly GradeService gradeService;


        public Guest1ShowReview()
        {
            InitializeComponent();
            Title = "Guest1 review";
            DataContext = this;
            gradeGuest1Repository = app.GradeGuest1Repository;
            reservationRepository = app.ReservationRepository;
            gradeService = new GradeService();
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            var reservations = reservationRepository.GetAll();
            var grades = gradeGuest1Repository.GetAll();

            var query = from grade in grades
                        join reservation in reservations on grade.ReservationId equals reservation.Id
                        where !gradeService.IsOwnerGradeExists(reservation.Id)
                        select new
                        {
                            GuestUserName = grade.GuestUserName,
                            AccommodationName = reservation.HotelName,
                            Cleanliness = grade.Cleanliness,
                            Respecting = grade.Respecting,
                            CommentText = grade.CommentText
                        };

            DataPanel.ItemsSource = query.ToList();
        }

    }
}
