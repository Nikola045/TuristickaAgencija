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
    internal class MoveReservationFileStorage : IStorage<MoveReservation>
    {
        private Serializer<MoveReservation> _serializer;
        private readonly string _file = "../../../Resources/Data/moveReservationRequest.csv";

        public MoveReservationFileStorage()
        {
            _serializer = new Serializer<MoveReservation>();
        }

        public List<MoveReservation> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<MoveReservation> reservations)
        {
            _serializer.ToCSV(_file, reservations);
        }
    }
}
