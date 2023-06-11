using TravelAgency.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TravelAgency.View;
using TravelAgency.View.Guest2;
using TravelAgency.View.Owner;
using TravelAgency.Domain.Model;
using TravelAgency.Repository.UserRepo;
using TravelAgency.Services;
using TravelAgency.Domain.RepositoryInterfaces;
using Microsoft.Graph.Models.Security;
using System.Windows.Navigation;
using TravelAgency.View.Guest1;
using System.Windows.Input;
using System.Diagnostics;
using System;

namespace TravelAgency
{
    public partial class SignInForm : Window
    {
        private readonly UserRepository _repository;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new(InjectorService.CreateInstance<IStorage<User>>());
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if(user.Password == txtPassword.Password)
                {
                    if(user.LoginRole == "Owner")
                    {                        
                        OwnerHome ownerHome = new OwnerHome(user);
                        ownerHome.Show();
                        Close();
                    }
                    if (user.LoginRole == "Guide")
                    {
                        GuideOverview guideOverview = new GuideOverview(user);
                        guideOverview.Show();
                        Close();
                    }
                    if (user.LoginRole == "Guest1")
                    {
                        Guest1Overview page = new Guest1Overview(user);
                        ShowOverview.Content = page;
                        SignInButton.Visibility = Visibility.Collapsed;
                        UsernameLabel.Visibility = Visibility.Collapsed;
                        PasswordLabel.Visibility = Visibility.Collapsed;
                        txtUsername.Visibility = Visibility.Collapsed;
                        txtPassword.Visibility = Visibility.Collapsed;
                        Facebook.Visibility = Visibility.Collapsed;
                        Logo.Visibility = Visibility.Collapsed;
                        Booking.Visibility = Visibility.Collapsed;
                        Google.Visibility = Visibility.Collapsed;
                        ValidUser.Visibility = Visibility.Collapsed;
                    }
                    if (user.LoginRole == "Guest2")
                    {
                        Guest2Overview guest2Overview = new Guest2Overview(user);
                        guest2Overview.Show();
                        Close();
                    }
                } 
                else
                {
                    ValidUser.Visibility = Visibility.Visible;
                }
            }
            else
            {
                ValidUser.Visibility = Visibility.Visible;
            }
            
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            string language1 = "English";
            string language2 = "Serbian";
            cbLanguage.Items.Add(language1);
            cbLanguage.Items.Add(language2);
            cbLanguage.Text = language1;

            ValidUser.Visibility = Visibility.Collapsed;
        }
        private void Facebook_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Facebook_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        private void Facebook_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string url = "https://www.facebook.com/login/";

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Facebook: " + ex.Message);
            }
        }

        private void Google_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Google_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        private void Google_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string url = "https://accounts.google.com/";

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Google: " + ex.Message);
            }
        }
        private void Booking_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Booking_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        private void Booking_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string url = "https://account.booking.com/";

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Booking: " + ex.Message);
            }
        }
    }
}
