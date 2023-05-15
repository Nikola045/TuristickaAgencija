using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    internal class TourRequests : TravelAgency.Serializer.ISerializable
    {
        public int Id { get; set; }
        public User Guest2 = new User();
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public DateTime FirstTime { get; set; }
        public DateTime SecondTime { get; set; }
        public string Status { get; set; }  



        public TourRequests() { }

        public TourRequests(int id , User guest2, string city, string country, string description, string language, int maxNumberOfGuests, DateTime firstTime, DateTime secondTime)
        {
            Id = id;
            Guest2 = guest2;
            City = city;
            Country = country;
            Description = description;
            Language = language;
            MaxNumberOfGuests = maxNumberOfGuests;
            FirstTime = firstTime;
            SecondTime = secondTime;
            Status = "Pending";
        }

        public string[] ToCSV()
        {
            string CheckPointsList = null;
            int currentIndex = 0;
            
            string[] csvValues = { Id.ToString(), Guest2.Id.ToString(), City, Country, Description, Language, MaxNumberOfGuests.ToString(), FirstTime.ToString(), SecondTime.ToString(), Status};
            return csvValues;
        }



        public void FromCSV(string[] values)
        {
     
            Id = Convert.ToInt32(values[0]);
            Guest2.Id = Convert.ToInt32(values[1]);
            City = values[2];
            Country = values[3];
            Description = values[4];
            Language = values[5];
            MaxNumberOfGuests = Convert.ToInt32(values[6]);
            FirstTime = Convert.ToDateTime(values[7]);
            SecondTime = Convert.ToDateTime(values[8]);
            Status = values[9];

        }
    }
}
