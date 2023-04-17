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
using TravelAgency.Domain.Model;
using TravelAgency.Repository;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for TourReview.xaml
    /// </summary>
    public partial class TourReview : Window
    {
        private readonly TourRepository _repository = new TourRepository();
        User LogedUser = new Domain.Model.User();
        public TourReview(User logedUser, Tour tour)
        {
            InitializeComponent();
            LogedUser = logedUser;
            DataContext = this;
            _repository = new TourRepository();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Fill1(object sender, RoutedEventArgs e)
        {
            CB1.Items.Add("1");
            CB1.Items.Add("2");
            CB1.Items.Add("3");
            CB1.Items.Add("4");
            CB1.Items.Add("5");
        }

        private void Fill2(object sender, RoutedEventArgs e)
        {
            CB2.Items.Add("1");
            CB2.Items.Add("2");
            CB2.Items.Add("3");
            CB2.Items.Add("4");
            CB2.Items.Add("5");
        }

        private void Fill3(object sender, RoutedEventArgs e)
        {
            CB3.Items.Add("1");
            CB3.Items.Add("2");
            CB3.Items.Add("3");
            CB3.Items.Add("4");
            CB3.Items.Add("5");
        }

        private void MakeReview(object sender, RoutedEventArgs e)
        {
            TourReview1 tourReview = new TourReview1();
            
        }
    }
}
