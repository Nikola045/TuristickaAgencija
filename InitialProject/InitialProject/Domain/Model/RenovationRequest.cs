using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Serializer;

namespace TravelAgency.Domain.Model
{
    public class RenovationRequest : ISerializable
    {
        public int Id { get; set; }
        public Hotel Hotel { get; set; } = new Hotel();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RenovationRequest() { }
        public RenovationRequest(int id, Hotel hotel, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Hotel = hotel;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Hotel.Id.ToString(), Hotel.Name, StartDate.ToString(), EndDate.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Hotel.Id = Convert.ToInt32(values[1]);
            Hotel.Name = values[2];
            StartDate = Convert.ToDateTime(values[3]);
            EndDate = Convert.ToDateTime(values[4]);
        }
    }
}
