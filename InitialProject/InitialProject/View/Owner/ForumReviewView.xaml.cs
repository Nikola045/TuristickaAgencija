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
    /// Interaction logic for ForumReviewView.xaml
    /// </summary>
    public partial class ForumReviewView : Page, INotifyPropertyChanged
    {
        private Forum CurrentForum { get; set; }
        private bool _buttonCreate;
        private string _useful;
        private Frame ShowSmallPage;
        public ForumComment SelectedComment { get; set; }
        public static ObservableCollection<ForumComment> Comments { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public readonly ForumCommentRepository forumCommentRepository;
        public ForumService forumService { get; set; }
        public User Owner { get; set; }
        public ForumReviewView(Forum forum, Frame frame, User user)
        {
            InitializeComponent();
            DataContext = this;
            CurrentForum = forum;
            ShowSmallPage = frame;
            forumCommentRepository = new(InjectorService.CreateInstance<IStorage<ForumComment>>());
            forumService = new ForumService();
            Comments = new ObservableCollection<ForumComment>(forumService.GetForumComments(forum));
            Owner = user;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            forumLabel.Content = CurrentForum.Country + " " + CurrentForum.City;
            ButtonCreate = forumService.IsOwnerLocation(Owner.Username, CurrentForum);
            Useful = forumService.IsForumVearyUseful(CurrentForum);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CrateComment crateComment = new CrateComment(Owner,CurrentForum);
            ShowSmallPage.Content = crateComment;
            OwnerHome.pages.Push(crateComment);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool ButtonCreate
        {
            get => _buttonCreate;
            set
            {
                if (_buttonCreate != value)
                {
                    _buttonCreate = value;
                    OnPropertyChanged();
                }

            }
        }
        public string Useful
        {
            get => _useful;
            set
            {
                if (_useful != value)
                {
                    _useful = value;
                    OnPropertyChanged();
                }

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            forumService.ReportComment(SelectedComment);
        }
    }
}
