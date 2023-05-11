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
    internal class HotelFileStorage : IStorage<Hotel>
    {
        private Serializer<Hotel> _serializer;
        private readonly string _file = "../../../Resources/Data/hotels.csv";

        public HotelFileStorage()
        {
            _serializer = new Serializer<Hotel>();
        }

        public List<Hotel> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Hotel> accommodations)
        {
            _serializer.ToCSV(_file, accommodations);
        }
    }
}
