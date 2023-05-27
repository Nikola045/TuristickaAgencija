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

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for ForumReviewView.xaml
    /// </summary>
    public partial class ForumReviewView : Page
    {
        private Forum CurrentForum { get; set; }
        public ForumReviewView(Forum forum)
        {
            InitializeComponent();
            DataContext = this;
            CurrentForum = forum;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            forumLabel.Content = CurrentForum.Country + " " + CurrentForum.City;
        }
    }
}
