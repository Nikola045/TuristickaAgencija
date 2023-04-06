using System.Collections.Generic;
using TravelAgency.Domain.Model;
using TravelAgency.Repository.GradeRepo;

namespace TravelAgency.Services
{
    internal class OwnerService
    {
        private readonly OwnerGradeRepository ownerGradeRepository;
        public OwnerService()
        {
            ownerGradeRepository = new OwnerGradeRepository();
        }

        public int CountGradesFromOwnerRating(string OwnerUserName)
        {
            int count = 0;
            List<OwnerGrade> grades = ownerGradeRepository.GetAll();
            foreach(OwnerGrade grade in grades)
            {
                if (grade.OwnerUsername == OwnerUserName)
                    count++;
            }
            return count;
        }
        public int GetAverageOwnerRating(string OwnerUserName)
        {
            int Grade = 0;
            List<OwnerGrade> grades = ownerGradeRepository.GetAll();
            foreach (OwnerGrade grade in grades)
            {
                if (grade.OwnerUsername == OwnerUserName)
                    Grade = Grade + grade.OwnerRating;
            }
            return Grade / CountGradesFromOwnerRating(OwnerUserName);
        }

        public string SuperOwner(string username)
        {
            if (CountGradesFromOwnerRating(username) >= 50)
            {
                if (GetAverageOwnerRating(username) < 9.5)
                {
                    return "Owner";
                }
                else
                {
                    return "Super-Owner";
                }
            }
            else
            {
                return "Owner";
            }

        }
    }
}
