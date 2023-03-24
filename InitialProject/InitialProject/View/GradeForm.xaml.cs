using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using TravelAgency.Forms;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View
{
    public partial class GradeForm : Window
    {
        private const string FilePath = "../../../Resources/Data/reservations.csv";

        private readonly GradeGuest1Repository gradeGuest1Repository; 

        private readonly ReservationRepository reservationRepository;

        public GradeForm()
        {
            InitializeComponent();
            Title = "Create new comment";
            DataContext = this;
            gradeGuest1Repository = new GradeGuest1Repository();
            reservationRepository = new ReservationRepository();
        }

        private void GusetLoaded(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = new List<Reservation>();
            
            reservations = reservationRepository.ReadFromReservationsCsv(FilePath);

            for (int i = 0; i < reservations.Count; i++)
            {
                DateTime dateTimeNow = DateTime.Now;
                if (reservations[i].EndDate < dateTimeNow && reservations[i].EndDate.AddDays(5) > dateTimeNow)
                {
                    string reservationForm = reservations[i].Id.ToString() + " " + reservations[i].GuestUserName;
                    GuestsCB.Items.Add(reservationForm);
                }
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
            GuestGrade newGrade = new GuestGrade(
                GuestsCB.Text,
                Convert.ToInt32(CB1.Text),
                Convert.ToInt32(CB2.Text),
                CommentText.Text);
            gradeGuest1Repository.Save(newGrade);

            CommentText.Clear();
            object selectedItem = GuestsCB.SelectedItem;
            Reservation oldReservation = new Reservation();
            int id;
            string line = selectedItem.ToString();
            string[] fields = line.Split(' ');
            id = Convert.ToInt32(fields[0]);

            oldReservation = reservationRepository.FindReservationByID(id);
            if (GuestsCB.Items.Contains(selectedItem))
            {
                GuestsCB.Items.Remove(selectedItem);
                reservationRepository.Delete(oldReservation);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.ReadFromReservationsCsv(FilePath);

            for (int i = 0; i < reservations.Count; i++)
            {
                DateTime dateTimeNow = DateTime.Now;
                    if (reservations[i].EndDate < dateTimeNow && reservations[i].EndDate.AddDays(5) > dateTimeNow)
                    {
                        MessageBox.Show("You have " + (5 - (dateTimeNow.Day - reservations[i].EndDate.Day)).ToString() + " days left to grade " + reservations[i].GuestUserName);
                    }

                if (reservations[i].EndDate < dateTimeNow && reservations[i].EndDate.AddDays(5) < dateTimeNow)
                {
                    reservationRepository.Delete(reservations[i]);
                }
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
