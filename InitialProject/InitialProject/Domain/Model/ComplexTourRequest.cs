using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    class ComplexTourRequest : TravelAgency.Serializer.ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public List<TourRequests> RequestToursIds { get; set; } = new List<TourRequests>();


        public ComplexTourRequest(int id, string name, string status, List<TourRequests> tours)
        {
            Id = id;
            Name = name;
            Status = status;
            RequestToursIds = tours;
        }

        public ComplexTourRequest() { }

        public string[] ToCSV()
        {
            string ToursIdsList = null;
            int currentIndex = 0;
            foreach (TourRequests point in RequestToursIds)
            {
                string delimiter = "|";
                if (currentIndex == RequestToursIds.Count - 1) delimiter = "";
                ToursIdsList = ToursIdsList + point.Id.ToString() + delimiter;
                currentIndex++;
            }
            string[] csvValues = { Id.ToString(), Name, Status, ToursIdsList };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            int i = 3;
            List<TourRequests> tourRequests = new List<TourRequests>();
            while (i <= values.Count())
            {
                int tourRequestId;
                tourRequestId = Convert.ToInt32(values[i]);
                i = i + 1;
            }
            RequestToursIds = tourRequests;
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Status = values[2];
        }

    }
}
