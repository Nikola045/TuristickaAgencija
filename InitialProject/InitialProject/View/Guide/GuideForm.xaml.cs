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

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for GuideForm.xaml
    /// </summary>
    public partial class GuideForm : Window
    {

        private readonly TourRepository _repository;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GuideForm()
        {
            InitializeComponent();
            Title = "Create new tour";
            DataContext = this;
            _repository = new TourRepository();
        }

        private void SaveTour(object sender, RoutedEventArgs e)
        {
            //GuideForm newTour = new GuideForm(/*mora forma da se napravi na xaml file*/);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
