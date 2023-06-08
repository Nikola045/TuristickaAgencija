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
using TravelAgency.Domain.Model;
using TravelAgency.Services;
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for StartNewDiscussion.xaml
    /// </summary>
    public partial class StartNewDiscussion : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly ForumService forumService;
        private User LoggedInUser { get; set; }
        private string _city;
        private string _country;
        private string _comment;
        public StartNewDiscussion(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            DataContext = this;
            forumService = new ForumService();
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string City
        {
            get => _city;
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged();
                }

            }
        }
        public string Country
        {
            get => _country;
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged();
                }

            }
        }
        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged();
                }

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Forum forum = new Forum(City,Country);
            forumService.CreateForum(LoggedInUser,forum);
            ForumComment forumComment = new ForumComment(forum,"No","No",Comment);
            forumService.CreateCommentOfGuest1(LoggedInUser,forumComment);
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Inlines.Add(new Run("If you need any kind of advice or question about any location, you can start your own discussion on our forum here and get advice from other users or owners of accommodations\non that location."));
            Label.Content = textBlock;
        }
    }
}
