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
using System.Windows.Shapes;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideLiveTour.xaml
    /// </summary>
    public partial class GuideLiveTour : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly TourRepository tourRepository;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GuideLiveTour()
        {
            InitializeComponent();
            Title = "Create new tour";
            DataContext = this;
            tourRepository = new TourRepository();
        }

        private void ShowTours(object sender, RoutedEventArgs e)
        {
            const string FilePath = "../../../Resources/Data/tours.csv";
            List<Tour> tours = new List<Tour>();
            tours = tourRepository.GetTodaysTours(FilePath);
            DataPanel.ItemsSource = tours;

        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            const string FilePath = "../../../Resources/Data/tours.csv";
            List<Tour> tours = new List<Tour>();
            tours = tourRepository.ReadFromToursCsv(FilePath);
            DataPanel.ItemsSource = tours;
        }

        private void DataPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /*private void LoadCheckPoints(object sender, RoutedEventArgs e)
        {
            List<CheckPoint> checkPoints = new List<CheckPoint>();
            string FilePath = "../../../Resources/Data/checkPoints.csv";
            checkPoints = checkPointRepository.ReadFromCheckPointsCsv(FilePath);

            for (int i = 0; i < checkPoints.Count; i++)
            {

                CheckPointsCB.Items.Add(checkPoints[i].Name);
            }

        }
        */
    }
}
