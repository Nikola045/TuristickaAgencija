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
using System.Windows.Navigation;
using System.Windows.Shapes;
using User = TravelAgency.Domain.Model.User;
namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for SucessfullyCanceled.xaml
    /// </summary>
    public partial class SucessfullyCanceled : Page, INotifyPropertyChanged
    {
        User LogedUser { get; set; }
        public SucessfullyCanceled(User user)
        {
            InitializeComponent();
            LogedUser = user;
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MoveReservationForm page = new MoveReservationForm(LogedUser);
            NavigationService.Navigate(page);
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
