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
using TravelAgency.Model;
using TravelAgency.Repository;
using User = TravelAgency.Model.User;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for ReviewForm.xaml
    /// </summary>
    public partial class ReviewForm : Window
    {
        OwnerGradeRepository ownerGradeRepository;
        ReservationRepository reservationRepository;
        
        private User LogedOwner { get; set; }
        public ReviewForm(User user)
        {
            InitializeComponent();
            reservationRepository = new ReservationRepository();
            ownerGradeRepository = new OwnerGradeRepository();
            LogedOwner = user;
        }

        public List<OwnerGrade> ShowReviews()
        {
            List<Reservation> allReservation = reservationRepository.ReadFromReservationsCsv();
            List<OwnerGrade> ownerGrades = new List<OwnerGrade>();
            foreach(Reservation reservation in allReservation)
            {
                if(reservation.GradeStatus == "Graded")
                {
                    if (ownerGradeRepository.IsGradeExists(reservation.Id))
                    {
                        ownerGrades.Add(ownerGradeRepository.FindByReservationId(reservation.Id));
                    }
                }
            }
            return ownerGrades;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowData(object sender, RoutedEventArgs e)
        {
            ReviewData.ItemsSource = ShowReviews();
        }
    }
}
