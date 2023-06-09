using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Serializer;

namespace TravelAgency.Domain.Model
{
    public class OwnerGrade : ISerializable
    {
        public User Guest1 { get; set; } = new User();
        public User Owner { get; set; } = new User();
        public Reservation Reservation { get; set; } = new Reservation();
        public int HotelRating { get; set; }
        public int OwnerRating { get; set; }
        public string Comment { get; set; }

        public OwnerGrade() { }

        public OwnerGrade(User guset1, User owner, Reservation reservation, int hotelRating, int ownerRating, string comment)
        {
            Guest1 = guset1;
            Owner = owner;
            Reservation = reservation;
            HotelRating = hotelRating;
            OwnerRating = ownerRating;
            Comment = comment;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Guest1.Username, Owner.Username, Reservation.Id.ToString(), HotelRating.ToString(), OwnerRating.ToString(), Comment };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Guest1.Username = values[0];
            Owner.Username = values[1];
            Reservation.Id = Convert.ToInt32(values[2]);
            HotelRating = Convert.ToInt32(values[3]);
            OwnerRating = Convert.ToInt32(values[4]);
            Comment = values[5];
        }

    }
}
