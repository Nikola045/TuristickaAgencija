using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Repository.UserRepo;
using TravelAgency.Services;

namespace TravelAgency
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public HotelRepository HotelRepository { get; }
        public GradeGuest1Repository GradeGuest1Repository { get; }
        public ImageRepository ImageRepository { get; }
        public MoveReservationRepository MoveReservationRepository { get; }
        public OwnerGradeRepository OwnerGradeRepository { get; }
        public ReservationRepository ReservationRepository { get; }
        public UserRepository UserRepository { get; }

        public App()
        {
            HotelRepository = new (InjectorService.CreateInstance<IStorage<Hotel>>());
            GradeGuest1Repository = new(InjectorService.CreateInstance<IStorage<GuestGrade>>());
            ImageRepository = new(InjectorService.CreateInstance<IStorage<Image>>());
            MoveReservationRepository = new(InjectorService.CreateInstance<IStorage<MoveReservation>>());
            OwnerGradeRepository = new(InjectorService.CreateInstance<IStorage<OwnerGrade>>());
            ReservationRepository = new(InjectorService.CreateInstance<IStorage<Reservation>>());
            UserRepository = new(InjectorService.CreateInstance<IStorage<User>>());
        }
    }
}
