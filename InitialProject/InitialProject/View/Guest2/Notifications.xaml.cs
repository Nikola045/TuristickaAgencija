using System.Collections.Generic;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for Notifications.xaml
    /// </summary>
    public partial class Notifications : Window
    {
        private readonly NotificationsRepository _repository;
        private readonly NotificationService notificationService;
        public User LoggedInUser { get; set; }
        public Notifications(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            _repository = new(InjectorService.CreateInstance<IStorage<Notification>>());
            notificationService = new NotificationService();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            List<Notification> notifications = new List<Notification>();
            notifications = _repository.GetAll();
            DataPanel.ItemsSource = notifications;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Guest2Overview guest2Overview = new Guest2Overview(LoggedInUser);
            this.Close();
            guest2Overview.Show();
        }
    }
}
