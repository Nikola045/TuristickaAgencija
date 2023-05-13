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
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for Guest1HomePage.xaml
    /// </summary>
    public partial class Guest1HomePage : Page
    {
        public User LoggedInUser { get; set; }
        public Guest1HomePage(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            string hyperlinkText = "Anywhere/anytime";
            string navigateUri = "AnywhereAnytimePage.xaml";

            Hyperlink hyperlink = new Hyperlink(new Run(hyperlinkText));
            hyperlink.NavigateUri = new Uri(navigateUri, UriKind.Relative);

            hyperlink.RequestNavigate += Hyperlink_RequestNavigate;

            TextBlock textBlock = new TextBlock();
            textBlock.Inlines.Add(new Run("Hello " + LoggedInUser.Username + ", here are some recommended accommodations for you, or try "));
            textBlock.Inlines.Add(hyperlink);

            welcomeLabel.Content = textBlock;
            Hyperlink alpinaLink = new Hyperlink();
            alpinaLink.Inlines.Add("Alpina");
            alpinaLink.Click += AlpinaLink_Click;
            alpinaLabel.Content = alpinaLink;

            Hyperlink jabukaLink = new Hyperlink();
            jabukaLink.Inlines.Add("Jabuka");
            jabukaLink.Click += JabukaLink_Click;
            jabukaLabel.Content = jabukaLink;

            Hyperlink malinaLink = new Hyperlink();
            malinaLink.Inlines.Add("Malina");
            malinaLink.Click += MalinaLink_Click;
            malinaLabel.Content = malinaLink;
            }

            private void AlpinaLink_Click(object sender, RoutedEventArgs e)
            {
            ReservationForm page = new ReservationForm(LoggedInUser);
            NavigationService.Navigate(page);
            }

            private void JabukaLink_Click(object sender, RoutedEventArgs e)
            {
            // Otvaranje stranice ReservationForm za Jabuka
            ReservationForm page = new ReservationForm(LoggedInUser);
            NavigationService.Navigate(page);
            }

            private void MalinaLink_Click(object sender, RoutedEventArgs e)
            {
             ReservationForm page = new ReservationForm(LoggedInUser);
             NavigationService.Navigate(page);
            }


            private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            AnywhereAnytime page = new AnywhereAnytime();
            NavigationService.Navigate(page);

            e.Handled = true;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReservationForm page = new ReservationForm(LoggedInUser);
            NavigationService.Navigate(page);
        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }
    }
}
