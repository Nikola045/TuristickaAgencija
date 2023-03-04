using TravelAgency.Model;
using TravelAgency.Repository;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Linq;

namespace TravelAgency.Forms
{
    /// <summary>
    /// Interaction logic for CommentForm.xaml
    /// </summary>
    public partial class OwnerForm : Window
    {

        public User LoggedInUser { get; set; }

        public Hotel SelectedHotel { get; set; }

        private readonly HotelRepository _repository;

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _max;
        public int Max
        {
            get => _max;
            set
            {
                if (value != _max)
                {
                    _max = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _min;
        public int Min  
        {
            get => _min;
            set
            {
                if (value != _min)
                {
                    _min = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _cancelD;
        public int CancelD
        {
            get => _cancelD;
            set
            {
                if (value != _cancelD)
                {
                    _cancelD = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        

        public OwnerForm(User user)
        {
            InitializeComponent();
            Title = "Create new hotel";
            DataContext = this;
            LoggedInUser = user;
           // _repository = new HotelRepository();
        }


        private void Save(object sender, RoutedEventArgs e)
        {
            
             if (SelectedHotel != null)
             {
                 SelectedHotel.Name = Name;
                 SelectedHotel.City = City;
                 SelectedHotel.Country = Country;
                 SelectedHotel.TypeOfHotel = Type;
                 SelectedHotel.MaxNumberOfGusets = Max;
                 SelectedHotel.MinNumberOfGusets = Min;
                 SelectedHotel.NumberOfDaysToCancel = CancelD;

                 Hotel updatedHotel = _repository.Update(SelectedHotel);
                 if (updatedHotel != null)
                 {
                     // Update observable collection
                     int index = OwnerOverview.Hotels.IndexOf(SelectedHotel);
                     OwnerOverview.Hotels.Remove(SelectedHotel);
                     OwnerOverview.Hotels.Insert(index, updatedHotel);
                 }
             }
             else
             {
                 Hotel newHotel = new Hotel(Name,City,Country,Type,Max,Min,CancelD);
                 Hotel savedHotel = _repository.Save(newHotel); //NEKA GRESKA
                 OwnerOverview.Hotels.Add(savedHotel);
             }

             Close();
        
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
