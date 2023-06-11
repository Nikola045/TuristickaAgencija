using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Graph.Models;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Services;
using User = TravelAgency.Domain.Model.User;
namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for MoveReservationRequestList.xaml
    /// </summary>
    public partial class MoveReservationRequestList : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private User LoggedInUser { get; set; }
        MoveReservationRepository moveReservationRepository;
        public ObservableCollection<MoveReservation> MoveReservations { get; set; }
        public MoveReservationRequestList(User loggedInUser)
        {
            InitializeComponent();
            LoggedInUser = loggedInUser;
            moveReservationRepository = new(InjectorService.CreateInstance<IStorage<MoveReservation>>());
            DataContext = this;
            MoveReservations = new ObservableCollection<MoveReservation>(moveReservationRepository.GetAll());
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
