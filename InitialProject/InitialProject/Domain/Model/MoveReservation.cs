using Microsoft.Graph.Drives.Item.Items.Item.GetActivitiesByIntervalWithStartDateTimeWithEndDateTimeWithInterval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Serializer;

namespace TravelAgency.Domain.Model
{
    public class MoveReservation : ISerializable
    {
        public Reservation Reservation { get; set; } = new Reservation();
        public string HotelName { get; set; }
        public string GuestUsername { get; set; }
        public DateTime OldStartDate { get; set; }
        public DateTime OldEndDate { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public string Status { get; set; }
        public MoveReservation() { }
        public MoveReservation(Reservation reservation, string hotelName, string guestUsername, DateTime oldStartDate, DateTime odlEndDate, DateTime newStartDate, DateTime newEndDate)
        {
            Reservation = reservation;
            HotelName = hotelName;
            GuestUsername = guestUsername;
            OldStartDate = oldStartDate;
            OldEndDate = odlEndDate;
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
            Status = "Pending";
        }

        public string[] ToCSV()
        {
            string[] csvValues = {Reservation.Id.ToString(), HotelName, GuestUsername, OldStartDate.ToString(), OldEndDate.ToString(), NewStartDate.ToString(), NewEndDate.ToString(), Status};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Reservation.Id = Convert.ToInt32(values[0]);
            HotelName = values[1];
            GuestUsername = values[2];
            OldStartDate = Convert.ToDateTime(values[3]);
            OldEndDate = Convert.ToDateTime(values[4]);
            NewStartDate = Convert.ToDateTime(values[5]);
            NewEndDate = Convert.ToDateTime(values[6]);
            Status = values[7];
        }
    }
}
