using TravelAgency.Serializer;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Domain.Model;
using System.IO;
using System;
using TravelAgency.Domain.RepositoryInterfaces;
using Cake.Core.IO;
using System.Xml.Linq;

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

        public User Update(User entity)
        {
            User current = users.Find(c => c.Id == entity.Id);
            int index = users.IndexOf(current);
            users.Remove(current);
            users.Insert(index, entity);
            _storage.Save(users);
            return entity;
        }
        public void Delete(User user)
        {
            users = _storage.Load();
            User founded = users.Find(c => c.Id == user.Id);
            users.Remove(founded);
            _storage.Save(users);
        }
    }
}
