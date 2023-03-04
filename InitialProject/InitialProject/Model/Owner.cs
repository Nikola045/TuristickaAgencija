using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    public class Owner : ISerializable
    {
        public enum Role {Owner,Guset1,Guset2,Guide}
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role LoginRole { get; set; }

        public Owner() { }

        public Owner(string username, string password, Role loginRole)
        {
            Username = username;
            Password = password;
            LoginRole = loginRole;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, LoginRole.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
        }
    }
}
