using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Serializer;

namespace TravelAgency.Domain.Model
{
    internal class Image : ISerializable
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public Image() { }

        public Image(int id, string url)
        {
            Id = id;
            Url = url;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Url = values[1];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Url };
            return csvValues;
        }

    }
}
