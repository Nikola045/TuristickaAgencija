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
    internal class CheckPointFileStorage : IStorage<CheckPoint>
    {
        private Serializer<CheckPoint> _serializer;
        private readonly string _file = "../../../Resources/Data/checkPoints.csv";

        public CheckPointFileStorage()
        {
            _serializer = new Serializer<CheckPoint>();
        }

        public List<CheckPoint> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<CheckPoint> checkPoints)
        {
            _serializer.ToCSV(_file, checkPoints);
        }
    }
}
