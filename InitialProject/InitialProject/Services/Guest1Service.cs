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
        private readonly OwnerService ownerService;
        public Guest1Service()
        {
            reservationRepository = new ReservationRepository(InjectorService.CreateInstance<IStorage<Reservation>>());
            userRepository = new UserRepository(InjectorService.CreateInstance<IStorage<User>>());
            ownerService = new OwnerService();
        }

        public int CountReservationsFromGuest(string guestUserName)
        {
            int currentYear = DateTime.Now.Year;
            List<Reservation> reservations = reservationRepository.GetAll();
            int reservationsCount = reservations.Count(reservation => reservation.GuestUserName == guestUserName && reservation.StartDate.Year == currentYear);
            return reservationsCount;
        }

        public string GetGuestStatus(string guestUserName)
        {
            int reservationCount = CountReservationsFromGuest(guestUserName);
            if (reservationCount >= 10)
            {
                string message = "Yes";
                return message;
            }

            else
            {
                string message = "No";
                return message;
            }
        }

        public int GetBonusPoints(string guestUserName)
        {
            DateTime dateTime = DateTime.Now;
            string guestStatus = GetGuestStatus(guestUserName);
            User guest1 = ownerService.GetOwnerByUsername(guestUserName);
            if (guestStatus == "No")
            {                                
                guest1.BonusPoints = 0;
                userRepository.Update(guest1);
            }
            int bonusPoints = guest1.BonusPoints;
            return bonusPoints;
        }
    }
}
