using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    public class GuestGrade : Serializer.ISerializable
    {
        public User Guest1 { get; set; } = new User();
        public int Cleanliness { get; set; }
        public int Respecting { get; set; }
        public string CommentText { get; set; }
        public Reservation Reservation = new Reservation();

        public GuestGrade() { }

        public GuestGrade(User guest1, int cleanilness, int respecting, string commentText, Reservation reservation)
        {
            Guest1 = guest1;
            Cleanliness = cleanilness;
            Respecting = respecting;
            CommentText = commentText;
            Reservation = reservation;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Guest1.Username, Cleanliness.ToString(), Respecting.ToString(), CommentText, Reservation.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Guest1.Username = values[0];
            Cleanliness = Convert.ToInt32(values[1]);
            Respecting = Convert.ToInt32(values[2]);
            CommentText = values[3];
            Reservation.Id = Convert.ToInt32(values[4]);
        }
    }
}
