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
                       
                        Logo.Visibility = Visibility.Collapsed;
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

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
