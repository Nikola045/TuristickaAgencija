using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal class CheckPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CheckPoint() { }
        public CheckPoint(int id, string name)
        {

            Id = id;
            Name = name;
        }
    }
}
