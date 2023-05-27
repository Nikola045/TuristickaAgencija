using LiveCharts;
using LiveCharts.Wpf;
using OxyPlot.Series;
using OxyPlot.Wpf;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TravelAgency.Domain.Model;
using TravelAgency.Forms;
using TravelAgency.Services;
using PieSeries = LiveCharts.Wpf.PieSeries;

namespace TravelAgency.View.Owner
{
    public partial class StatisticPage : Page, INotifyPropertyChanged
    {
        private readonly ReservationService reservationService;
        private readonly HotelService hotelService;
        private string _xTitle;
        private List<string> _xLabels;
        private SeriesCollection _data;
        private string _hotelName;
        private int _year;
        private Frame ShowSmallFrame;
        public event PropertyChangedEventHandler? PropertyChanged;
        private User LogedUser { get; }
        public StatisticPage(User user, Frame frame)
        {
            InitializeComponent();
            DataContext = this;
            reservationService = new ReservationService();
            hotelService = new HotelService();
            LogedUser = user;
            DataChart = new SeriesCollection();
            ShowSmallFrame = frame;
        }

        private void YearStatistic(object sender, RoutedEventArgs e)
        {
            DataChart.Clear();
            DateTime dateTime = DateTime.Now;
            List<string> labels = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                labels.Add((dateTime.Year - 4 + i).ToString());
            }
            HideButton.IsEnabled = true;
            ShowAllButton.IsEnabled = true;
            ShowButton.IsEnabled = true;
            ResultButton.Visibility = Visibility.Hidden;
            Label1.Visibility = Visibility.Hidden;
            Label2.Visibility = Visibility.Hidden;
            YearCB.Visibility = Visibility.Hidden;
            XTitle = "Years";
            XLabels = labels;
        }

        private void MounthStatistic(object sender, RoutedEventArgs e)
        {
            DataChart.Clear();
            List<string> months = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            HideButton.IsEnabled = true;
            ShowAllButton.IsEnabled = false;
            ShowButton.IsEnabled = true;
            ResultButton.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            YearCB.Visibility = Visibility.Visible;
            PdfButton.IsEnabled = true;
            XTitle = "Months";
            XLabels = months;
        }

        private void OnPageLoad(object sender, RoutedEventArgs e)
        {
            //implementiraj preporuku

            //grupise ih po lokacijama
            //treba da uzme sve hotele i proveri im broj rezervacija

            //kada vidi gde ima najvise rezervacija po lokaciji predlozi kreiranje novog hotela
            //kada vidi da ima premalo rezervacija po lokaciji predlozi zatvaranje hotela

            //**gleda statistiku u proslih god dana

            //ovo ide ako prihvati kreiranje novog smestaja
            /*OwnerForm ownerForm = new OwnerForm(LogedUser);
            ownerForm.Country = "a";
            ownerForm.City = "b";
            ownerForm.Show();   */

        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {    
            HotelCB.ItemsSource = hotelService.FillForComboBoxHotels(LogedUser);
        }

        private void OnLoadYear(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            for(int i = 0; i<5; i++)
            {
                YearCB.Items.Add(dateTime.Year-i);
            }
        }

        private void ShowStatisticForHotel(object sender, RoutedEventArgs e)
        {
            if(XTitle == "Years")
            {
                for(int i = 0; i<4; i++)
                {
                    DataChart.Add(reservationService.ShowHotelReservationInChart(HotelName)[i]);
                }
                
            }
            else if(XTitle == "Months")
            {
                for (int i = 0; i < 4; i++)
                {
                    DataChart.Add(reservationService.ShowHotelReservationPerMonth(HotelName, YearForStatistic)[i]);
                }
            }
            else { }
            
        }

        private void HideStatisticForHotel(object sender, RoutedEventArgs e)
        {
            foreach(ColumnSeries column in DataChart)
            {
                if (column.Title.Split(":")[0] == HotelName)
                {
                    DataChart.Remove(column);
                }
            }
        }

        private void ShowAllStatistic(object sender, RoutedEventArgs e)
        {
            List<Hotel> hotels = hotelService.GetHotelByOwner(LogedUser.Username);
            foreach(Hotel hotel in hotels)
            {
                for (int i = 0; i < 4; i++)
                {
                    DataChart.Add(reservationService.ShowHotelReservationInChart(hotel.Name)[i]);
                }
                
            }
        }
        private void Detect(object sender, RoutedEventArgs e)
        {
            LiveCharts.ChartValues<int> chartValues = (LiveCharts.ChartValues<int>)reservationService.ShowHotelReservationPerMonth(HotelName, YearForStatistic)[0].Values;
            List<int> monthValues = chartValues.ToList();
            int i = 0;
            int max = monthValues[i];
            int month = i;
            for(i = 1; i < monthValues.Count; i++)
            {
                if (monthValues[i] > max)
                {
                    max = monthValues[i];
                    month = i;
                }
            }
            MessageBox.Show(HotelName + " was the busiest in year: " + YearForStatistic.ToString() + " month: " + ConvertIntToMonth(month) + " with number of reservations: " + max.ToString());
        }

        private void PdfButton_Click(object sender, RoutedEventArgs e)
        {
            StatisticPdf statisticPdfPage = new StatisticPdf();
            ShowSmallFrame.Content = statisticPdfPage;
            OwnerHome.pages.Push(statisticPdfPage);
        }

        public string ConvertIntToMonth(int i)
        {
            switch (i)
            {
                case 0: return "Jan";
                case 1: return "Feb";
                case 2: return "Mar";
                case 3: return "Apr";
                case 4: return "May";
                case 5: return "Jun";
                case 6: return "Jul";
                case 7: return "Aug";
                case 8: return "Sep";
                case 9: return "Oct";
                case 10: return "Nov";
                case 11: return "Dec";
                default: return "";
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string XTitle
        {
            get => _xTitle;
            set
            {
                if (_xTitle != value)
                {
                    _xTitle = value;
                    OnPropertyChanged();
                }

            }
        }

        public List<string> XLabels
        {
            get => _xLabels;
            set
            {
                if (_xLabels != value)
                {
                    _xLabels = value;
                    OnPropertyChanged();
                }

            }
        }

        public SeriesCollection DataChart
        {
            get => _data;
            set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged();
                }

            }
        }

        public string HotelName
        {
            get => _hotelName;
            set
            {
                if (_hotelName != value)
                {
                    _hotelName = value;
                    OnPropertyChanged();
                }

            }
        }

        public int YearForStatistic
        {
            get => _year;
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged();
                }

            }
        }
    }
}
