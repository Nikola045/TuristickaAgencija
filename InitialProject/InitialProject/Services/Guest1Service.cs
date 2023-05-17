using System;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository.UserRepo;

namespace TravelAgency.Services
{
    internal class Guest1Service
    {
        private readonly ReservationRepository reservationRepository;
        private readonly UserRepository userRepository;

        public Guest1Service()
        {
            reservationRepository = new ReservationRepository(InjectorService.CreateInstance<IStorage<Reservation>>());
            userRepository = new UserRepository(InjectorService.CreateInstance<IStorage<User>>());
        }

        public int CountReservationsFromGuest(string guestUserName)
        {
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);
            List<Reservation> reservations = reservationRepository.GetAll();
            return reservations.Count(reservation => reservation.GuestUserName == guestUserName && reservation.StartDate >= oneYearAgo);
        }

        public string GetGuestStatus(string guestUserName)
        {
            int reservationCount = CountReservationsFromGuest(guestUserName);
            if (reservationCount >= 10)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }

        public int GetBonusPoints(string guestUserName)
        {
            string guestStatus = GetGuestStatus(guestUserName);
            if (guestStatus == "Yes")
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }
    }
}
