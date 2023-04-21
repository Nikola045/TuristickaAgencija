using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    public class ReservationRepository
    {
        private List<Reservation> reservations;
        private IStorage<Reservation> _storage;

        public ReservationRepository(IStorage<Reservation> storage)
        {
            _storage = storage;
            reservations = _storage.Load();
        }

        public Reservation Save(Reservation reservation)
        {
            reservations.Add(reservation);
            _storage.Save(reservations);
            return reservation;
        }

        public int NextId()
        {
            if (reservations.Count < 1)
            {
                return 1;
            }
            return reservations.Max(r => r.Id) + 1;
        }

        public List<Reservation> GetAll()
        {
            return reservations;
        }

        public void Delete(Reservation reservation)
        {
            Reservation founded = reservations.Find(c => c.Id == reservation.Id);
            reservations.Remove(founded);
            _storage.Save(reservations);
        }

        public Reservation Update(Reservation reservation)
        {
            Reservation current = reservations.Find(c => c.Id == reservation.Id);
            int index = reservations.IndexOf(current);
            reservations.Remove(current);
            reservations.Insert(index, reservation);
            _storage.Save(reservations);
            return reservation;
        }        
    }
}
