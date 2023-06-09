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
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for CrateComment.xaml
    /// </summary>
    public partial class CrateComment : Page, INotifyPropertyChanged
    {
        private readonly ForumService forumService;
        private readonly ForumCommentRepository forumCommentRepository;
        public string _comment;
        public event PropertyChangedEventHandler? PropertyChanged;
        public User User { get; private set; }
        public Forum Forum { get; private set; }
        public CrateComment(User user, Forum forum)
        {
            InitializeComponent();
            DataContext = this;
            forumService = new ForumService();
            forumCommentRepository = new(InjectorService.CreateInstance<IStorage<ForumComment>>());
            User = user;
            Forum = forum;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ForumComment comment = new ForumComment(Forum,"No","No",Comment);
            forumService.CreateCommentOfOwner(User, comment);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }
}
