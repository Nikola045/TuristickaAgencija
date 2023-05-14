using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;

namespace TravelAgency.Repository
{
    public class RenovationRequestRepository
    {
        private readonly List<RenovationRequest> renovations;
        private readonly IStorage<RenovationRequest> _storage;

        public RenovationRequestRepository(IStorage<RenovationRequest> storage)
        {
            _storage = storage;
            renovations = _storage.Load();     
        }

        public RenovationRequest Save(RenovationRequest entity)
        {
            renovations.Add(entity);
            _storage.Save(renovations);
            return entity;
        }

        public List<RenovationRequest> GetAll()
        {
            return renovations;
        }

        public void Delete(RenovationRequest entity)
        {
            RenovationRequest founded = renovations.Find(c => c.Id == entity.Id);
            renovations.Remove(founded);
            _storage.Save(renovations);
        }

    }
}
