using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for ReservationForm.xaml
    /// </summary>
    public partial class ReservationForm : Window
    {
        Reservation NewReservation = new Reservation();

        private readonly ReservationRepository _repository;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public ReservationForm()
        {
            InitializeComponent();
            Title = "Create new reservation";
            DataContext = this;
            _repository = new ReservationRepository();
        }
        
        private void SaveGrade_Click(object sender, RoutedEventArgs e)
        {

            Reservation newReservation = new Reservation(
                HotelNameCB.Text,
                Convert.ToDateTime(Date1.Text),
                Convert.ToDateTime(Date2.Text),
                Convert.ToInt32(txtNumberOfDays.Text),
                Convert.ToInt32(txtNumberOfGuests.Text));
            _repository.Save(newReservation);

            txtNumberOfDays.Clear();
            txtNumberOfGuests.Clear();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Reserve(object sender, RoutedEventArgs e)
        {

        }
    }
}
