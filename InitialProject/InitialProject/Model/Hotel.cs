using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TravelAgency.Model
{
    public enum Type { Apartment, House, Hut, Hotel }
    public class Hotel
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public Type TypeOfHotel { get; set; }
        public int MaxNumberOfGusets { get; set; }
        public int MinNumberOfGusets { get; set; }
        public int NumberOfDaysToCancel { get; set; }

        public Hotel() { }
        
        public Hotel(string name, Location location, Type typeOfHotel, int maxNumberOfGusets, int minNumberOfGuests, int numberOfDaysToCancel)
        {
            Name = name;
            Location = location;
            TypeOfHotel = typeOfHotel;
            MaxNumberOfGusets = maxNumberOfGusets;
            MinNumberOfGusets = minNumberOfGuests;
            NumberOfDaysToCancel = numberOfDaysToCancel;
        }



    }
}
