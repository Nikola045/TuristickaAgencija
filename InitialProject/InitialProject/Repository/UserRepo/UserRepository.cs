using TravelAgency.Serializer;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Domain.Model;
using System.IO;
using System;

namespace TravelAgency.Repository.UserRepo
{
    public class UserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetAll() 
        {
            List<User> users = new List<User>();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] fields = line.Split('|');
                    User user = new User();
                    user.Id = Convert.ToInt32(fields[0]);
                    user.Username = fields[1];
                    user.Password = fields[2];
                    user.LoginRole = fields[3];
                    users.Add(user);
                }
            }
            return users;
        }
    }
}
