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
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for ActiveReservationsPage.xaml
    /// </summary>
    public partial class ActiveReservationsPage : Page
    {
        private User LoggedInUser { get; set; }
        public ActiveReservationsPage(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MoveReservationForm moveReservationForm = new MoveReservationForm(LoggedInUser);
            moveReservationForm.Show();
        }
    }
}
