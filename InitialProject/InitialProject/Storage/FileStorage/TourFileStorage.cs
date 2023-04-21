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
    internal class TourFileStorage : IStorage<Tour>
    {
        private Serializer<Tour> _serializer;
        private readonly string _file = "../../../Resources/Data/tours.csv";

        public TourFileStorage()
        {
            _serializer = new Serializer<Tour>();
        }

        public List<Tour> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Tour> tours)
        {
            _serializer.ToCSV(_file, tours);
        }
    }
}
