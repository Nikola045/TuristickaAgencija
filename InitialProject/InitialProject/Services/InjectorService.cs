using DevExpress.Utils.Filtering.Internal;
using System;
using System.Collections.Generic;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Repository.UserRepo;
using Image = TravelAgency.Domain.Model.Image;
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.Services
{
    class InjectorService
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(Domain.RepositoryInterfaces.IStorage < Tour >), new TourRepository() },
            { typeof(Domain.RepositoryInterfaces.IStorage < Hotel >), new HotelRepository() },
            { typeof(Domain.RepositoryInterfaces.IStorage < User >), new UserRepository() },
            { typeof(Domain.RepositoryInterfaces.IStorage < GuestGrade >), new GradeGuest1Repository() },
            { typeof(Domain.RepositoryInterfaces.IStorage < OwnerGrade >), new OwnerGradeRepository() },
            { typeof(Domain.RepositoryInterfaces.IStorage < CheckPoint >), new CheckPointRepository() },
            { typeof(Domain.RepositoryInterfaces.IStorage < GuestOnTour >), new GuestOnTourRepository() },
            { typeof(Domain.RepositoryInterfaces.IStorage < MoveReservation >), new MoveReservationRepository()},
            { typeof(Domain.RepositoryInterfaces.IStorage < Reservation >), new ReservationRepository()},
            { typeof(Domain.RepositoryInterfaces.IStorage < Image >), new ImageRepository()},
            { typeof(Domain.RepositoryInterfaces.IStorage < Voucher >), new VoucherRepository()},
        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);
            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }
            throw new ArgumentException($"No implementation found for type {type}");
        }
        
    }
}
