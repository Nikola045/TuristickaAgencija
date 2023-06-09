using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using TravelAgency.Repository;
using TravelAgency.Services;
using User = TravelAgency.Domain.Model.User;
using TravelAgency.Domain.RepositoryInterfaces;
using System.Runtime.CompilerServices;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for ForumPage.xaml
    /// </summary>
    public partial class ForumPage : Page, INotifyPropertyChanged
    {
        public static ObservableCollection<Forum> Forums { get; set; }
        private readonly ForumRepository forumRepository;
        private readonly ForumService forumService;
        public Forum SelectedForum { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private User LoggedInUser { get; set; }
        public ForumPage(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;            
            forumRepository = new(InjectorService.CreateInstance<IStorage<Forum>>());
            Forums = new ObservableCollection<Forum>(forumRepository.GetAll());
            forumService = new ForumService();
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartNewDiscussion page = new StartNewDiscussion(LoggedInUser);
            NavigationService.Navigate(page);
        }
        private void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SelectedForum != null)
            {
                OpenedForum page = new OpenedForum(SelectedForum, LoggedInUser);
                NavigationService.Navigate(page);
            }
        }
    }
}
