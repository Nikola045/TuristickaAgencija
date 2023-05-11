using System;
using System.Collections.Generic;
using Cake.Core.IO;
using System.IO;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;
using TravelAgency.Domain.RepositoryInterfaces;

namespace TravelAgency.Repository.GradeRepo
{
    public class GradeGuest1Repository
    {
        private List<GuestGrade> grades;
        private readonly IStorage<GuestGrade> _storage;

        public GradeGuest1Repository(IStorage<GuestGrade> storage)
        {
            _storage = storage;
            grades = _storage.Load();
        }

        public GuestGrade Save(GuestGrade grade)
        {
            grades.Add(grade);
            _storage.Save(grades);
            return grade;
        }
        public List<GuestGrade> GetAll()
        {       
            return grades;
        }
    }
}
