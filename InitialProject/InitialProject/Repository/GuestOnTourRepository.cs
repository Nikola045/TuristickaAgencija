using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;

namespace TravelAgency.Repository
{
    public class GuestOnTourRepository
    {
        private readonly List<GuestOnTour> guests;
        private readonly IStorage<GuestOnTour> _storage;
        public GuestOnTourRepository(IStorage<GuestOnTour> storage)
        {
            _storage = storage;
            guests = _storage.Load();
        }

        public int NextId()
        {
            if (guests.Count < 1)
            {
                return 1;
            }
            return guests.Max(t => t.Id) + 1;
        }

        public List<GuestOnTour> GetAll()
        {
            return guests;
        }


        public GuestOnTour Update(GuestOnTour entity)
        {
            GuestOnTour current = guests.Find(c => c.Id == entity.Id);
            int index = guests.IndexOf(current);
            guests.Remove(current);
            guests.Insert(index, entity);
            _storage.Save(guests);
            return entity;
        }

        public GuestOnTour Save(GuestOnTour entity)
        {
            guests.Add(entity);
            _storage.Save(guests);
            return entity;
        }
    }
}
