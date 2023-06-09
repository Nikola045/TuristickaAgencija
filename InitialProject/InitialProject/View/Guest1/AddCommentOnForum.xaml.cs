using System;
using System.Collections.Generic;
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
using Microsoft.Graph.Models.Security;
using TravelAgency.Domain.Model;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for AddCommentOnForum.xaml
    /// </summary>
    public partial class AddCommentOnForum : Page
    {
        public Forum SelectedForum { get; set; }
        private User LoggedInUser { get; set; }

        public AddCommentOnForum(Forum forum, User user)
        {
            InitializeComponent();
            SelectedForum = forum;
            LoggedInUser = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenedForum page = new OpenedForum(SelectedForum, LoggedInUser);
            NavigationService.Navigate(page);
        }
    }
}
