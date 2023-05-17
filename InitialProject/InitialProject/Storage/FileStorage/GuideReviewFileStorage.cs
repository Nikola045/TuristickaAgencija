using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;
using TravelAgency.View.Guest2;

namespace TravelAgency.Storage.FileStorage
{
    internal class GuideReviewFileStorage : IStorage<TourReview1>
    {
        private Serializer<TourReview1> _serializer;
        private readonly string _file = "../../../Resources/Data/guideReview.csv";

        public GuideReviewFileStorage()
        {
            _serializer = new Serializer<TourReview1>();
        }

        public List<TourReview1> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<TourReview1> tourRequests)
        {
            _serializer.ToCSV(_file, tourRequests);
        }
    }
}
