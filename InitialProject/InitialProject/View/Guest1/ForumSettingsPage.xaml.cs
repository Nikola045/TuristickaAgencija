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
using Microsoft.Graph.Models.Security;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for ForumSettingsPage.xaml
    /// </summary>
    public partial class ForumSettingsPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly ForumService forumService;
        public static ObservableCollection<Forum> Forums { get; set; }
        private readonly ForumRepository forumRepository;
        public Forum SelectedForum { get; set; }
        private User LoggedInUser { get; set; }
        public ForumSettingsPage(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            forumRepository = new(InjectorService.CreateInstance<IStorage<Forum>>());
            Forums = new ObservableCollection<Forum>(forumRepository.GetAll());
            forumService = new ForumService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            forumService.CloseForum(SelectedForum);
            RefreshData();
        }
        private void RefreshData()
        {
            Forums.Clear();
            foreach (var forum in forumRepository.GetAll())
            {
                Forums.Add(forum);
            }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Inlines.Add(new Run("*Note\n The discussion will not be deleted (it will be visible),only\n further replying will be disabled."));
            Label.Content = textBlock;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                CloseDiscussion.IsEnabled = true;
            }
            else
            {
                CloseDiscussion.IsEnabled = false;
            }

        }
    }
}
