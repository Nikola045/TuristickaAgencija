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
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for ForumSettingsPage.xaml
    /// </summary>
    public partial class ForumSettingsPage : Page
    {
        private User LoggedInUser { get; set; }
        public ForumSettingsPage(User user)
        {
            LoggedInUser = user;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Inlines.Add(new Run("*Note\n The discussion will not be deleted (it will be visible),only\n further replying will be disabled."));
            Label.Content = textBlock;
        }
    }
}
