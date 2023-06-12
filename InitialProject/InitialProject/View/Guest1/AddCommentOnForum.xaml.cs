using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TravelAgency.Domain.Model;

namespace TravelAgency.View.Guest1
{
    public partial class AddCommentOnForum : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public Forum SelectedForum { get; set; }
        private User LoggedInUser { get; set; }

        private bool isCommentEmpty;
        public bool IsCommentEmpty
        {
            get { return isCommentEmpty; }
            set
            {
                isCommentEmpty = value;
                OnPropertyChanged();
                CommentBtn.IsEnabled = !isCommentEmpty;
            }
        }

        public AddCommentOnForum(Forum forum, User user)
        {
            InitializeComponent();
            DataContext = this;
            SelectedForum = forum;
            LoggedInUser = user;
            IsCommentEmpty = true;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsCommentEmpty = string.IsNullOrWhiteSpace(CommentTextBox.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenedForum page = new OpenedForum(SelectedForum, LoggedInUser);
            NavigationService.Navigate(page);
        }
    }
}
