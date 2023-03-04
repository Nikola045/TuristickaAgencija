using InitialProject.Model;
using InitialProject.Repository;
using System.Collections.ObjectModel;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for CommentsOverview.xaml
    /// </summary>
    public partial class OwnerOverview : Window
    {

        public static ObservableCollection<Owner> Comments { get; set; }

        public Owner SelectedComment { get; set; }

        public User LoggedInUser { get; set; }

        private readonly OwnerRepository _repository;

        public OwnerOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new OwnerRepository();
            
        }

        

        
    }
}
