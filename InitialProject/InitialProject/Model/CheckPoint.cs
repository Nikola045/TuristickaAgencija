using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal class CheckPoint : Serializer.ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CheckPoint() { }
        public CheckPoint(int id, string name)
        {

            Id = id;
            Name = name;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
        }

        public override string ToString() {
            return Id + "|" + Name;
        }
    }
}
