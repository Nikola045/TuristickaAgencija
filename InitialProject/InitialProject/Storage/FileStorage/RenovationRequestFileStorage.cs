using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Storage.FileStorage
{
    internal class RenovationRequestFileStorage : IStorage<RenovationRequest>
    {
        private Serializer<RenovationRequest> _serializer;
        private readonly string _file = "../../../Resources/Data/renovationRequests.csv";
        public RenovationRequestFileStorage()
        {
            _serializer = new Serializer<RenovationRequest>();
        }
        public List<RenovationRequest> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<RenovationRequest> list)
        {
            _serializer.ToCSV(_file, list);
        }
    }
}
