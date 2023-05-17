using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    public class TourRepository
    {
        private IStorage<Tour> _storage;

        private List<Tour> _tours;
        private List<GuestOnTour> _guestsOnTours;

        public TourRepository(IStorage<Tour> storage)
        {
            _storage = storage;
            _tours = _storage.Load();
        }

        public Tour Save(Tour entity)
        {
            _tours.Add(entity);
            _storage.Save(_tours);
            return entity;
        }

        public int NextId()
        {
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(h => h.Id) + 1;
        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public Tour Update(Tour entity)
        {
            Tour current = _tours.Find(c => c.Id == entity.Id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, entity);
            _storage.Save(_tours);
            return entity;
        }

        public void Delete(Tour entity)
        {
            Tour founded = _tours.Find(c => c.Id == entity.Id);
            _tours.Remove(founded);
            _storage.Save(_tours);
        }

    }

}
