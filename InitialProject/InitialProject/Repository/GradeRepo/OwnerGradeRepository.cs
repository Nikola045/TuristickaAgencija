using System;
using System.Collections.Generic;
using System.IO;
using TravelAgency.Domain.Model;

namespace TravelAgency.Repository.GradeRepo
{
    internal class OwnerGradeRepository
    {
        private const string FilePath = "../../../Resources/Data/OwnerRating.csv";
        private List<OwnerGrade> ownerGrades;

        public OwnerGradeRepository()
        {
            ownerGrades = GetAll();
        }

        public List<OwnerGrade> GetAll()
        {
            List<OwnerGrade> grades = new List<OwnerGrade>();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    OwnerGrade grade = new OwnerGrade();
                    grade.Guest1Username = fields[0];
                    grade.OwnerUsername = fields[1];
                    grade.ReservationId = Convert.ToInt32(fields[2]);
                    grade.HotelRating = Convert.ToInt32(fields[3]);
                    grade.OwnerRating = Convert.ToInt32(fields[4]);
                    grade.Comment = fields[5];
                    grades.Add(grade);
                }
            }
            return grades;
        }

        public OwnerGrade GetByReservationId(int id)
        {
            foreach (OwnerGrade grade in ownerGrades)
            {
                if (grade.ReservationId == id)
                {
                    return grade;
                }
            }
            return null;
        }
    }

}
