using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Domain.Model;
using TravelAgency.Repository;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Storage.FileStorage;

namespace TravelAgency.Services
{
    internal class ReservationService
    {
        private readonly App app = (App)App.Current;
        public HotelRepository hotelRepository;
        private ReservationRepository reservationRepository;
        private MoveReservationRepository moveReservationRepository;
        private HotelService hotelService;
        private RenovationRequestRepository renovationRequestRepository;
        public ReservationService() 
        {
            reservationRepository = app.ReservationRepository;
            hotelRepository = app.HotelRepository;
            moveReservationRepository = app.MoveReservationRepository;
            renovationRequestRepository = app.RenovationRequestRepository;
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
            List<Reservation> reservations = reservationRepository.GetAll();

            if (!IsAvailable(reservations, hotelName, startDate, endDate))
            {
                DateTime currentDate = startDate.AddDays(1);
                DateTime lastDate = endDate.AddDays(-numberOfDays);
                while (currentDate <= lastDate)
                {
                    if (IsAvailable(reservations, hotelName, currentDate, currentDate.AddDays(numberOfDays)))
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
            List<Reservation> reservations = reservationRepository.GetAll();
            DateTime startDate = checkInDate.AddDays(1);
            DateTime endDate = checkOutDate.AddDays(30);
            while (startDate < endDate)
            {
                if (IsAvailable(reservations, hotelName, startDate, startDate.AddDays(numberOfDays)))
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

        public bool IsAvailable(List<Reservation> reservations, string hotelName, DateTime startDate, DateTime endDate)
        {
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
                    break;
                }
            }
        }

        public string TextForReservationInfo(int reservationId, string hotelName, DateTime newStartDate, DateTime newEndDate)
        {
                string InfoText;
                string Available;
                List<Reservation> reservations = reservationRepository.GetAll();
                List<Reservation> reservationData = new List<Reservation>();
                foreach (Reservation reservation in reservations)
                {
                    if (reservation.Id != reservationId)
                        reservationData.Add(reservation);

                }
                if (IsAvailable(reservationData, hotelName, newStartDate, newEndDate))
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

        public List<Reservation> FindReservationByGuestUsername(string username)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            List<Reservation> findedReservation = new List<Reservation>();
            DateTime dateTime = DateTime.Now;
            foreach (Reservation reservation in reservations)
            {
                if(reservation.GuestUserName == username && reservation.EndDate > dateTime && reservation.GradeStatus == "NotGraded")
                {
                    findedReservation.Add(reservation);
                }
            }
            return findedReservation;
        }

        public void ReserveRenovation(ComboBox comboBox,DateTime startDate, DateTime endDate)
        {
            ComboBoxItem comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
            List<Reservation> reservations = reservationRepository.GetAll();
            if(!(startDate > endDate))
            {
                if (IsAvailable(reservations, comboBoxItem.Content.ToString(), startDate, endDate))
                {
                    RenovationRequest renovationRequest = new RenovationRequest(Convert.ToInt32(comboBoxItem.Tag), comboBoxItem.Content.ToString(), startDate, endDate);
                    renovationRequestRepository.Save(renovationRequest);
                    MessageBox.Show("Success");
                }
            }
            else { MessageBox.Show("Error"); }
        }

        public List<RenovationRequest> ShowAllRenovationForOwnerHotels()
        {
            DateTime dateTime = DateTime.Now;
            List<RenovationRequest> renovations = renovationRequestRepository.GetAll();
            List<RenovationRequest> futureRenovatin = new List<RenovationRequest>();
            foreach(RenovationRequest renovation in renovations)
            {
                if(renovation.EndDate > dateTime)
                {
                    futureRenovatin.Add(renovation);
                }
            }
            return futureRenovatin;
        }

        public void CancelRenovation(RenovationRequest renovation)
        {
            DateTime dateTime = DateTime.Now;
            if(renovation.StartDate.Day - dateTime.Day > 5 )
            {
                renovationRequestRepository.Delete(renovation);
            }
        }

        public void IsHotelRenovated()
        {
            DateTime dateTime = DateTime.Now;
            List<Hotel> hotels = hotelRepository.GetAll();
            List<RenovationRequest> renovations = renovationRequestRepository.GetAll(); 
            if(renovations.Count != 0 && hotels.Count != 0)
            {
                foreach (Hotel hotel in hotels)
                {
                    foreach (RenovationRequest renovation in renovations)
                    {
                        if (hotel.Id == renovation.Id)
                        {
                            if (renovation.StartDate <= dateTime && dateTime <= renovation.EndDate)
                            {
                                hotel.RenovationStatus = "Is Renovationg";
                            }
                            else if (dateTime >= renovation.EndDate && dateTime.Year - renovation.EndDate.Year < 1)
                            {
                                hotel.RenovationStatus = "Renovated";
                            }
                            else
                            {
                                hotel.RenovationStatus = "Not Renovated";
                                renovationRequestRepository.Delete(renovation);
                            }
                        }
                        if (renovations.Count == 0) break;
                    }
                }
            }
        }
    }
}
