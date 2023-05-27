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
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for ForumView.xaml
    /// </summary>
    public partial class ForumView : Page, INotifyPropertyChanged
    {
        private Frame SmallPageShow;
        public static ObservableCollection<Forum> Forums { get; set; }
        private readonly ForumRepository forumRepository;
        private readonly ForumService forumService;
        public Forum SelectedForum { get; set; }
        private User LoggedOwner {get; set;}
        public event PropertyChangedEventHandler? PropertyChanged;

        public ForumView(User user, Frame frame)
        {
            InitializeComponent();
            DataContext = this;
            SmallPageShow = frame;
            forumRepository = new(InjectorService.CreateInstance<IStorage<Forum>>());
            Forums = new ObservableCollection<Forum>(forumRepository.GetAll());
            forumService = new ForumService();
            LoggedOwner = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedForum != null)
            {
                ForumReviewView forumReviewView = new ForumReviewView(SelectedForum);
                SmallPageShow.Content = forumReviewView;
                OwnerHome.pages.Push(forumReviewView);
            }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Forum forum = forumService.ShowMessageForForums(LoggedOwner.Username);
            if(forum != null)
            {
                ForumReviewView forumReviewView = new ForumReviewView(forum);
                SmallPageShow.Content = forumReviewView;
                OwnerHome.pages.Push(forumReviewView);
            }
        }
    }
}
