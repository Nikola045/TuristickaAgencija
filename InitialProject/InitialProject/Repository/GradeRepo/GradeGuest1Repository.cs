using System.Collections.Generic;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository.GradeRepo
{
    class GradeGuest1Repository
    {
        private const string FilePathGuestRatingde = "../../../Resources/Data/guestRating.csv";
        private readonly Serializer<GuestGrade> _serializer;
        private List<GuestGrade> grades;

        public GradeGuest1Repository()
        {
            _serializer = new Serializer<GuestGrade>();
            grades = _serializer.FromCSV(FilePathGuestRatingde);
        }

        public GuestGrade Save(GuestGrade grade)
        {
            grades = _serializer.FromCSV(FilePathGuestRatingde);
            grades.Add(grade);
            _serializer.ToCSV(FilePathGuestRatingde, grades);
            return grade;
        }
    }
}
