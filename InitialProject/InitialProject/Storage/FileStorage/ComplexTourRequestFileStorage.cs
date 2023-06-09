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
    class ComplexTourRequestFileStorage : IStorage<ComplexTourRequest>
    {
        private Serializer<ComplexTourRequest> _serializer;
        private readonly string _file = "../../../Resources/Data/complexTourRequests.csv";

        public ComplexTourRequestFileStorage()
        {
            _serializer = new Serializer<ComplexTourRequest>();
        }

        public List<ComplexTourRequest> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<ComplexTourRequest> vouchers)
        {
            _serializer.ToCSV(_file, vouchers);
        }
    
    }
}
