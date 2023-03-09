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
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TypeOfHotel { get; set; }
        public int MaxNumberOfGusets { get; set; }
        public int MinNumberOfGusets { get; set; }
        public int NumberOfDaysToCancel { get; set; }

        public Hotel() { }
        
        public Hotel(int id, string name, string city, string country, string typeOfHotel, int maxNumberOfGusets, int minNumberOfGuests, int numberOfDaysToCancel)
        {
            Id = id;
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
            string[] csvValues = { Id.ToString() , Name, City, Country, TypeOfHotel, MaxNumberOfGusets.ToString(), MinNumberOfGusets.ToString(), NumberOfDaysToCancel.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            City = values[2];
            Country = values[3];
            TypeOfHotel = values[4];
            MaxNumberOfGusets = Convert.ToInt32(values[5]);
            MinNumberOfGusets = Convert.ToInt32(values[6]);
            NumberOfDaysToCancel = Convert.ToInt32(values[7]);

        }

    }
}
