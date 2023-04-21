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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository;
using TravelAgency.Services;
using TravelAgency.Domain.Model;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for GradePage.xaml
    /// </summary>
    public partial class GradePage : Page
    {
        private readonly App app = (App)App.Current;
        private readonly GradeGuest1Repository gradeGuest1Repository;
        private readonly ReservationRepository reservationRepository;
        private readonly GradeService gradeService;
        private readonly ReservationService reservationService;
        public GradePage()
        {
            InitializeComponent();
            Title = "Create new comment";
            DataContext = this;
            gradeGuest1Repository = app.GradeGuest1Repository;
            reservationRepository = app.ReservationRepository;
            gradeService = new GradeService();
            reservationService = new ReservationService();
        }

        private void GusetLoaded(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = new List<Reservation>();

            reservations = reservationRepository.GetAll();

            for (int i = 0; i < reservations.Count; i++)
            {
                if (gradeService.FindGuestsForGrade(i) != null)
                    GuestsCB.Items.Add(gradeService.FindGuestsForGrade(i));
            }
        }

        private void Fill(object sender, RoutedEventArgs e)
        {
            CB1.Items.Add("1");
            CB1.Items.Add("2");
            CB1.Items.Add("3");
            CB1.Items.Add("4");
            CB1.Items.Add("5");
        }

        private void Fill1(object sender, RoutedEventArgs e)
        {
            CB2.Items.Add("1");
            CB2.Items.Add("2");
            CB2.Items.Add("3");
            CB2.Items.Add("4");
            CB2.Items.Add("5");
        }

        private void SaveGrade_Click(object sender, RoutedEventArgs e)
        {
            object selectedItem = GuestsCB.SelectedItem;
            Reservation oldReservation = new Reservation();
            int id;
            string line = selectedItem.ToString();
            string[] fields = line.Split(' ');
            id = Convert.ToInt32(fields[0]);

            GuestGrade newGrade = new GuestGrade(
                GuestsCB.Text,
                Convert.ToInt32(CB1.Text),
                Convert.ToInt32(CB2.Text),
                CommentText.Text,
                id);
            gradeGuest1Repository.Save(newGrade);

            CommentText.Clear();
            oldReservation = reservationService.FindReservationByID(id);
            GuestsCB.Items.Remove(selectedItem);
            reservationService.LogicalDelete(oldReservation);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetAll();

            for (int i = 0; i < reservations.Count; i++)
            {
                if (gradeService.ShowMessageForGrade(i) != null)
                {
                    MessageBox.Show(gradeService.ShowMessageForGrade(i));
                }
                gradeService.FindAndLogicalDeleteExpiredReservation(i);
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

