using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    internal class GuestOnTour : TravelAgency.Serializer.ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string StatusCP { get; set; }
        public int NumOfGuests { get; set; }

        public List<CheckPoint> CurentCheckPoints { get; set; }

        public GuestOnTour() { }

        public GuestOnTour(int id, int userId, int tourId, string tourName, List<CheckPoint> checkPoint, string Status, int numOfGuests)
        {

            Id = id;
            GuestId = userId;
            TourId = tourId;
            TourName = tourName;
            CurentCheckPoints = checkPoint;
            StatusCP = Status;
            NumOfGuests = numOfGuests;
        }

        public string[] ToCSV()
        {
            string CheckPointsList = null;
            int currentIndex = 0;
            foreach (CheckPoint point in CurentCheckPoints)
            {
                string delimiter = "|";
                if (currentIndex == CurentCheckPoints.Count - 1) delimiter = "";
                CheckPointsList = CheckPointsList + point.Id.ToString() + "|" + point.Name + "|" + point.Status + delimiter;
                currentIndex++;
            }
            string[] csvValues = { Id.ToString(), GuestId.ToString(), TourId.ToString(), TourName, StatusCP, NumOfGuests.ToString(), CheckPointsList};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {

            int i = 6;
            int j = 7;
            int k = 8;
            List<CheckPoint> checkPoints = new List<CheckPoint>();
            while (k <= values.Count())
            {
                CheckPoint checkPoint = new CheckPoint();
                checkPoint.Id = Convert.ToInt32(values[i]);
                checkPoint.Name = values[j];
                checkPoint.Status = values[k];
                checkPoints.Add(checkPoint);
                i = i + 3;
                j = j + 3;
                k = k + 3;
            }
            CurentCheckPoints = checkPoints;
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            TourName= values[3];
            StatusCP = values[4];
            NumOfGuests = Convert.ToInt32(values[5]);

        }
    }
}
