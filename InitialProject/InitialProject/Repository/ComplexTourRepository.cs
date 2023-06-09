using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.RepositoryInterfaces;

namespace TravelAgency.Repository
{
    class ComplexTourRepository
    {
        private IStorage<ComplexTour> _storage;

        private List<ComplexTour> _tours;

        public ComplexTourRepository(IStorage<ComplexTour> storage)
        {
            _storage = storage;
            _tours = _storage.Load();
        }

        public ComplexTour Save(ComplexTour entity)
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

            return _tours.Max(t => t.Id) + 1;
        }

        public List<ComplexTour> GetAll()
        {
            return _tours;
        }

        public ComplexTour Update(ComplexTour entity)
        {
            ComplexTour current = _tours.Find(c => c.Id == entity.Id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, entity);
            _storage.Save(_tours);
            return entity;
        }

        public void Delete(ComplexTour entity)
        {
            ComplexTour founded = _tours.Find(c => c.Id == entity.Id);
            _tours.Remove(founded);
            _storage.Save(_tours);
        }
    }
}
