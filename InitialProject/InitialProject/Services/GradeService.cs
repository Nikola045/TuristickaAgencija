using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository.UserRepo;

namespace TravelAgency.Services
{
    internal class GradeService
    {
        private readonly OwnerGradeRepository ownerGradeRepository;
        private readonly ReservationRepository reservationRepository;
        private readonly UserRepository userRepository;
        private readonly ReservationService reservationService;
        private readonly HotelService hotelService;
        public GradeService() 
        {
            ownerGradeRepository = new(InjectorService.CreateInstance<IStorage<OwnerGrade>>());
            reservationRepository = new(InjectorService.CreateInstance<IStorage<Reservation>>());
            userRepository = new(InjectorService.CreateInstance<IStorage<User>>());
            hotelService = new HotelService();
            reservationService = new ReservationService();
        }

        internal OwnerGrade FindOwnerGradeByReservationId(int id)
        {
            List<OwnerGrade> grades = ownerGradeRepository.GetAll();
            foreach(OwnerGrade grade in grades)
            {
                if (grade.Reservation.Id == id)
                {
                    return grade;
                }
            }
            return null;
        }

        public bool IsOwnerGradeExists(int id)
        {
            List<OwnerGrade> grades = ownerGradeRepository.GetAll();
            foreach (OwnerGrade grade in grades)
            {
                if (grade.Reservation.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public List<OwnerGrade> ShowReviewsForOwner()
        {
            List<Reservation> allReservation = reservationRepository.GetAll();
            List<OwnerGrade> ownerGrades = new List<OwnerGrade>();
            foreach (Reservation reservation in allReservation)
            {
                if (reservation.GradeStatus == "Graded")
                {
                    if (IsOwnerGradeExists(reservation.Id))
                    {
                        OwnerGrade ownerGrade = FindOwnerGradeByReservationId(reservation.Id);
                        ownerGrade.Reservation.Hotel.Name = hotelService.GetHotelByIdOfReservation(reservation.Id);
                        ownerGrades.Add(FindOwnerGradeByReservationId(reservation.Id));
                    }
                }
            }
            return ownerGrades;
        }

        public void FindAndLogicalDeleteExpiredReservation(int i)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetAll();
            DateTime dateTimeNow = DateTime.Now;

            if (reservations[i].EndDate < dateTimeNow && reservations[i].EndDate.AddDays(5) < dateTimeNow)
            {
                reservationService.LogicalDeleteExpire(reservations[i]);
            }
        }

        public void ShowMessageForGrade(int i)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.GetAll();
            DateTime dateTimeNow = DateTime.Now;
            string message = null;

            if (reservations[i].EndDate < dateTimeNow && reservations[i].EndDate.AddDays(5) > dateTimeNow && reservations[i].GradeStatus == "NotGraded")
            {
                if (dateTimeNow < reservations[i].EndDate)
                {
                    message = "You have " + (5 - DateTime.DaysInMonth(dateTimeNow.Year, dateTimeNow.Month) - (dateTimeNow.Day - reservations[i].EndDate.Day)).ToString() + " days left to grade " + reservations[i].GuestUserName;
                }
                else
                {
                    message = "You have " + (5 - (dateTimeNow.Day - reservations[i].EndDate.Day)).ToString() + " days left to grade " + reservations[i].GuestUserName;
                }
            }
            if(message != null)
            {
                
                MessageBox.Show(message);
            }   
        }

        public ComboBox FindGuestsForGrade(ComboBox comboBox)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            DateTime dateTimeNow = DateTime.Now;
            foreach(Reservation reservation in reservations)
            {
                if (reservation.EndDate < dateTimeNow && reservation.EndDate.AddDays(5) > dateTimeNow && reservation.GradeStatus == "NotGraded")
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Tag = reservation.Id;
                    item.Content = reservation.GuestUserName;
                    comboBox.Items.Add(item);
                }
                else;
            }

            return comboBox;
        }

        public string FindGradedGuest(int i)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            if (reservations[i].GradeStatus == "Graded")
            {
                string username = reservations[i].GuestUserName;
                return username;
            }
            else return null;
        }

        public bool IsOwnerAlreadyRatedByGuest(int reservationId, string guestUsername)
        {
            OwnerGrade ownerGrade = ownerGradeRepository.GetByReservationId(reservationId);
            if (ownerGrade != null && ownerGrade.Guest1.Username == guestUsername)
            {
                return true;
            }
            return false;
        }

        public User FindGuestByUsername(string username)
        {
            List<User> guests = userRepository.GetAll();
            foreach (User user in guests)
            {
                if (user.Username == username)
                {
                    return user;
                }
                
            }
            return null;
        }

    }
}
