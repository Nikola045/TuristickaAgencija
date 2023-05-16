using Microsoft.Graph.Models.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<OwnerGrade> Grades { get; set; }
        public ReviewPage(User user)
        {
            InitializeComponent();
            DataContext = this;
            gradeService = new GradeService();
            Grades = new ObservableCollection<OwnerGrade>(gradeService.ShowReviewsForOwner());
        }

        private void ShowData(object sender, RoutedEventArgs e)
        {
            
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
