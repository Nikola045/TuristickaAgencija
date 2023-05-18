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
using DevExpress.XtraEditors.Filtering;
using static TravelAgency.View.Guest1.GradeOwnerForm;
using System.Collections.ObjectModel;
using TravelAgency.Domain.RepositoryInterfaces;
using Microsoft.Kiota.Abstractions;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for GradeOwnerForm.xaml
    /// </summary>
    public partial class GradeOwnerForm : Page
    {
        private readonly OwnerGradeRepository ownerGradeRepository;
        private readonly ReservationRepository reservationRepository;
        public HotelRepository hotelRepository { get; }
        private readonly GradeService gradeService;
        private readonly HotelService hotelService;
        private readonly ReservationService reservationService;
        private readonly OwnerService ownerService;
        private User LogedUser { get; set; }
        public VisitedHotel SelectedItem{ get; set; }
        public ObservableCollection<VisitedHotel> Hotels { get; set; }

        public GradeOwnerForm(User user, VisitedHotel hotel)
        {
            InitializeComponent();
            Title = "Grade owner";
            DataContext = this;
            ownerGradeRepository = new(InjectorService.CreateInstance<IStorage<OwnerGrade>>());
            reservationRepository = new(InjectorService.CreateInstance<IStorage<Reservation>>());
            hotelRepository = new(InjectorService.CreateInstance<IStorage<Hotel>>());
            gradeService = new GradeService();
            hotelService = new HotelService();
            reservationService = new ReservationService();
            ownerService = new OwnerService();
            LogedUser = user;
            SelectedItem = hotel;
            Hotels = new ObservableCollection<VisitedHotel>(AddHotel());    
        }

        public List<VisitedHotel> AddHotel()
        {
            List<VisitedHotel> hotels = new List<VisitedHotel>();
            hotels.Add(SelectedItem);
            return hotels;  
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            
        }
        private void ExpandColumns(DataGrid dataGrid)
        {
            double totalWidth = dataGrid.ActualWidth;
            int columnCount = dataGrid.Columns.Count;

            if (columnCount > 0)
            {
                double columnWidth = totalWidth / columnCount;

                foreach (DataGridColumn column in dataGrid.Columns)
                {
                    column.Width = new DataGridLength(columnWidth);
                }
            }
        }
        private void btnPlus_Click(object sender, RoutedEventArgs e)
            {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            bool? result = openFileDialog.ShowDialog();

            if (result == true && !string.IsNullOrEmpty(openFileDialog.FileName))
            {
                BitmapImage image = new BitmapImage(new Uri(openFileDialog.FileName));

                ListViewImg.Items.Add(image);
            }
        }

        private void Grade(object sender, RoutedEventArgs e)
        {
            object selectedItem = cbHotelName.SelectedItem;
            Reservation reservation = new Reservation();
            
            int id;
            string line = selectedItem.ToString();
            string[] fields = line.Split(' ');
            id = Convert.ToInt32(fields[0]);
            string hotelName = fields[1];

            int hotelRating = 0;
            if (rbHotelOption1.IsChecked == true)
            {
                hotelRating = 1;
            }
            else if (rbHotelOption2.IsChecked == true)
            {
                hotelRating = 2;
            }
            else if (rbHotelOption3.IsChecked == true)
            {
                hotelRating = 3;
            }
            else if (rbHotelOption4.IsChecked == true)
            {
                hotelRating = 4;
            }
            else if (rbHotelOption5.IsChecked == true)
            {
                hotelRating = 5;
            }

            int ownerRating = 0;
            if (rbOwnerOption1.IsChecked == true)
            {
                ownerRating = 1;
            }
            else if (rbOwnerOption2.IsChecked == true)
            {
                ownerRating = 2;
            }
            else if (rbOwnerOption3.IsChecked == true)
            {
                ownerRating = 3;
            }
            else if (rbOwnerOption4.IsChecked == true)
            {
                ownerRating = 4;
            }
            else if (rbOwnerOption5.IsChecked == true)
            {
                ownerRating = 5;
            }

            List<string> images = new List<string>();
            foreach (object item in ListViewImg.Items)
            {
                if (item is string)
                {
                    images.Add(item as string);
                }
            }

            Hotel selectedOwnerUsername = hotelService.GetHotelByName(hotelName);

            OwnerGrade newGrade = new OwnerGrade(
                ownerService.GetOwnerByUsername(LogedUser.Username),
                ownerService.GetOwnerByUsername(selectedOwnerUsername.OwnerUsername),
                reservationService.FindReservationByID(id),
                hotelRating,
                ownerRating,
                txtComment.Text
            );
            ownerGradeRepository.Save(newGrade);

            RecommendationForRenovation recommendationForRenovation = new RecommendationForRenovation(LogedUser, hotelName, id);
            NavigationService.Navigate(recommendationForRenovation);
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
                        hotelNames.Add(reservation.Id.ToString() + " " + reservation.HotelName);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnGrade.IsEnabled = false;
        }
        private void EnableGradeButton()
        {
            bool isHotelOptionSelected = rbHotelOption1.IsChecked == true || rbHotelOption2.IsChecked == true || rbHotelOption3.IsChecked == true || rbHotelOption4.IsChecked == true || rbHotelOption5.IsChecked == true;
            bool isOwnerOptionSelected = rbOwnerOption1.IsChecked == true || rbOwnerOption2.IsChecked == true || rbOwnerOption3.IsChecked == true || rbOwnerOption4.IsChecked == true || rbOwnerOption5.IsChecked == true;
            bool isHotelNameSelected = cbHotelName.SelectedItem != null;
            bool isCommentEntered = !string.IsNullOrWhiteSpace(txtComment.Text);
 
            if (isHotelOptionSelected && isOwnerOptionSelected && isHotelNameSelected && isCommentEntered)
            {
                btnGrade.IsEnabled = true;
            }
            else
            {
                btnGrade.IsEnabled = false;
            }
        }

        private void rbHotelOption_Checked(object sender, RoutedEventArgs e)
        {
            EnableGradeButton();
        }

        private void rbOwnerOption_Checked(object sender, RoutedEventArgs e)
        {
            EnableGradeButton();
        }

        private void cbHotelName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableGradeButton();
        }

        private void txtComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableGradeButton();
        }

    }
}
