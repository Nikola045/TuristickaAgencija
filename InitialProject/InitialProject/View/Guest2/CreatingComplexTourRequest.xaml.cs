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
    /// <summary>
    /// Interaction logic for CreatingComplexTourRequest.xaml
    /// </summary>
    public partial class CreatingComplexTourRequest : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ComplexTourRequestRepositoty complexTourRequestsRepository;
        private readonly TourRequestsRepository tourRequestsRepository;
        private readonly UserService ownerService;
        public User LoggedInUser { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CreatingComplexTourRequest(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            complexTourRequestsRepository = new(InjectorService.CreateInstance<IStorage<ComplexTourRequest>>());
            tourRequestsRepository = new(InjectorService.CreateInstance<IStorage<TourRequests>>());
            ownerService = new UserService();
        }
        private void SaveTourRequest(object sender, RoutedEventArgs e)
        {
            ComplexTourRequest newTourRequests = new ComplexTourRequest();
            complexTourRequestsRepository.Save(newTourRequests);
            MessageBox.Show("Uspesno unet zahtev za kompleksnu turu.");
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            CreatingTourRequest guest2Overview = new CreatingTourRequest(LoggedInUser);
            this.Close();
            guest2Overview.Show();

        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Guest2Overview guest2Overview = new Guest2Overview(LoggedInUser);
            this.Close();
            guest2Overview.Show();

        }

        private void AddRequestToList(object sender, RoutedEventArgs e)
        {
            TourRequests request = new TourRequests();
            tourRequestsRepository.Save(request);
            ListCheckPoints.Items.Add(request);
        }

    }
}
