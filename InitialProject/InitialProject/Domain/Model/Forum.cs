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
        public int NumberOfReplies { get; set; }
        public bool IsActive { get; set; }
        public bool VeryUseful { get; set; }  
        public Forum() { }

        public Forum(string city, string country)
        {
            City = city;
            Country = country;
            NumberOfReplies = 0;
            IsActive = true;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Guest1.Username, City, Country, Date.ToString(),NumberOfReplies.ToString(),IsActive.ToString(),VeryUseful.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest1.Username = values[1];
            City = values[2];
            Country = values[3];
            Date = Convert.ToDateTime(values[4]);
            NumberOfReplies = Convert.ToInt32(values[5]);
            IsActive = Convert.ToBoolean(values[6]);
            VeryUseful = Convert.ToBoolean(values[7]);
        }
    }
}

