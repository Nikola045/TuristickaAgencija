﻿using System;
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

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for VisitedAccommodationsPage.xaml
    /// </summary>
    public partial class VisitedAccommodationsPage : Page
    {
        private readonly App app = (App)App.Current;
        private GradeGuest1Repository gradeGuest1Repository;
        private ReservationRepository reservationRepository;
        private HotelRepository hotelRepository;
        private readonly GradeService gradeService;
        private User LoggedInUser { get; set; }
        public VisitedAccommodationsPage(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
            DataContext = this;
            gradeGuest1Repository = app.GradeGuest1Repository;
            reservationRepository = app.ReservationRepository;
            hotelRepository = app.HotelRepository;
            gradeService = new GradeService();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            List<GuestGrade> guestGrades = gradeGuest1Repository.GetAll();
            List<object> filteredGrades = new List<object>();

            foreach (GuestGrade grade in guestGrades)
            {
                Reservation reservation = reservationRepository.Get(grade.ReservationId);

                if (reservation != null && gradeService.IsOwnerGradeExists(reservation.Id))
                {
                    var obj = new
                    {
                        OwnerOf = reservation.HotelName,
                        Cleanliness = grade.Cleanliness,
                        Politeness = grade.Respecting,
                        Comment = grade.CommentText,
                        ReservationId = grade.ReservationId
                    };
                    filteredGrades.Add(obj);
                }
            }

            DataPanel.ItemsSource = filteredGrades;
            ExpandColumns(DataPanel);
        }

        private bool isVisitedLoaded = false;

        private void LoadVisited(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            List<object> filteredGrades = new List<object>();

            foreach (Reservation reservation in reservations)
            {
                Hotel hotel = hotelRepository.GetByHotelName(reservation.HotelName);

                if (hotel != null)
                {
                    var obj = new
                    {
                        HotelName = hotel.Name,
                        City = hotel.City,
                        Country = hotel.Country,
                        Type = hotel.TypeOfHotel,
                        NumberOfGuests = reservation.NumberOfGuests,
                        NumberOfDays = reservation.NumberOfDays,
                        IsRated = reservation.GradeStatus,
                    };
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
            RemoveLastColumns(ShowVisited, 7);
            ExpandColumns(ShowVisited);
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

        private void RemoveLastColumns(DataGrid dataGrid, int count)
        {
            int columnCount = dataGrid.Columns.Count;
            int startIndex = columnCount - count;

            // Provera da li ima dovoljno kolona za uklanjanje
            if (startIndex >= 0)
            {
                for (int i = columnCount - 1; i >= startIndex; i--)
                {
                    dataGrid.Columns.RemoveAt(i);
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
                GradeOwnerForm gradeOwnerPage = new GradeOwnerForm(LoggedInUser);
                NavigationService.Navigate(gradeOwnerPage);
            }
        }


        private void IsRated_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBlock = sender as TextBlock;
            var item = textBlock.DataContext;

            var propertyInfo = item.GetType().GetProperty("IsRated");
            var isRatedValue = propertyInfo?.GetValue(item);

            if (isRatedValue != null && isRatedValue.ToString() == "NotGraded")
            {
                GradeOwnerForm gradeOwnerPage = new GradeOwnerForm(LoggedInUser);
                NavigationService.Navigate(gradeOwnerPage);
            }
        }

    }
}