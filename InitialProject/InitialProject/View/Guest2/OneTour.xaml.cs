using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Guest2
{
    public partial class OneTour : Window
    {
        User LogedUser = new User();

        public event PropertyChangedEventHandler PropertyChanged;

        public Tour selectedTour;

        private readonly TourRepository _repository;
        private readonly VoucherRepository _vouchersRepository;
        private readonly TourService tourService;

        public OneTour(User logedUser, Tour tour)
        {
            InitializeComponent();
            LogedUser = logedUser;
            selectedTour = tour;
            _repository = new(InjectorService.CreateInstance<IStorage<Tour>>());
            _vouchersRepository = new(InjectorService.CreateInstance<IStorage<Voucher>>());
            tourService = new TourService();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddPeopleOnSelectedTour(object sender, RoutedEventArgs e)
        {
            int numGuests = Convert.ToInt32(txtNumOfGuests.Text);
            bool hasVoucher = false;
            if(Vouchers.SelectedItem != null)
            {
                hasVoucher = true;
            }

            if (numGuests <= 0)
            {
                MessageBox.Show("Please select how many guests want to go on the tour.");
            }
            else
            {
                if (selectedTour.MaxNumberOfGuests < selectedTour.CurentNumberOfGuests + numGuests)
                {
                    MessageBox.Show("Selected tour doesn't have that many free places." +
                        "Here are some similar tours for that many people.");
                }
                else
                {

                    if (tourService.ReserveTour(selectedTour.Id, LogedUser, numGuests, hasVoucher))
                    {
                        selectedTour.CurentNumberOfGuests = selectedTour.CurentNumberOfGuests + numGuests;
                        _repository.Update(selectedTour);
                        if (Vouchers.SelectedItem != null)
                        {
                            MessageBox.Show("Reserved with voucher.");

                        }
                        else
                        {
                            MessageBox.Show("Reserved.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not reserved.");
                    }

                }
            }
        }

        public void LoadData(object sender, RoutedEventArgs e)
        {
            txtName.Text = selectedTour.Name.ToString();
            txtCity.Text = selectedTour.City.ToString();
            txtCountry.Text = selectedTour.Country.ToString();
            txtDescription.Text = selectedTour.Description.ToString();
            txtLenguage.Text = selectedTour.Lenguage.ToString();
            txtMaxNumberOfGuests.Text = selectedTour.MaxNumberOfGuests.ToString();
            txtCurentNumberOfGuests.Text = selectedTour.CurentNumberOfGuests.ToString();
            txtDuration.Text = selectedTour.TourDuration.ToString();
            txtDate.Text = selectedTour.StartTime.ToString();
            List < CheckPoint >  checkPoints = selectedTour.CheckPoints;
            CheckPoints.ItemsSource = checkPoints;
            List<string> dates = new List<string>();

        }

        private void Vouchers_Loaded(object sender, RoutedEventArgs e)
        {
            List<Voucher> vouchers = _vouchersRepository.GetAll();
            for (int i = 0; i < vouchers.Count; i++) {
                Vouchers.Items.Add(vouchers[i]);
            }
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

        private void txtDuration_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
