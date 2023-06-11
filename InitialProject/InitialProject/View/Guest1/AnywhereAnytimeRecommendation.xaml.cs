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
using Microsoft.Graph.Models.Security;
using User = TravelAgency.Domain.Model.User;
namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for AnywhereAnytimeRecommendation.xaml
    /// </summary>
    public partial class AnywhereAnytimeRecommendation : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public User LoggedInUser { get; set; }
        public AnywhereAnytimeRecommendation(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            DataContext = this;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Inlines.Add(new Run("Hello " + LoggedInUser.Username + ", here are some recommended accommodations based only on number of guests that are going, number of days that you are "));
            textBlock.Inlines.Add(new Run("\nplanning to stay and period of time(optional)."));
            textBlock.Inlines.Add(new Run("\n\nLets see what we have for you!"));
            Label.Content = textBlock;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReservationForm page = new ReservationForm(LoggedInUser);
            NavigationService.Navigate(page);
        }
    }
}
