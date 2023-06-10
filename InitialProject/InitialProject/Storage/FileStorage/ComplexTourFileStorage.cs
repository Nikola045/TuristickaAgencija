using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Storage.FileStorage
{
    class ComplexTourFileStorage : IStorage<ComplexTour>
    {
        private Serializer<ComplexTour> _serializer;
        private readonly string _file = "../../../Resources/Data/complexTours.csv";

        public ComplexTourFileStorage()
        {
            _serializer = new Serializer<ComplexTour>();
        }
        public List<ComplexTour> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<ComplexTour> complexTours)
        {
            _serializer.ToCSV(_file, complexTours);
        }
    }
}
