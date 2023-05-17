using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Storage.FileStorage;

namespace TravelAgency.Services
{
    internal class ReservationService
    { 
        public HotelRepository hotelRepository;
        private ReservationRepository reservationRepository;
        private MoveReservationRepository moveReservationRepository;
        private HotelService hotelService;
        private RenovationRequestRepository renovationRequestRepository;
        public ReservationService() 
        {
            reservationRepository = new(InjectorService.CreateInstance<IStorage<Reservation>>());
            hotelRepository = new(InjectorService.CreateInstance<IStorage<Hotel>>());
            moveReservationRepository = new(InjectorService.CreateInstance<IStorage<MoveReservation>>());
            renovationRequestRepository = new(InjectorService.CreateInstance<IStorage<RenovationRequest>>());
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
                    reservation.NumberOfMuveReservation++;
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
                    RenovationRequest renovationRequest = new RenovationRequest(
                        renovationRequestRepository.NextId(),
                        hotelService.GetHotelById(Convert.ToInt32(comboBoxItem.Tag.ToString())),
                        startDate, 
                        endDate);
                    renovationRequestRepository.Save(renovationRequest);
                    MessageBox.Show("Success");
                }
            }
            else { MessageBox.Show("Error"); }
        }

        public List<RenovationRequest> ShowAllRenovationForOwnerHotels()
        {
            List<RenovationRequest> renovations = renovationRequestRepository.GetAll();
            foreach (RenovationRequest renovation in renovations)
            {
                renovation.Hotel.Name = hotelService.GetHotelById(renovation.Hotel.Id).Name;
            }
            return renovations;
        }

        public void CancelRenovation(RenovationRequest renovation)
        {   
            if(renovation != null)
            {
                DateTime dateTime = DateTime.Now;
                if (renovation.StartDate.Day - dateTime.Day > 5)
                {
                    renovationRequestRepository.Delete(renovation);
                    MessageBox.Show("Sucess");
                }
                else
                {
                    MessageBox.Show("You can't cancel renovation.");
                }
            }
        }

        public List<Hotel> IsHotelRenovated()
        {
            DateTime dateTime = DateTime.Now;
            List<Hotel> hotels = hotelRepository.GetAll();
            List<Hotel> changedHotels = new List<Hotel>();
            List<RenovationRequest> renovations = renovationRequestRepository.GetAll(); 
            foreach (Hotel hotel in hotels)
                {
                    foreach (RenovationRequest renovation in renovations)
                    {
                        if (hotel.Id == renovation.Hotel.Id)
                        {
                            if (renovation.StartDate <= dateTime && dateTime <= renovation.EndDate)
                            {
                                hotel.RenovationStatus = "Is Renovationg";
                                changedHotels.Add(hotel);   
                            }
                            else if (dateTime >= renovation.EndDate && dateTime.Year - renovation.EndDate.Year < 1)
                            {
                                hotel.RenovationStatus = "Renovated";
                                changedHotels.Add(hotel);
                            }
                            else if(dateTime < renovation.EndDate && dateTime.Year - renovation.EndDate.Year == 0)
                            {
                                hotel.RenovationStatus = "Not Renovated";
                                changedHotels.Add(hotel);
                            }
                            else
                            {
                                hotel.RenovationStatus = "Not Renovated";
                                changedHotels.Add(hotel);
                                renovationRequestRepository.Delete(renovation);
                        }

                        }
                        if (renovations.Count == 0) break;
                    }
            }
            return changedHotels;
        }
        public void ChangeAllRenovatedStatus()
        {
            List<Hotel> hotels = IsHotelRenovated();
            foreach(Hotel hotel in hotels)
            {
                hotelRepository.Update(hotel);
            }
        }

        public List<ColumnSeries> ShowHotelReservationInChart(string hotelName)
        {
            DateTime dateTime = DateTime.Now;
            List<int> yValuesReservation = new List<int>();
            List<int> yValuesMoveReservation = new List<int>();
            List<int> yValuesRenovation = new List<int>();
            List<int> yValuesCanceledReservation = new List<int>();
            List<Reservation> reservations = reservationRepository.GetAll();
            for (int i = 0; i < 5; i++)
            {
                int tempCount = 0;
                int tempM = 0;
                int tempIs = 0;
                int tempR = 0;
                foreach (Reservation reservation in reservations)
                {
                    if (reservation.HotelName == hotelName && reservation.StartDate.Year == dateTime.Year - 4 + i)
                    {
                        
                        if(reservation.IsCanceled == 1)
                            tempIs++;
                        else tempCount++;
                        tempM += reservation.NumberOfMuveReservation;
                        tempR += reservation.NumberOfRenovationRequest;
                    } 
                }
                yValuesReservation.Add(tempCount);
                yValuesCanceledReservation.Add(tempIs);
                yValuesMoveReservation.Add(tempM);
                yValuesRenovation.Add(tempR);
            }
            ColumnSeries columnSeries1 = new ColumnSeries();
            columnSeries1.Title = hotelName + ": reservations";
            columnSeries1.Values = new ChartValues<int>(yValuesReservation);
            ColumnSeries columnSeries2 = new ColumnSeries();
            columnSeries2.Title = hotelName + ": canceled reservations";
            columnSeries2.Values = new ChartValues<int>(yValuesCanceledReservation);
            ColumnSeries columnSeries3 = new ColumnSeries();
            columnSeries3.Title = hotelName + ": move reservations";
            columnSeries3.Values = new ChartValues<int>(yValuesMoveReservation);
            ColumnSeries columnSeries4 = new ColumnSeries();
            columnSeries4.Title = hotelName + ": renovation request";
            columnSeries4.Values = new ChartValues<int>(yValuesRenovation);

            List<ColumnSeries> columns = new List<ColumnSeries>();
            columns.Add(columnSeries1);
            columns.Add(columnSeries2);
            columns.Add(columnSeries3);
            columns.Add(columnSeries4);
            return columns;
        }

        public List<int> ShowHotelReservationPerMonth(string hotelName, int year)
        {
            List<int> yValues = new List<int>();
            List<Reservation> reservations = reservationRepository.GetAll();
            for (int i = 1; i <= 12; i++)
            {
                int tempCount = 0;
                foreach (Reservation reservation in reservations)
                {
                    if (reservation.HotelName == hotelName && reservation.StartDate.Month == i && year == reservation.StartDate.Year)
                    {
                        tempCount++;
                    }
                }
                yValues.Add(tempCount);
            }
            return yValues;
        }
    }
}
