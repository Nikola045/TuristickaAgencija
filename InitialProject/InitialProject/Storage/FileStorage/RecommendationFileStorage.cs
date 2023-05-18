using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;
using TravelAgency.Domain.RepositoryInterfaces;

namespace TravelAgency.Storage.FileStorage
{
    internal class RecommendationFileStorage : IStorage<Recommendation>
    {
        private Serializer<Recommendation> _serializer;
        private readonly string _file = "../../../Resources/Data/renovationRecommendations.csv";
        public RecommendationFileStorage()
        {
            _serializer = new Serializer<Recommendation>();
        }
        public List<Recommendation> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Recommendation> list)
        {
            _serializer.ToCSV(_file, list);
        }
    }
}
