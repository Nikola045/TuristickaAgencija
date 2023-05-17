using System;
using System.Collections.Generic;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Repository.UserRepo;
using TravelAgency.Storage.FileStorage;
using Image = TravelAgency.Domain.Model.Image;
using User = TravelAgency.Domain.Model.User;

namespace TravelAgency.Services
{
    class InjectorService
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(IStorage < Tour >), new TourFileStorage() },
            { typeof(IStorage < Hotel >), new HotelFileStorage() },
            { typeof(IStorage < User >), new UserFileStorage() },
            { typeof(IStorage < GuestGrade >), new GradeGuest1FileStorage() },
            { typeof(IStorage < OwnerGrade >), new OwnerGradeFileStorage() },
            { typeof(IStorage < CheckPoint >), new CheckPointFileStorage() },
            { typeof(IStorage < GuestOnTour >), new GuestOnTourFileStorage() },
            { typeof(IStorage < MoveReservation >), new MoveReservationFileStorage()},
            { typeof(IStorage < Reservation >), new ReservationFileStorage()},
            { typeof(IStorage < Image >), new ImageFileStorage()},
            { typeof(IStorage < TourRequests >), new VoucherFileStorage()},
            { typeof(IStorage < RenovationRequest >), new RenovationRequestFileStorage()},
            { typeof(IStorage < GuestOnTour >), new GuestOnTourFileStorage()},
            { typeof(IStorage < TourRequests >), new TourRequestsFileStorage()}
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
