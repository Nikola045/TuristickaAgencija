using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;

namespace TravelAgency.Repository
{
    public class RenovationRecommendationRepository
    {
        private readonly List<Recommendation> renovations;
        private readonly IStorage<Recommendation> _storage;

        public RenovationRecommendationRepository(IStorage<Recommendation> storage)
        {
            _storage = storage;
            renovations = _storage.Load();
        }

        public Recommendation Save(Recommendation entity)
        {
            renovations.Add(entity);
            _storage.Save(renovations);
            return entity;
        }

        public List<Recommendation> GetAll()
        {
            return renovations;
        }

        public void Delete(Recommendation entity)
        {
            Recommendation founded = renovations.Find(c => c.Id == entity.Id);
            renovations.Remove(founded);
            _storage.Save(renovations);
        }

        public int NextId()
        {
            if (renovations.Count < 1)
            {
                return 1;
            }
            return renovations.Max(h => h.Id) + 1;
        }
    }
}
