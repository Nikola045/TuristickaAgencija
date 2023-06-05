using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    public class Forum : Serializer.ISerializable
    {
        public int Id { get; set; }
        public User Guest1 { get; set; } = new User();
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }

        public Forum() { }

        public Forum(int id, User guest1, string city, string country, DateTime date)
        {
            Id = id;
            Guest1 = guest1;
            City = city;
            Country = country;
            Date = date;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Guest1.Username, City, Country, Date.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest1.Username = values[1];
            City = values[2];
            Country = values[3];
            Date = Convert.ToDateTime(values[4]);
        }
    }
}

