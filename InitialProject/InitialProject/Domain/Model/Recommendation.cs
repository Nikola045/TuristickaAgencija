using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Serializer;

namespace TravelAgency.Domain.Model
{
    public class Recommendation : ISerializable
    {
        public int Id { get; set; }
        public int IdReservation { get; set; }
        public string HotelName { get; set; }
        public string Info { get; set; }
        public int Level { get; set; }
        public Recommendation() { }
        public Recommendation(int id, int idReservation, string hotel, string info, int level)
        {
            Id = id;
            IdReservation = idReservation;
            HotelName = hotel;
            Info = info;
            Level = level;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), IdReservation.ToString(), HotelName, Info, Level.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IdReservation = Convert.ToInt32(values[1]);
            HotelName = values[2];
            Info = values[3];
            Level = Convert.ToInt32(values[4]);
        }
    }
}
