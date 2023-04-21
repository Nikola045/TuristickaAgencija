using System;
using System.Collections.Generic;
using System.IO;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Repository.GradeRepo
{
    public class OwnerGradeRepository
    {
        private List<OwnerGrade> ownerGrades;
        private IStorage<OwnerGrade> _storage;

        public OwnerGradeRepository(IStorage<OwnerGrade> storage)
        {
            _storage = storage;
            ownerGrades = _storage.Load();

        }

        public List<OwnerGrade> GetAll()
        {

            return ownerGrades;
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

        public OwnerGrade Save(OwnerGrade grade)
        {
            ownerGrades.Add(grade);
            _storage.Save(ownerGrades);
            return grade;
        }
    }

}
