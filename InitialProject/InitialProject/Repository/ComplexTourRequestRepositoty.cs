using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    class ComplexTourRequestRepositoty
    {
        private const string FilePath = "../../../Resources/Data/complexTourRequests.csv";

        private readonly Serializer<ComplexTourRequest> _serializer;
        private readonly IStorage<ComplexTourRequest> _storage;

        private List<ComplexTourRequest> _tourRequests;


        public ComplexTourRequestRepositoty(IStorage<ComplexTourRequest> storage)
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _tourRequests = _serializer.FromCSV(FilePath);
            _storage = storage;
        }



        public ComplexTourRequest Save(ComplexTourRequest entity)
        {
            _tourRequests.Add(entity);
            _storage.Save(_tourRequests);
            return entity;
        }
        public ComplexTourRequest Update(ComplexTourRequest entity)
        {
            ComplexTourRequest current = _tourRequests.Find(c => c.Id == entity.Id);
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

        public List<ComplexTourRequest> GetAll()
        {
            return _tourRequests;
        }
    }
}
