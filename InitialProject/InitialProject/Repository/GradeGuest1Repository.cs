using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    class GradeGuest1Repository
    {
        private const string FilePath = "../../../Resources/Data/guestRating.csv";

        private readonly Serializer<GuestGrade> _serializer;

        private List<GuestGrade> _grades;

        public GradeGuest1Repository()
        {
            _serializer = new Serializer<GuestGrade>();
            _grades = _serializer.FromCSV(FilePath);
        }

        public GuestGrade Save(GuestGrade grade)
        {
            _grades = _serializer.FromCSV(FilePath);
            _grades.Add(grade);
            _serializer.ToCSV(FilePath, _grades);
            return grade;
        }
    }
}
