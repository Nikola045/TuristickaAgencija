using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository;
using TravelAgency.Services;
using TravelAgency.Domain.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls.Primitives;
using TravelAgency.Domain.RepositoryInterfaces;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for GradePage.xaml
    /// </summary>
    public partial class GradePage : Page, INotifyPropertyChanged
    {
        private readonly GradeGuest1Repository gradeGuest1Repository;
        private readonly ReservationRepository reservationRepository;
        private readonly GradeService gradeService;
        private readonly ReservationService reservationService;
        private string _guestCB;
        private int _cb1Text;
        private int _cb2Text;
        private string _comment;
        private ButtonBase _button;
        public event PropertyChangedEventHandler? PropertyChanged;


        public GradePage()
        {
            InitializeComponent();
            Title = "Create new comment";
            DataContext = this;
            gradeGuest1Repository = new(InjectorService.CreateInstance<IStorage<GuestGrade>>());
            reservationRepository = new(InjectorService.CreateInstance<IStorage<Reservation>>());
            gradeService = new GradeService();
            reservationService = new ReservationService();
        }

        private void GusetLoaded(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = new List<Reservation>();

            reservations = reservationRepository.GetAll();
            gradeService.FindGuestsForGrade(GuestsCB);
        }

        private void Fill(object sender, RoutedEventArgs e)
        {
            CB1.Items.Add("1");
            CB1.Items.Add("2");
            CB1.Items.Add("3");
            CB1.Items.Add("4");
            CB1.Items.Add("5");
        }

        private void Fill1(object sender, RoutedEventArgs e)
        {
            CB2.Items.Add("1");
            CB2.Items.Add("2");
            CB2.Items.Add("3");
            CB2.Items.Add("4");
            CB2.Items.Add("5");
        }

        private void SaveGrade_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem comboBoxItem = GuestsCB.SelectedItem as ComboBoxItem;
            Reservation oldReservation = new Reservation();
            if (comboBoxItem != null && CB1Text != null && CB2Text != null && Comment != null)
            {
                GuestGrade newGrade = new GuestGrade(
                gradeService.FindGuestByUsername(comboBoxItem.Content.ToString()),
                CB1Text,
                CB2Text,
                Comment,    
                reservationService.FindReservationByID(Convert.ToInt32(comboBoxItem.Tag)));
                gradeGuest1Repository.Save(newGrade);

                CommentText.Clear();
                oldReservation = reservationService.FindReservationByID(Convert.ToInt32(comboBoxItem.Tag));
                GuestsCB.Items.Remove(comboBoxItem);
                reservationService.LogicalDelete(oldReservation);
            }
            else { };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Reservation> reservations = new List<Reservation>();

            reservations = reservationRepository.GetAll();
            if (reservations.Count > 0)
            {
                for (int i = 0; i < reservations.Count; i++)
                {
                    gradeService.ShowMessageForGrade(i);
                    gradeService.FindAndLogicalDeleteExpiredReservation(i);
                }
            }

        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ButtonBase Back
        {
            get => _button;
            set
            {
                if (_button != value)
                {
                    _button = value;
                    OnPropertyChanged();
                }
            }
        }

        public string GuestCB
        {
            get => _guestCB;
            set
            {
                if (_guestCB != value)
                {
                    _guestCB = value;
                    OnPropertyChanged();
                }

            }
        }

        public int CB1Text
        {
            get => _cb1Text;
            set
            {
                if (_cb1Text != value)
                {
                    _cb1Text = value;
                    OnPropertyChanged();
                }

            }
        }

        public int CB2Text
        {
            get => _cb2Text;
            set
            {
                if (_cb2Text != value)
                {
                    _cb2Text = value;
                    OnPropertyChanged();
                }

            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged();
                }

            }
        }
    }
}

