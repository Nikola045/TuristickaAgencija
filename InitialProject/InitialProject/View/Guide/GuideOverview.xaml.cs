using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.UserRepo;
using TravelAgency.Services;
using TravelAgency.View.Guide;

namespace TravelAgency.View
{
    public partial class GuideOverview : Window
    {
        private readonly UserRepository userRepository;
        private readonly TourRepository tourRepository;
        private readonly TourService tourService;
        private readonly GuestOnTourRepository guestOnTourRepository;
        private readonly VoucherRepository voucherRepository;
        public GuideOverview()
        {
        }
        public User LoggedInUser { get; set; }

        public GuideOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;

            tourRepository = new(InjectorService.CreateInstance<IStorage<Tour>>());
            voucherRepository = new(InjectorService.CreateInstance<IStorage<Voucher>>());
            guestOnTourRepository = new(InjectorService.CreateInstance<IStorage<GuestOnTour>>());
            userRepository = new(InjectorService.CreateInstance<IStorage<User>>());
            tourService = new TourService();
        }

        private void OpenGuideForm(object sender, RoutedEventArgs e)
        {
            GuideForm createGuideForm = new GuideForm(LoggedInUser);
            createGuideForm.Show();
        }

        private void OpenLiveTourForm(object sender, RoutedEventArgs e)
        {
            GuideLiveTour createLiveTourForm = new GuideLiveTour(LoggedInUser);
            createLiveTourForm.Show();
        }

        private void OpenTourStatisticForm(object sender, RoutedEventArgs e)
        {
            TourStatistic createTourStatistic = new TourStatistic();
            createTourStatistic.Show();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            DataPanel.ItemsSource = tourRepository.GetAll();
            List<double> ratings = new List<double>();
            foreach(string language in tourService.GetAllLanguages(LoggedInUser))
            {
                ratings.Add(tourService.GetAllRatings(language, LoggedInUser));
            }
            if(ratings.Max() > 4.5)
            {
                SuperGuideLabel.Content = "Super-Guide";
            }
            else
            {
                SuperGuideLabel.Content = "Guide";
            }
            
        }

        private void DeleteTour(object sender, RoutedEventArgs e)
        {
            Tour SelectedTour = DataPanel.SelectedItem as Tour;
            tourRepository.Delete(SelectedTour);
            DataPanel.ItemsSource = tourRepository.GetAll();
        }
        private void GetFired(object sender, RoutedEventArgs e)
        {
            List<GuestOnTour> guests = new List<GuestOnTour>();
            guests = guestOnTourRepository.GetAll();
            List<Tour> tours = new List<Tour>();
            tours = tourRepository.GetAll();
            foreach (Tour tour in tours)
            {
                if (LoggedInUser.Id == tour.GuideId)
                { 
                    foreach(GuestOnTour guest in guests)
                    {
                        if (guest.Tour.Id == tour.Id)
                        {
                            Voucher voucher = new Voucher(voucherRepository.NextId(),"Otkaz",DateTime.Now.AddYears(2),guest.Guest2);
                            voucherRepository.Save(voucher);
                        }
                    }

                }
            }
            userRepository.Delete(LoggedInUser);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

        private void OpenReviews(object sender, RoutedEventArgs e)
        {
            Tour SelectedTour = DataPanel.SelectedItem as Tour;
            GuideReviews createGuideReviewsForm = new GuideReviews(SelectedTour);
            createGuideReviewsForm.Show();
        }

        private void OpenTourRequestsForm(object sender, RoutedEventArgs e)
        {
            GuideTourRequests createGuideTourRequestsForm = new GuideTourRequests(LoggedInUser);
            createGuideTourRequestsForm.Show();
        }

        private void OpenTourRequestsStatisticsForm(object sender, RoutedEventArgs e)
        {
            GuideTourRequestStatistic createTourRequestStatisticsForm = new GuideTourRequestStatistic(LoggedInUser);
            createTourRequestStatisticsForm.Show();
        }
    }
}


