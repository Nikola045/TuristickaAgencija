using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    class Notification : TravelAgency.Serializer.ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public User Guest2 { get; set; } = new User();
        public string Status { get; set; }

        public Notification() { }
        public Notification(int id, string name, DateTime expirationDate, User guest2, string status)
        {
            Id = id;
            Name = name;
            Guest2 = guest2;
            Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Guest2.Id.ToString(), Status};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Guest2.Id = Convert.ToInt32(values[2]);
            Status = values[3];
        }
    }
}
