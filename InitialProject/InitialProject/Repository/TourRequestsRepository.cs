using System.Collections.Generic;
using System.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    internal class TourRequestsRepository
    {

        private const string FilePath = "../../../Resources/Data/tourRequests.csv";

        private readonly Serializer<TourRequests> _serializer;
        private readonly IStorage<TourRequests> _storage;

        private List<TourRequests> _tourRequests;


        public TourRequestsRepository(IStorage<TourRequests> storage)
        {
            _serializer = new Serializer<TourRequests>();
            _tourRequests = _serializer.FromCSV(FilePath);
            _storage = storage;
        }


        public TourRequests Save(TourRequests entity)
        {
            _tourRequests.Add(entity);
            _storage.Save(_tourRequests);
            return entity;
        }
        public TourRequests Update(TourRequests entity)
        {
            TourRequests current = _tourRequests.Find(c => c.Id == entity.Id);
            int index = _tourRequests.IndexOf(current);
            _tourRequests.Remove(current);
            _tourRequests.Insert(index, entity);
            _storage.Save(_tourRequests);
            return entity;
        }

        public int NextId()
        {
            if (_tourRequests.Count < 1)
            {
                return 1;
            }
            return _tourRequests.Max(h => h.Id) + 1;
        }

        public List<TourRequests> GetAll()
        {
            return _tourRequests;
        }
    }
}
