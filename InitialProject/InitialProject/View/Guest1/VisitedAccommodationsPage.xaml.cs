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
using TravelAgency.Domain.Model;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository;
using TravelAgency.Services;
using User = TravelAgency.Domain.Model.User;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;
using Microsoft.Graph.Models.Security;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for VisitedAccommodationsPage.xaml
    /// </summary>
    public partial class VisitedAccommodationsPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private GradeGuest1Repository gradeGuest1Repository;
        private ReservationRepository reservationRepository;
        private HotelRepository hotelRepository;
        private readonly GradeService gradeService;
        public ObservableCollection<VisitedHotel> VisitedHotels { get; set; }
        private User LoggedInUser { get; set; }
        public VisitedHotel SelectedHotel { get; set; }
        public VisitedAccommodationsPage(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            DataContext = this;
            gradeGuest1Repository = new(InjectorService.CreateInstance<IStorage<GuestGrade>>());
            reservationRepository = new(InjectorService.CreateInstance<IStorage<Reservation>>());
            hotelRepository = new(InjectorService.CreateInstance<IStorage<Hotel>>());
            gradeService = new GradeService();
            VisitedHotels = new ObservableCollection<VisitedHotel>(GetAll());
        }

        public List<VisitedHotel> GetAll()
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            List<VisitedHotel> filteredGrades = new List<VisitedHotel>();

            foreach (Reservation reservation in reservations)
            {
                Hotel hotel = hotelRepository.GetByHotelName(reservation.HotelName);

                if (hotel != null)
                {
                    VisitedHotel obj = new VisitedHotel
                    (
                        hotel.Name,
                        hotel.City,
                        hotel.Country,
                        hotel.TypeOfHotel,
                        reservation.NumberOfGuests,
                        reservation.NumberOfDays,
                        reservation.GradeStatus
                    );
                filteredGrades.Add(obj);
                }
            }


            return filteredGrades;
        }

    private void OnLoad(object sender, RoutedEventArgs e)
    {
      List<GuestGrade> guestGrades = gradeGuest1Repository.GetAll();
      List<object> filteredGrades = new List<object>();

      foreach (GuestGrade grade in guestGrades)
      {
          Reservation reservation = reservationRepository.Get(grade.Reservation.Id);

          if (reservation != null && gradeService.IsOwnerGradeExists(reservation.Id))
          {
              var obj = new
              {
                  OwnerOf = reservation.HotelName,
                  Cleanliness = grade.Cleanliness,
                  Politeness = grade.Respecting,
                  Comment = grade.CommentText
              };
              filteredGrades.Add(obj);
          }
      }

      DataPanel.ItemsSource = filteredGrades;
    }

    private bool isVisitedLoaded = false;

    private void LoadVisited(object sender, RoutedEventArgs e)
    {
    /*  List<Reservation> reservations = reservationRepository.GetAll();
      List<object> filteredGrades = new List<object>();

      foreach (Reservation reservation in reservations)
      {
          Hotel hotel = hotelRepository.GetByHotelName(reservation.HotelName);

          if (hotel != null)
          {
              VisitedHotel obj = new VisitedHotel
              (
                  hotel.Name,
                  hotel.City,
                  hotel.Country,
                  hotel.TypeOfHotel,
                  reservation.NumberOfGuests,
                  reservation.NumberOfDays,
                  reservation.GradeStatus
              );
              filteredGrades.Add(obj);
          }
      }

      ShowVisited.ItemsSource = filteredGrades;


      ShowVisited.Loaded += (s, args) =>
      {
          foreach (var item in ShowVisited.Items)
          {
              var container = ShowVisited.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
              var isRatedTextBlock = FindVisualChild<TextBlock>(container, "IsRatedTextBlock");

              if (isRatedTextBlock != null)
              {
                  var propertyInfo = item.GetType().GetProperty("IsRated");
                  var isRatedValue = propertyInfo?.GetValue(item);

                  if (isRatedValue != null && isRatedValue.ToString() == "NotGraded")
                  {
                      isRatedTextBlock.Foreground = Brushes.Blue;
                      isRatedTextBlock.TextDecorations = TextDecorations.Underline;
                  }
              }
          }
      };
      ExpandColumns(ShowVisited);*/
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

        private static T FindVisualChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    return typedChild;
                }

                var result = FindVisualChild<T>(child, childName);
                if (result != null) return result;
            }

            return null;
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var item = textBlock.DataContext;

            var propertyInfo = item.GetType().GetProperty("IsRated");
            var isRatedValue = propertyInfo?.GetValue(item);

            if (isRatedValue != null && isRatedValue.ToString() == "NotGraded")
            {
                VisitedHotel SelectedItem = SelectedHotel;
                GradeOwnerForm gradeOwnerPage = new GradeOwnerForm(LoggedInUser, SelectedItem);
                NavigationService.Navigate(gradeOwnerPage);
            }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VisitedHotel SelectedItem = SelectedHotel;
            GradeOwnerForm gradeOwnerPage = new GradeOwnerForm(LoggedInUser, SelectedItem);
            NavigationService.Navigate(gradeOwnerPage);
        }
    }
}
