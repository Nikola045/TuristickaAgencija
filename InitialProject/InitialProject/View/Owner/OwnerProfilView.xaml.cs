using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Services;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerProfil.xaml
    /// </summary>
    public partial class OwnerProfil : Window, INotifyPropertyChanged
    {
        private User LogedOwner { get; set; }
        private string _newPassword;
        private string _confirmPassword;
        private string _superOwner;
        private string _username;
        private readonly OwnerService ownerService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public OwnerProfil(User user)
        {
            InitializeComponent();
            DataContext = this;
            LogedOwner = user;
            ownerService = new OwnerService();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            App.Current.Windows[0].Close();
            this.Close();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Username = LogedOwner.Username;
            NewPassword = "New Password";
            ConfirmPassword = "Confirm Password";   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SuperOwnerLabel_Loaded(object sender, RoutedEventArgs e)
        {
            SuperOwner = ownerService.SuperOwner(LogedOwner.Username);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string SuperOwner
        {
            get => _superOwner;
            set
            {
                if (_superOwner != value)
                {
                    _superOwner = value;
                    OnPropertyChanged();
                }

            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }

            }
        }

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                if (_newPassword != value)
                {
                    _newPassword = value;
                    OnPropertyChanged();
                }

            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged();
                }

            }
        }

        private void ChangeUsername(object sender, RoutedEventArgs e)
        {
            LogedOwner.Username = Username;
            ownerService.UpadateUsername(LogedOwner);
        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            if(NewPassword == ConfirmPassword)
            {
                LogedOwner.Password = NewPassword;
                ownerService.UpadateUsername(LogedOwner);
            }
            else
            {

            }
        }
    }
}
