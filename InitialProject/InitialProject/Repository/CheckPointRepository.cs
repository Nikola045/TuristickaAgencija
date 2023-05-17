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
    public class CheckPointRepository
    {
        private List<CheckPoint> _checkPoints;
        private readonly IStorage<CheckPoint> _storage;

        public CheckPointRepository(IStorage<CheckPoint> storage)
        {
            _storage = storage;
            _checkPoints = _storage.Load(); 
        }

        public List<CheckPoint> GetAll()
        {
            return _checkPoints;
        }

        public CheckPoint Update(CheckPoint entity)
        {
            CheckPoint current = _checkPoints.Find(c => c.Id == entity.Id);
            int index = _checkPoints.IndexOf(current);
            _checkPoints.Remove(current);
            _checkPoints.Insert(index, entity);
            _storage.Save(_checkPoints);
            return entity;
        }
    }
}
