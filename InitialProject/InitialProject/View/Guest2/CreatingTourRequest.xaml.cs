using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;

using TravelAgency.Services;

using static Azure.Core.HttpHeader;


namespace TravelAgency.View.Guest2
{
    public partial class CreatingTourRequest : Window
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly TourRequestsRepository tourRequestsRepository;
        private readonly UserService ownerService;
        public User LogedUser { get; set; }
        
        private string _city;
        private string _country;
        private int _max;
        private string _description;
        private string _language;


        private string _cityV;
        private string _countryV;
        private string _maxV;
        private string _descriptionV;
        private string _languageV;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreatingTourRequest(User user)
        {
            InitializeComponent();
            LogedUser = user;
            tourRequestsRepository = new(InjectorService.CreateInstance<IStorage<TourRequests>>());
            ownerService = new UserService();
            this.Width = 1100;
            this.Height = 600;
        }



        public bool ButtonActivator()
        {
            if (
                CityV == "" &&
                CountryV == "" &&
                MaxV == "" &&
                DescriptionV == "" &&
                LanguageV == "" &&

                City != "" &&
                Country != "" &&
                Max.ToString() != "" &&
                Description != "" &&
                Language != "")

            {
                return true;
            }
            else
            { return false; }
           
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
        public string CityV
        {
            get => _cityV;
            set
            {
                if (_cityV != value)
                {
                    _cityV = value;
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
        public string CountryV
        {
            get => _countryV;
            set
            {
                if (_countryV != value)
                {
                    _countryV = value;
                    OnPropertyChanged();
                }

            }
        }

        public int Max
        {
            get => _max;
            set
            {
                if (_max != value)
                {
                    _max = value;
                    OnPropertyChanged();
                }

            }
        }
        public string MaxV
        {
            get => _maxV;
            set
            {
                if (_maxV != value)
                {
                    _maxV = value;
                    OnPropertyChanged();
                }

            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }

            }
        }
        public string DescriptionV
        {
            get => _descriptionV;
            set
            {
                if (_descriptionV != value)
                {
                    _descriptionV = value;
                    OnPropertyChanged();
                }

            }
        }
        public string Language
        {
            get => _language;
            set
            {
                if (_language != value)
                {
                    _language = value;
                    OnPropertyChanged();
                }

            }
        }
        public string LanguageV
        {
            get => _languageV;
            set
            {
                if (_languageV != value)
                {
                    _languageV = value;
                    OnPropertyChanged();
                }

            }
        }

        private void CityValidation(object sender, TextChangedEventArgs e)
        {
            string city = City;
            if (Regex.IsMatch(city, @"^[a-zA-Z\s]+$"))
            {
                CityV = "";
            }
            else
            {
                CityV = "Please enter valid value";
            }
        }

        private void CountryValidation(object sender, TextChangedEventArgs e)
        {
            string country = Country;
            if (Regex.IsMatch(country, @"^[a-zA-Z\s]+$"))
            {
                CountryV = "";
            }
            else
            {
                CountryV = "Please enter valid value";
            }
        }

        private void MaxGuestValidation(object sender, TextChangedEventArgs e)
        {
            int maxGuests = Max;
            if (Regex.IsMatch(maxGuests.ToString(), @"^[0-9]+$"))
            {
                MaxV = "";
            }
            else
            {
                MaxV = "Please enter valid value";
            }
        }

        private void SaveTourRequest(object sender, RoutedEventArgs e)
        {
            TourRequests newTourRequests = new TourRequests(
                tourRequestsRepository.NextId(),
                ownerService.GetOwnerByUsername(LogedUser.Username),
                txtCity.Text,
                txtCountry.Text,
                txtDescription.Text,
                txtLanguage.Text,
                Convert.ToInt32(txtMaxNumberOfGuests.Text),
                Convert.ToDateTime(FirstDateBox.Text),
                Convert.ToDateTime(SecondDateBox.Text));

                tourRequestsRepository.Save(newTourRequests);
                MessageBox.Show("Uspesno kreiran zahtev za turu.");
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Guest2Overview guest2Overview = new Guest2Overview(LogedUser);
            this.Close();
            guest2Overview.Show();

        }
        private void CreateComplex(object sender, RoutedEventArgs e)
        {
            CreatingComplexTourRequest guest2Overview = new CreatingComplexTourRequest(LogedUser);
            this.Close();
            guest2Overview.Show();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Guest2Overview guest2Overview = new Guest2Overview(LogedUser);
            this.Close();
            guest2Overview.Show();

        }

        private void OpenGuestOverview(object sender, RoutedEventArgs e)
        {
            Guest2Overview createGuest2Form = new Guest2Overview(LogedUser);
            Close();
            createGuest2Form.Show();
        }
        private void OpenGuest2Form(object sender, RoutedEventArgs e)
        {
            Guest2Form createGuest2Form = new Guest2Form(LogedUser);
            Close();
            createGuest2Form.Show();
        }
        private void OpenComplexTourRequests(object sender, RoutedEventArgs e)
        {
            ComplexTourRequests createGuest2Form = new ComplexTourRequests(LogedUser);
            Close();
            createGuest2Form.Show();
        }

        private void OpenGuestOnTour(object sender, RoutedEventArgs e)
        {
            GuestOnTour createGuestOnTour = new GuestOnTour(LogedUser);
            Close();
            createGuestOnTour.Show();
        }

        private void OpenVouchers(object sender, RoutedEventArgs e)
        {
            VouchersView createVouchers = new VouchersView(LogedUser);
            Close();
            createVouchers.Show();
        }

        private void OpenTourReviews(object sender, RoutedEventArgs e)
        {
            PastTours pastTours = new PastTours(LogedUser);
            Close();
            pastTours.Show();
        }
        private void OpenCreateTourRequest(object sender, RoutedEventArgs e)
        {
            CreatingTourRequest creatingTourRequest = new CreatingTourRequest(LogedUser);
            Close();
            creatingTourRequest.Show();
        }

        private void OpenTourRequestsStatistic(object sender, RoutedEventArgs e)
        {
            TourRequestsStatistic tourRequestsStatistic = new TourRequestsStatistic(LogedUser);
            Close();
            tourRequestsStatistic.Show();
        }

        private void OpenAllTourRequests(object sender, RoutedEventArgs e)
        {
            AllTourRequests tourRequestsStatistic = new AllTourRequests(LogedUser);
            Close();
            tourRequestsStatistic.Show();
        }

        private void OpenNotifications(object sender, RoutedEventArgs e)
        {
            Notifications notifications = new Notifications(LogedUser);
            notifications.Show();
        }

    }

}
