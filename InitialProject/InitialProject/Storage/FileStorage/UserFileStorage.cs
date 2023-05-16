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
    internal class UserFileStorage : IStorage<User>
    {
        private Serializer<User> _serializer;
        private readonly string _file = "../../../Resources/Data/users.csv";

        public UserFileStorage()
        {
            _serializer = new Serializer<User>();
        }

        public List<User> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<User> users)
        {
            _serializer.ToCSV(_file, users);
        }
    }
}
