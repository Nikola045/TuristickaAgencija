using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using TravelAgency.Services;
using Microsoft.Win32;
using TravelAgency.Domain.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Image = TravelAgency.Domain.Model.Image;
using System.Collections.Generic;

namespace TravelAgency.Forms
{
    public partial class OwnerForm : Window, INotifyPropertyChanged
    {
        private readonly HotelService hotelService;
        private User LogedUser;
        private string _hotelName;
        private string _city;
        private string _country;
        private string _hotelType;
        private int _max;
        private int _min;
        private int _cancelDays;
        private string _img;
        private string _url;
        private string _nameV;
        private string _cityV;
        private string _countryV;
        private string _typeV;
        private string _maxV;
        private string _minV;
        private string _cancelV;
        private string _imgV;
        private string _urlV;
        public event PropertyChangedEventHandler? PropertyChanged;

        public OwnerForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            hotelService = new HotelService();
            LogedUser = user;
        }


        private void Save(object sender, RoutedEventArgs e)
        {
            Hotel newHotel = new Hotel(HotelName,City,Country,HotelType,Max,Min,CancelDays);
            hotelService.SaveHotel(ButtonActivator(), LogedUser.Username, newHotel);
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            Image newImage = new Image(Img);
            ImageList.Items.Add(Img);
            hotelService.AddHotelImage(newImage);
            ImgV = "";
            Img = "";
        }

        private void DeleteImage(object sender, RoutedEventArgs e)
        {
            string selectedItem = ImageList.SelectedItem.ToString();
            ImageList.Items.Remove(selectedItem);
            hotelService.DeleteHotelImage(selectedItem);
            if (ImageList.Items.IsEmpty == true)
            {
                ImgV = "Please add at least one image";
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            List<string> urls = new List<string>();
            foreach (string url in ImageList.Items)
                urls.Add(url);
            hotelService.ClearListOfImage(urls);
            this.Close();
        }

        //Validation 
        public bool ButtonActivator()
        {
            if (
                NameV == "" &&
                CityV == "" &&
                CountryV == "" &&
                MaxV == "" &&
                MinV == "" &&
                CancelV == "" &&
                TypeV == "" &&
                ImgV == "" &&
                HotelName != "" &&
                City != "" &&
                Country != "" &&
                Max.ToString() != "" &&
                Min.ToString() != "" &&
                CancelDays.ToString() != "" &&
                HotelType != "")
            {
                return true;
            }
            else
            { return false; }
        }

        private void NameValidation(object sender, TextChangedEventArgs e)
        {
            string name = HotelName;
            if (Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                NameV = "";
            }
            else
            {
                NameV = "Please enter valid value";
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

        private void MinDaysValidation(object sender, TextChangedEventArgs e)
        {
            int minDays = Min;
            if (Regex.IsMatch(minDays.ToString(), @"^[0-9]+$"))
            {
                MinV = "";
            }
            else
            {
                MinV = "Please enter valid value";
            }
        }
        private void CancelDaysValidation(object sender, TextChangedEventArgs e)
        {
            int cancelDays = CancelDays;
            if (Regex.IsMatch(cancelDays.ToString(), @"^[0-9]+$"))
            {
                CancelV = "";
            }
            else
            {
                CancelV = "Please enter valid value";
            }
        }

        private void UrlValidation(object sender, TextChangedEventArgs e)
        {
            string url = Img;
            if (Regex.IsMatch(url, @"(?: ([^:/?#]+):)?(?://([^/?#]*))?([^?#]*\.(?:jpg|gif|png))(?:\?([^#]*))?(?:#(.*))?"))
            {
                UrlV = "";
                AddImgButton.IsEnabled = true;
            }
            else
            {
                UrlV = "Please input valid ulr address";
                AddImgButton.IsEnabled = false;
            }
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            ImgV = "Please add at least one image";
            TypeV = "Please select one option";
        }

        private void DataFill(object sender, RoutedEventArgs e)
        {
            Type.Items.Add("Hotel");
            Type.Items.Add("Hut");
            Type.Items.Add("House");
            Type.Items.Add("Apartment");
        }

        private void ValidationType(object sender, SelectionChangedEventArgs e)
        {
            TypeV = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            Img = file.FileName;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NameValidation(object sender, RoutedEventArgs e)
        {

        }
        public string HotelName
        {
            get => _hotelName;
            set
            {
                if (_hotelName != value)
                {
                    _hotelName = value;
                    OnPropertyChanged();
                }

            }
        }
        public string NameV
        {
            get => _nameV;
            set
            {
                if (_nameV != value)
                {
                    _nameV = value;
                    OnPropertyChanged();
                }

            }
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
        public string HotelType
        {
            get => _hotelType;
            set
            {
                if (_hotelType != value)
                {
                    _hotelType = value;
                    OnPropertyChanged();
                }

            }
        }
        public string TypeV
        {
            get => _typeV;
            set
            {
                if (_typeV != value)
                {
                    _typeV = value;
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

        public int Min
        {
            get => _min;
            set
            {
                if (_min != value)
                {
                    _min = value;
                    OnPropertyChanged();
                }

            }
        }
        public string MinV
        {
            get => _minV;
            set
            {
                if (_minV != value)
                {
                    _minV = value;
                    OnPropertyChanged();
                }

            }
        }

        public int CancelDays
        {
            get => _cancelDays;
            set
            {
                if (_cancelDays != value)
                {
                    _cancelDays = value;
                    OnPropertyChanged();
                }

            }
        }
        public string CancelV
        {
            get => _cancelV;
            set
            {
                if (_cancelV != value)
                {
                    _cancelV = value;
                    OnPropertyChanged();
                }

            }
        }
        public string ImgV
        {
            get => _imgV;
            set
            {
                if (_imgV != value)
                {
                    _imgV = value;
                    OnPropertyChanged();
                }

            }
        }
        public string Img
        {
            get => _img;
            set
            {
                if (_img != value)
                {
                    _img = value;
                    OnPropertyChanged();
                }

            }
        }
        public string Url
        {
            get => _url;
            set
            {
                if (_url != value)
                {
                    _url = value;
                    OnPropertyChanged();
                }

            }
        }

        public string UrlV
        {
            get => _urlV;
            set
            {
                if (_urlV != value)
                {
                    _urlV = value;
                    OnPropertyChanged();
                }

            }
        }
    }
}
