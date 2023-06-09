using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Repository;
using TravelAgency.Services;
using TravelAgency.Domain.Model;


public class ComplexTour : TravelAgency.Serializer.ISerializable
{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public List<int> ToursIds { get; set; } = new List<int>();


        public ComplexTour(int id, string name, string status, List<int> tours)
        {
            Id = id;
            Name = name;
            Status = status;
            ToursIds = tours;
        }

        public ComplexTour() { }

        public string[] ToCSV()
        {
            string ToursIdsList = null;
            int currentIndex = 0;
            foreach (int point in ToursIds)
            {
                string delimiter = "|";
                if (currentIndex == ToursIds.Count - 1) delimiter = "";
                ToursIdsList = ToursIdsList + point.ToString() + "|" + delimiter;
                currentIndex++;
            }
            string[] csvValues = { Id.ToString(), Name, Status, ToursIdsList};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            int i = 3;
            List<int> tours = new List<int>();
            while (i <= values.Count())
            {
                int tourId;
                tourId = Convert.ToInt32(values[i]);
                i = i + 1;
            }
            ToursIds = tours;
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Status = values[2];
        }


    }

