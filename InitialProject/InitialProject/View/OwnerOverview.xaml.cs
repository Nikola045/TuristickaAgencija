using System.Collections.ObjectModel;
using System.Windows;
using System.Xml.Linq;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.Forms
{
    /// <summary>
    /// Interaction logic for CommentsOverview.xaml
    /// </summary>
    public partial class OwnerOverview : Window
    {

        public static ObservableCollection<Hotel> Hotels { get; set; }

        public Owner SelectedHotel { get; set; }

        public User LoggedInUser { get; set; }

        private readonly HotelRepository _repository;

        public OwnerOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new HotelRepository();
        }

        private void OpenOwnerForm(object sender, RoutedEventArgs e)
        {
            OwnerForm createOwnerForm = new OwnerForm(LoggedInUser);
            createOwnerForm.Show();
        }


    }
}
