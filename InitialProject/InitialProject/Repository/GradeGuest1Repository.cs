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

        private List<GuestGrade> grades;

        public GradeGuest1Repository()
        {
            _serializer = new Serializer<GuestGrade>();
            grades = _serializer.FromCSV(FilePath);
        }

        public GuestGrade Save(GuestGrade grade)
        {
            grades = _serializer.FromCSV(FilePath);
            grades.Add(grade);
            _serializer.ToCSV(FilePath, grades);
            return grade;
        }
    }
}
