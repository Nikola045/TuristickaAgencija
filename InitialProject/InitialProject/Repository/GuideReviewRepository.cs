using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    class GuideReviewRepository
    {
        private List<TourReview1> _tourreviews;
        private IStorage<TourReview1> _storage;

        public GuideReviewRepository(IStorage<TourReview1> storage)
        {
            _storage = storage;
            _tourreviews = _storage.Load();
        }

        public TourReview1 Save(TourReview1 entity)
        {
            _tourreviews.Add(entity);
            _storage.Save(_tourreviews);
            return entity;
        }
        public List<TourReview1> GetAll()
        {
            return _tourreviews;
        }
        public int NextId()
        {
            if (_tourreviews.Count < 1)
            {
                return 1;
            }
            return _tourreviews.Max(t => t.Id) + 1;
        }

    }
}


