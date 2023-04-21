using TravelAgency.Serializer;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Domain.Model;
using System.IO;
using System;
using TravelAgency.Domain.RepositoryInterfaces;

namespace TravelAgency.Repository.UserRepo
{
    public class UserRepository
    {
        private List<User> users;
        private IStorage<User> _storage;

        public UserRepository(IStorage<User> storage)
        {
            _storage = storage;
            users = _storage.Load();
        }

        public User GetByUsername(string username)
        {
            return users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetAll() 
        {
            return users;
        }
    }
}
