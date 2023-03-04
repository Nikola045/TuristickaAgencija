using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class Location : ISerializable
    {
        public string City { get; set; }
        public string Country { get; set; }

        public Location() { }
        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { City, Country};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            City = values[1];
            Country = values[2];
        }
    }
}
