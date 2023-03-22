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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GuideLiveTour()
        {
            InitializeComponent();
            Title = "Create new tour";
            DataContext = this;
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
