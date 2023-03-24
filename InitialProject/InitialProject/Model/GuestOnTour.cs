using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal class GuestOnTour
    {
        public int Id { get; set; } 
        public string GuestUserName { get; set; }    
        public int TourId { get; set; }
        public CheckPoint CurentCheckPoint { get; set; }
        public string StatusCP { get; set; } 

        public GuestOnTour() { }

        public GuestOnTour(int id, string userName, int tourId, CheckPoint checkPoint, string Status) {
        
            Id = id;
            GuestUserName = userName;
            TourId = tourId;
            CurentCheckPoint = checkPoint;
            StatusCP = Status;

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestUserName, TourId.ToString(), CurentCheckPoint.ToString(), StatusCP };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestUserName = values[1];
            TourId= Convert.ToInt32(values[2]);
            CurentCheckPoint.Id = Convert.ToInt32(values[3]);
            CurentCheckPoint.Name= values[4];
            CurentCheckPoint.Status= values[5];
            StatusCP = values[6];
        }




    }

}
