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
    internal class GuestOnTourFileStorage : IStorage<GuestOnTour>
    {
        private Serializer<GuestOnTour> _serializer;
        private readonly string _file = "../../../Resources/Data/guestOnTour.csv";

        public GuestOnTourFileStorage()
        {
            _serializer = new Serializer<GuestOnTour>();
        }

        public List<GuestOnTour> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<GuestOnTour> guests)
        {
            _serializer.ToCSV(_file, guests);
        }
    }
}
