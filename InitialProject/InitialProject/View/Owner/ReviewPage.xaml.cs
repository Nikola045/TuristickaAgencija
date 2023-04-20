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
using TravelAgency.Services;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for ReviewPage.xaml
    /// </summary>
    public partial class ReviewPage : Page
    {
        private readonly GradeService gradeService;
        public ReviewPage(User user)
        {
            InitializeComponent();
            gradeService = new GradeService();
        }

        private void ShowData(object sender, RoutedEventArgs e)
        {
            ReviewData.ItemsSource = gradeService.ShowReviewsForOwner();
        }
    }
}
