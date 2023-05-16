using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Model
{
    public class Voucher : TravelAgency.Serializer.ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public User Guest2 { get; set; } = new User();

        public Voucher() { }
        public Voucher(int id, string name, DateTime expirationDate, User guest2)
        {
            Id = id;
            Name = name;
            ExpirationDate = expirationDate;
            Guest2 = guest2;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, ExpirationDate.ToString() , Guest2.Id.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            ExpirationDate = Convert.ToDateTime(values[2]);
            Guest2.Id = Convert.ToInt32(values[3]);
        }


    }
}
