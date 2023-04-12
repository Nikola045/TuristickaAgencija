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

        public CheckPoint CurentCheckPoint { get; set; }

        public GuestOnTour() { }

        public GuestOnTour(int id, int userId, int tourId, string tourName,  CheckPoint checkPoint, string Status, int numOfGuests)
        {

            Id = id;
            GuestId = userId;
            TourId = tourId;
            TourName = tourName;
            CurentCheckPoint = checkPoint;
            StatusCP = Status;
            NumOfGuests = numOfGuests;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), TourId.ToString(), TourName, StatusCP, NumOfGuests.ToString(), CurentCheckPoint.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            TourName= values[3];
            StatusCP = values[4];
            NumOfGuests = Convert.ToInt32(values[5]);
            CurentCheckPoint.Id = Convert.ToInt32(values[6]);
            CurentCheckPoint.Name = values[7];
            CurentCheckPoint.Status = values[8];
        }
    }
}
