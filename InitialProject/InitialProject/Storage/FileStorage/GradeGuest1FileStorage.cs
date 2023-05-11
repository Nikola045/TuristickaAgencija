using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Storage.FileStorage
{
    internal class GradeGuest1FileStorage : IStorage<GuestGrade>
    {
        private Serializer<GuestGrade> _serializer;
        private readonly string _file = "../../../Resources/Data/GuestRating.csv";

        public GradeGuest1FileStorage()
        {
            _serializer = new Serializer<GuestGrade>();
        }

        public List<GuestGrade> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<GuestGrade> grades)
        {
            _serializer.ToCSV(_file, grades);
        }
    }
}
