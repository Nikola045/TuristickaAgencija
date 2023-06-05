using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Storage.FileStorage
{
    internal class ForumFileStorage : IStorage<Forum>
    {
        private Serializer<Forum> _serializer;
        private readonly string _file = "../../../Resources/Data/forums.csv";

        public ForumFileStorage()
        {
            _serializer = new Serializer<Forum>();
        }

        public List<Forum> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Forum> forums)
        {
            _serializer.ToCSV(_file, forums);
        }
    }
}
