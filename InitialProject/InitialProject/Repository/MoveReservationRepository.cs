using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    public class MoveReservationRepository
    {
        private List<MoveReservation> reservations;
        private IStorage<MoveReservation> _storage;

        public MoveReservationRepository(IStorage<MoveReservation> storage)
        {
            _storage = storage;
            reservations = _storage.Load();
        }

        public List<MoveReservation> GetAll()
        {
            reservations = _storage.Load();
            return reservations;
        }

        public MoveReservation GetById(int id)
        {
            foreach(MoveReservation reservation in reservations)
            {
                if (reservation.ReservationId == id)
                    return reservation;
            }
            return null;
        }

        public void Delete(MoveReservation reservation)
        {
            MoveReservation founded = reservations.Find(c => c.ReservationId == reservation.ReservationId);
            reservations.Remove(founded);
            _storage.Save(reservations);
        }

        public MoveReservation Save(MoveReservation moveReservation)
        {
            reservations.Add(moveReservation);
            _storage.Save(reservations);
            return moveReservation;
        }
    }
}
