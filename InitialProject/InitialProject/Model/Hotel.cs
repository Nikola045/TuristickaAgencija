using TravelAgency.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using TravelAgency.Serializer;

namespace TravelAgency.Model
{
    public class Hotel : TravelAgency.Serializer.ISerializable
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TypeOfHotel { get; set; }
        public int MaxNumberOfGusets { get; set; }
        public int MinNumberOfGusets { get; set; }
        public int NumberOfDaysToCancel { get; set; }

        public Hotel() { }
        
        public Hotel(string name, string city, string country, string typeOfHotel, int maxNumberOfGusets, int minNumberOfGuests, int numberOfDaysToCancel)
        {
            Name = name;
            City = city;
            Country = country;
            TypeOfHotel = typeOfHotel;
            MaxNumberOfGusets = maxNumberOfGusets;
            MinNumberOfGusets = minNumberOfGuests;
            NumberOfDaysToCancel = numberOfDaysToCancel;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Name, City, Country, TypeOfHotel, MaxNumberOfGusets.ToString(), MinNumberOfGusets.ToString(), NumberOfDaysToCancel.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Name = values[0];
            City = values[1];
            Country = values[2];
            TypeOfHotel = values[3];
            MaxNumberOfGusets = Convert.ToInt32(values[4]);
            MinNumberOfGusets = Convert.ToInt32(values[5]);
            NumberOfDaysToCancel = Convert.ToInt32(values[6]);

        }

    }
}
