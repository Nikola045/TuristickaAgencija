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
        public string Status { get; set; }
        public List<int> RequestToursIds { get; set; } = new List<int>();


        public ComplexTourRequest(int id, List<int> tours)
        {
            Id = id;
            Status = "Pending";
            RequestToursIds = tours;
        }

        public ComplexTourRequest() { }

        public string[] ToCSV()
        {
            string ToursIdsList = null;
            int currentIndex = 0;
            foreach (int point in RequestToursIds)
            {
                string delimiter = "|";
                if (currentIndex == RequestToursIds.Count - 1) delimiter = "";
                ToursIdsList = ToursIdsList + point.ToString() + delimiter;
                currentIndex++;
            }
            string[] csvValues = { Id.ToString(), Status, ToursIdsList };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            int i = 2;
            List<int> tourRequests = new List<int>();
            RequestToursIds = tourRequests;
            Id = Convert.ToInt32(values[0]);
            Status = values[1];
        }

    }
}
