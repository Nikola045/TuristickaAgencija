using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;
using TravelAgency.Model;
using TravelAgency.Repository;
using TravelAgency.View;

namespace TravelAgency.Forms
{
    /// <summary>
    /// Interaction logic for CommentsOverview.xaml
    /// </summary>
    public partial class OwnerOverview : Window
    {

        public static ObservableCollection<Hotel> Hotels { get; set; }

        public Owner SelectedHotel { get; set; }

        public User LoggedInUser { get; set; }

        private readonly HotelRepository _repository;

        public int DaysLeftForGrade = 5;

        public OwnerOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new HotelRepository();
        }

        public OwnerOverview()
        {
        }

        private void OpenOwnerForm(object sender, RoutedEventArgs e)
        {
            OwnerForm createOwnerForm = new OwnerForm();
            createOwnerForm.Show();
        }

        // Todo: dodaj funkciju koja ce da izbacuje massagebox nardenih 5 dana od isteka rezervacije nekom gostu
        private void OpenGradeForm(object sender, RoutedEventArgs e)
        {
            GradeForm createGradeForm = new GradeForm();
            createGradeForm.Show();
        }

        private void GradeAlert(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
