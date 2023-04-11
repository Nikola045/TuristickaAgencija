using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Repository;
using TravelAgency.Repository.HotelRepo;

namespace TravelAgency.Services
{
    internal class ReservationService
    {
        private readonly ReservationRepository reservationRepository;
        private readonly HotelRepository hotelRepository;
        private readonly MoveReservationRepository moveReservationRepository;
        private readonly HotelService hotelService;
        public ReservationService() 
        {
            reservationRepository = new ReservationRepository();
            hotelRepository = new HotelRepository();
            moveReservationRepository = new MoveReservationRepository();
            hotelService = new HotelService();
        }

        public void LogicalDelete(Reservation reservation)
        {
            reservation.GradeStatus = "Graded";
            reservationRepository.Update(reservation);
        }

        public void LogicalDeleteExpire(Reservation reservation)
        {
            if (reservation.GradeStatus != "Graded" && reservation.GradeStatus != "Expired")
            {
                reservation.GradeStatus = "Expire";
                reservationRepository.Update(reservation);
            }
        }


        public Reservation FindReservationByID(int id)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            foreach(Reservation reservation in reservations)
            {
                if (reservation.Id == id)
                {
                    return reservation;
                }
            }
            return null;
        }

        public List<DateTime> GetReservedDates(string hotelName)
        {
            List<DateTime> dates = new List<DateTime>();
            List<Reservation> reservations = reservationRepository.GetAll();
            
            foreach (Reservation reservation in reservations)
            {
                if (reservation.HotelName == hotelName)
                {
                    dates.Add(reservation.StartDate);
                    dates.Add(reservation.EndDate);
                }
            }            
            return dates;
        }
        public List<DateTime> GetAvailableDates(string hotelName, DateTime startDate, DateTime endDate, int numberOfDays)
        {
            List<DateTime> availableDates = new List<DateTime>();

            if (!IsAvailable(hotelName, startDate, endDate))
            {
                DateTime currentDate = startDate.AddDays(1);
                DateTime lastDate = endDate.AddDays(-numberOfDays);
                while (currentDate <= lastDate)
                {
                    if (IsAvailable(hotelName, currentDate, currentDate.AddDays(numberOfDays)))
                    {
                        availableDates.Add(currentDate);
                    }
                    currentDate = currentDate.AddDays(1);
                }
            }
            else
            {
                availableDates.Add(startDate);
            }

            return availableDates;
        }
        public List<DateTime> FindAlternativeDates(string hotelName, DateTime checkInDate, DateTime checkOutDate, int numberOfDays)
        {
            List<DateTime> alternativeDates = new List<DateTime>();
            DateTime startDate = checkInDate.AddDays(1);
            DateTime endDate = checkOutDate.AddDays(30);
            while (startDate < endDate)
            {
                if (IsAvailable(hotelName, startDate, startDate.AddDays(numberOfDays)))
                {
                    alternativeDates.Add(startDate);
                    if (alternativeDates.Count == 5)
                    {
                        break;
                    }
                }
                startDate = startDate.AddDays(1);
            }

            return alternativeDates;
        }

        public bool IsAvailable(string hotelName, DateTime startDate, DateTime endDate)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            Hotel hotel = hotelService.GetHotelByName(hotelName);

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                int reservationsForDate = reservations.Where(r => r.HotelName == hotelName && date >= r.StartDate && date <= r.EndDate).Sum(r => r.NumberOfGuests);

                if (reservationsForDate >= hotel.MaxNumberOfGuests)
                {
                    return false;
                }
            }
            return true;
        }

        public void MoveReservation(int id, DateTime newStartDate, DateTime newEndDate)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            foreach(Reservation reservation in reservations)
            {
                if(reservation.Id == id)
                {
                    reservation.StartDate = newStartDate;
                    reservation.EndDate = newEndDate;
                    reservationRepository.Update(reservation);
                    moveReservationRepository.Delete(moveReservationRepository.GetById(id));
                    MessageBox.Show("Reservation seccesfuly changed.");
                }
            }
        }

        public string TextForReservationInfo(string hotelName, DateTime newStartDate, DateTime newEndDate)
        {
            string InfoText;
            string Available;
            if(IsAvailable(hotelName, newStartDate, newEndDate))
            {
                Available = "is available";
            }
            else
            {
                Available = "not available";
            }
            InfoText = hotelName + " " + Available + " for requested period";
            return InfoText;
        }

    }
}
