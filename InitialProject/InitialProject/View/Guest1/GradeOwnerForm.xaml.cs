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
using Microsoft.Win32;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository;
using TravelAgency.Services;
using TravelAgency.Domain.Model;
using TravelAgency.Repository.HotelRepo;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for GradeOwnerForm.xaml
    /// </summary>
    public partial class GradeOwnerForm : Window
    {
        private readonly OwnerGradeRepository ownerGradeRepository;
        private readonly ReservationRepository reservationRepository;
        private readonly HotelRepository hotelRepository;
        private readonly GradeService gradeService;
        public GradeOwnerForm()
        {
            InitializeComponent();
            Title = "Grade owner";
            DataContext = this;
            ownerGradeRepository = new OwnerGradeRepository();
            reservationRepository = new ReservationRepository();
            hotelRepository = new HotelRepository();
            gradeService = new GradeService();
        }

        private string imagePath;

        private string imageUrl;

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            txtImg.Text = file.FileName;
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            string imageUrl = txtImg.Text;

            if (!string.IsNullOrEmpty(imageUrl))
            {
                ListViewItem item = new ListViewItem
                {
                    Content = imageUrl
                };

                ListViewImg.Items.Add(item);

                txtImg.Text = "";
            }
        }

        private void Grade(object sender, RoutedEventArgs e)
        {

        }

        private void LoadHotels(object sender, RoutedEventArgs e)
        {
            List<string> hotelNames = new List<string>();
            List<Reservation> reservations = reservationRepository.GetAll();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.EndDate >= DateTime.Today.AddDays(-5) && reservation.EndDate <= DateTime.Today)
                {
                    if (!hotelNames.Contains(reservation.HotelName))
                    {
                        hotelNames.Add(reservation.HotelName);
                    }
                }
            }

            cbHotelName.ItemsSource = hotelNames;
        }

        private void btnDeleteImage_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewImg.SelectedItem != null)
            {
                ListViewImg.Items.Remove(ListViewImg.SelectedItem);
            }
        }

    }
}
