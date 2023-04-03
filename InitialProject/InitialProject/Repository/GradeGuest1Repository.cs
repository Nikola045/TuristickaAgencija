using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    class GradeGuest1Repository
    {
        private const string FilePathGuestRatingde = "../../../Resources/Data/guestRating.csv";

        private readonly Serializer<GuestGrade> _serializer;

        private readonly ReservationRepository reservationRepository;

        private List<GuestGrade> grades;

        public GradeGuest1Repository()
        {
            _serializer = new Serializer<GuestGrade>();
            grades = _serializer.FromCSV(FilePathGuestRatingde);
            reservationRepository = new ReservationRepository();
        }

        public GuestGrade Save(GuestGrade grade)
        {
            grades = _serializer.FromCSV(FilePathGuestRatingde);
            grades.Add(grade);
            _serializer.ToCSV(FilePathGuestRatingde, grades);
            return grade;
        }

        public void FindAndLogicalDeleteExpiredReservation(int i)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.ReadFromReservationsCsv();
            DateTime dateTimeNow = DateTime.Now;

            if (reservations[i].EndDate < dateTimeNow && reservations[i].EndDate.AddDays(5) < dateTimeNow)
            {
                reservationRepository.LogicalDeleteExpire(reservations[i]);
            }
        }

        public string ShowMessageForGrade(int i)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.ReadFromReservationsCsv();
            DateTime dateTimeNow = DateTime.Now;
            string message = null;
            if (reservations[i].EndDate < dateTimeNow && reservations[i].EndDate.AddDays(5) > dateTimeNow && reservations[i].GradeStatus == "NotGraded")
            {
                
                message = "You have " + (5 - (dateTimeNow.Day - reservations[i].EndDate.Day)).ToString() + " days left to grade " + reservations[i].GuestUserName;
            }
            return message;
        }

        public string FindGuestsForGrade(int i)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.ReadFromReservationsCsv();
            DateTime dateTimeNow = DateTime.Now;
            if (reservations[i].EndDate < dateTimeNow && reservations[i].EndDate.AddDays(5) > dateTimeNow && reservations[i].GradeStatus == "NotGraded")
            {
                string reservationForm = reservations[i].Id.ToString() + " " + reservations[i].GuestUserName;
                return reservationForm;
            }
            else return null;
        }

        public string FindGradedGuest(int i)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations = reservationRepository.ReadFromReservationsCsv();
            if (reservations[i].GradeStatus == "Graded")
            {
                string username = reservations[i].GuestUserName;
                return username;
            }
            else return null;
        }


    }
}
