using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    public class GuestOnTour : TravelAgency.Serializer.ISerializable
    {
        public int Id { get; set; }
        public User Guest2 { get; set; } = new User();
        public Tour Tour { get; set; } = new Tour();
        public string TourName { get; set; }
        public string StartingPoint { get; set; }
        public int NumOfGuests { get; set; }
        public int GuestAge { get; set; }
        public string WithVoucher { get; set; }
        public List<CheckPoint> CurentCheckPoints { get; set; }

        public GuestOnTour() { }

        public GuestOnTour(int id, User guest2, Tour tour, string tourName, List<CheckPoint> checkPoint, string Status, int numOfGuests, int guestAge, string withVoucher)
        {

            Id = id;
            Guest2 = guest2;
            Tour = tour;
            TourName = tourName;
            CurentCheckPoints = checkPoint;
            StartingPoint = Status;
            NumOfGuests = numOfGuests;
            GuestAge = guestAge;
            WithVoucher = withVoucher;
               
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
            string[] csvValues = { Id.ToString(), Guest2.Id.ToString(), Tour.Id.ToString(), TourName, StartingPoint, NumOfGuests.ToString(), GuestAge.ToString(),WithVoucher, CheckPointsList};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {

            int i = 8;
            int j = 9;
            int k = 10;
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
            Guest2.Id = Convert.ToInt32(values[1]);
            Tour.Id = Convert.ToInt32(values[2]);
            TourName= values[3];
            StartingPoint = values[4];
            NumOfGuests = Convert.ToInt32(values[5]);
            GuestAge = Convert.ToInt32(values[6]);
            WithVoucher = values[7];
        }
    }
}
