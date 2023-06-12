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
using TravelAgency.Services;
using User = TravelAgency.Domain.Model.User;
namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccountSettingsPage.xaml
    /// </summary>
    public partial class AccountSettingsPage : Page
    {
        private readonly Guest1Service guest1Service;
        private User LoggedInUser { get; set; }

        private readonly ReservationService reservationService;
        public AccountSettingsPage(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            guest1Service = new Guest1Service();
            reservationService = new ReservationService();
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            App.Current.Windows[0].Close();
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            Date1.SelectedDate = new DateTime(2000, 5, 15);
        }

        private void SuperGuestLabel_Loaded(object sender, RoutedEventArgs e)
        {
            string guestStatus = guest1Service.GetGuestStatus(LoggedInUser.Username);
            SuperGuestLabel.Content = guestStatus;
        }

        private void BonusPointsLabel_Loaded(object sender, RoutedEventArgs e)
        {
            int bonusPoints = guest1Service.GetBonusPoints(LoggedInUser.Username);
            BonusPointsLabel.Content = bonusPoints.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PdfReportMadeReservation page = new PdfReportMadeReservation(reservationService.ReservationInfoForPDF(Convert.ToDateTime(StartDate.Text), Convert.ToDateTime(EndDate.Text)));
            NavigationService.Navigate(page);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PdfReportCanceledReservation page = new PdfReportCanceledReservation(reservationService.CanceledReservationInfoForPDF(Convert.ToDateTime(StartDate.Text), Convert.ToDateTime(EndDate.Text)));
            NavigationService.Navigate(page);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StartDate.SelectedDateChanged += StartDate_SelectedDateChanged;
            EndDate.SelectedDateChanged += EndDate_SelectedDateChanged;
            UpdateButtonAvailability();
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonAvailability();
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonAvailability();
        }

        private void UpdateButtonAvailability()
        {
            bool isStartDateSelected = StartDate.SelectedDate.HasValue;
            bool isEndDateSelected = EndDate.SelectedDate.HasValue;
            bool isStartDateGreaterThanEndDate = false;

            if (isStartDateSelected && isEndDateSelected)
            {
                isStartDateGreaterThanEndDate = StartDate.SelectedDate.Value > EndDate.SelectedDate.Value;
            }

            MadeReservation.IsEnabled = isStartDateSelected && isEndDateSelected && !isStartDateGreaterThanEndDate;
            CanceledReservation.IsEnabled = isStartDateSelected && isEndDateSelected && !isStartDateGreaterThanEndDate;
        }

    }
}

