using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    public class VisitedHotel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TypeOfHotel { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public int MinNumberOfDays { get; set; }
        public string GradeStatus { get; set; } 

        public VisitedHotel() { }

        public VisitedHotel(string name, string city, string country, string typeOfHotel, int maxNumberOfGuests, int minNumberOfDays, string gradeStatus)
        {
            Name = name;
            City = city;
            Country = country;
            TypeOfHotel = typeOfHotel;
            MaxNumberOfGuests = maxNumberOfGuests;
            MinNumberOfDays = minNumberOfDays;
            GradeStatus = gradeStatus;
        }    
    }
}
