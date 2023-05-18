
using LiveCharts;
using LiveCharts.Definitions.Charts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Guide
{
    public partial class TourStatistic : Window, INotifyPropertyChanged
    {
        private SeriesCollection _data;

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly TourRepository tourRepository;
        private readonly TourService tourService;
        private readonly GuestOnTourService guestOnTourService;
        public Tour selectedTour;

        public TourStatistic()
        {
            InitializeComponent();
            tourRepository = new(InjectorService.CreateInstance<IStorage<Tour>>());
            tourService = new TourService();
            guestOnTourService = new GuestOnTourService();

        }

        private void PerYearRB_Checked(object sender, RoutedEventArgs e)
        {
            CB.IsEnabled = true;
        }

        private void AllTimeRB_Checked(object sender, RoutedEventArgs e)
        {
            CB.IsEnabled = false;
            Tour tour = new Tour();
            List<Tour> tours = new List<Tour>();
            tour = tourService.FindMostAttendedTour();
            tours.Add(tour);
            DataPanel.ItemsSource = tours;

        }

        private void FillComboBox(object sender, RoutedEventArgs e)
        {
            CB.Items.Add("2023");
            CB.Items.Add("2022");
            CB.Items.Add("2021");
            CB.Items.Add("2020");
            CB.Items.Add("2019");
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            Tour tour = new Tour();
            string Year = CB.SelectedItem.ToString();
            List<Tour> tours = new List<Tour>();
            tour = tourService.FindMostAttendedTourThisYear(Year);
            tours.Add(tour);
            DataPanel.ItemsSource = tours;
            
        }

        private void ShowStatisticButton_Click(object sender, RoutedEventArgs e)
        {
            pieChart.Series.Clear();
            selectedTour = (Tour) DataPanel.SelectedItem;
            int[] Info = new int[4];
            Info = guestOnTourService.ShowStatistic(selectedTour.Id);
            txt1.Text = Info[0].ToString();
            txt2.Text = Info[1].ToString();
            txt3.Text = Info[2].ToString(); 
            txt4.Text = Info[3].ToString();
            string[] titles = { "a", "b", "c" };
            for (int i=0;i<Info.Length-1;i++)
            {
                
                pieChart.Series.Add(new PieSeries
                {
                    Title = titles[i],
                    Values = new ChartValues<double> { Info[i] },
                    DataLabels = true
                });
            }
            

        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }
}
