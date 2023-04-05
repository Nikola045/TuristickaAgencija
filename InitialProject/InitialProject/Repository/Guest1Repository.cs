using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    public class Guest1Repository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<GuestGrade> _serializer;

        

        public List<User> FindAllGusts1()
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
        public Guest1Repository() { }

        public Guest1 FindByUsername(string username)
        {
            List<User> users = FindAllGusts1();
            Guest1 guest1 = null;
            foreach (User user in users) 
            {
                if (user.Username == username)
                {
                    guest1.Id = user.Id;
                    guest1.Username = user.Username;
                    guest1.Password = user.Password;
                    return guest1;
                }
                    
            }
            return guest1;
        }

    }
}
