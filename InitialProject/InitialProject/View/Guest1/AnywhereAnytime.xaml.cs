using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.Security;
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for AnywhereAnytime.xaml
    /// </summary>
    public partial class AnywhereAnytime : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public User LoggedInUser { get; set; }
        public AnywhereAnytime(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            DataContext = this;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            AnywhereAnytimeRecommendation page = new AnywhereAnytimeRecommendation(LoggedInUser);
            NavigationService.Navigate(page);
        }

        private bool isDate1Valid = true;
        private bool isDate2Valid = true;
        private bool isTextBoxesFilled = false;

        private void ValidateTextBoxesAndDates()
        {
            bool isTextBox1Filled = !string.IsNullOrEmpty(txtNumberOfGuests.Text);
            bool isTextBox2Filled = !string.IsNullOrEmpty(txtNumberOfDays.Text);

            isTextBoxesFilled = isTextBox1Filled && isTextBox2Filled;

            bool isDate1Selected = Date1.SelectedDate.HasValue;
            bool isDate2Selected = Date2.SelectedDate.HasValue;

            bool isButtonEnabled = false;

            if (!isTextBoxesFilled)
            {
                isButtonEnabled = false;
            }
            else if (!isDate1Selected && !isDate2Selected)
            {
                isButtonEnabled = true;
            }
            else if (!isDate1Selected || !isDate2Selected)
            {
                isButtonEnabled = false;
            }
            else
            {
                DateTime today = DateTime.Now.Date;
                isDate1Valid = Date1.SelectedDate.Value.Date >= today;
                isDate2Valid = Date2.SelectedDate.Value.Date >= today;

                if (isDate1Valid && isDate2Valid && Date1.SelectedDate <= Date2.SelectedDate)
                {
                    isButtonEnabled = true;
                }
                else
                {
                    isButtonEnabled = false;
                }
            }

            btnReserve.IsEnabled = isButtonEnabled;
            dateValidationBefore.Visibility = isDate1Valid ? Visibility.Collapsed : Visibility.Visible;
            dateValidationBefore.Visibility = isDate2Valid ? Visibility.Collapsed : Visibility.Visible;

            if (isDate1Valid && isDate2Valid)
            {
                if (Date1.SelectedDate.HasValue && Date2.SelectedDate.HasValue && Date1.SelectedDate.Value > Date2.SelectedDate.Value)
                {
                    dateValidation.Visibility = Visibility.Visible;
                }
                else
                {
                    dateValidation.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                dateValidation.Visibility = Visibility.Collapsed;
            }
        }

        private void ShowDateValidationMessages()
        {
            DateTime today = DateTime.Now.Date;

            if (Date1.SelectedDate.HasValue)
            {
                if (Date1.SelectedDate.Value.Date < today)
                {
                    isDate1Valid = false;
                    dateValidationBefore.Visibility = Visibility.Visible;
                }
                else
                {
                    isDate1Valid = true;
                    dateValidationBefore.Visibility = Visibility.Collapsed;
                }
            }

            if (Date2.SelectedDate.HasValue)
            {
                if (Date2.SelectedDate.Value.Date < today)
                {
                    isDate2Valid = false;
                    dateValidationBefore.Visibility = Visibility.Visible;
                }
                else
                {
                    isDate2Valid = true;
                    dateValidationBefore.Visibility = Visibility.Collapsed;
                }
            }

            if (isDate1Valid && isDate2Valid)
            {
                if (Date1.SelectedDate.HasValue && Date2.SelectedDate.HasValue && Date1.SelectedDate.Value > Date2.SelectedDate.Value)
                {
                    dateValidation.Visibility = Visibility.Visible;
                }
                else
                {
                    dateValidation.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                dateValidation.Visibility = Visibility.Collapsed;
            }
        }

        private void Date1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateTextBoxesAndDates();
            ShowDateValidationMessages();
        }

        private void Date2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateTextBoxesAndDates();
            ShowDateValidationMessages();
        }

        private void txtNumberOfGuests_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBoxesAndDates();
        }

        private void txtNumberOfDays_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBoxesAndDates();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Inlines.Add(new Run("Hello " + LoggedInUser.Username + ", here are some recommended accommodations based only on number of guests that are going, number of days that you are "));
            textBlock.Inlines.Add(new Run("\nplanning to stay and period of time(optional)."));
            textBlock.Inlines.Add(new Run("\n\nLets see what we have for you!"));
            Label.Content = textBlock;
        }
    }
}
