using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    internal class TourRequestsRepository
    {

        private const string FilePath = "../../../Resources/Data/tourRequests.csv";

        private readonly Serializer<TourRequests> _serializer;

        private List<TourRequests> _tourRequests;


        public TourRequestsRepository()
        {
            _serializer = new Serializer<TourRequests>();
            _tourRequests = _serializer.FromCSV(FilePath);
        }


        public TourRequests Save(TourRequests tourRequest)
        {
            tourRequest.Id = NextId();
            _tourRequests = _serializer.FromCSV(FilePath);
            _tourRequests.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
            return tourRequest;
        }

        public int NextId()
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            if (_tourRequests.Count < 1)
            {
                return 1;
            }
            return _tourRequests.Max(t => t.Id) + 1;
        }


        public List<TourRequests> MyRequests(int id)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            List<TourRequests> tourRequests = new List<TourRequests>();

            for (int i = 0; i < _tourRequests.Count(); i++)
            {
                if (_tourRequests[i].Guest2.Id == id && _tourRequests[i].Status == "Pending")
                {
                    tourRequests.Add(_tourRequests[i]);
                }
            }

            return tourRequests;
        }


    }
}
