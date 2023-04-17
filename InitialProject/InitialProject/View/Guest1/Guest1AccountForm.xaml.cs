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

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for Guest1AccountForm.xaml
    /// </summary>
    public partial class Guest1AccountForm : Window
    {
        public Guest1AccountForm()
        {
            InitializeComponent();
        }

        private void OpenAccount(object sender, RoutedEventArgs e)
        {
            Guest1AccountForm createAccountForm = new Guest1AccountForm();
            createAccountForm.Show();
        }

        private void ShowReview(object sender, RoutedEventArgs e)
        {
            Guest1ShowReview createShowReview = new Guest1ShowReview();
            createShowReview.Show();
        }

        private void GradeOwner(object sender, RoutedEventArgs e)
        {
            GradeOwnerForm gradeOwner = new GradeOwnerForm();
            gradeOwner.Show();
        }
    }
}
