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
    internal class OwnerGradeFileStorage : IStorage<OwnerGrade>
    {
        private Serializer<OwnerGrade> _serializer;
        private readonly string _file = "../../../Resources/Data/OwnerRating.csv";

        public OwnerGradeFileStorage()
        {
            _serializer = new Serializer<OwnerGrade>();
        }

        public List<OwnerGrade> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<OwnerGrade> grades)
        {
            _serializer.ToCSV(_file, grades);
        }
    }
}
