using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Storage.FileStorage
{
    internal class TourRequestsFileStorage
    {
        private Serializer<TourRequests> _serializer;
        private readonly string _file = "../../../Resources/Data/tourRequests.csv";

        public TourRequestsFileStorage()
        {
            _serializer = new Serializer<TourRequests>();
        }

        public List<TourRequests> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<TourRequests> vouchers)
        {
            _serializer.ToCSV(_file, vouchers);
        }
    }
}
