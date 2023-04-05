using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    internal class OwnerGradeRepository
    {
        private const string FilePath = "../../../Resources/Data/OwnerRating.csv";

        private readonly Serializer<OwnerGrade> _serializer;

        private readonly ReservationRepository reservationRepository;

        public OwnerGradeRepository()
        {
            _serializer = new Serializer<OwnerGrade>();
            reservationRepository = new ReservationRepository();
        }

        internal OwnerGrade FindByReservationId(int id)
        {
            OwnerGrade grade = new OwnerGrade();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    grade.Guest1Username  = fields[0];
                    grade.OwnerUsername = fields[1];
                    grade.ReservationId = Convert.ToInt32(fields[2]);
                    grade.HotelRating = Convert.ToInt32(fields[3]); 
                    grade.OwnerRating = Convert.ToInt32(fields[4]); 
                    grade.Comment = fields[5];
                    if (grade.ReservationId == id) 
                    {
                        return grade;
                    }
                }
            }
            return null;
        }

        public bool IsGradeExists(int id)
        {
            List<OwnerGrade> grades = new List<OwnerGrade>();

            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    if (Convert.ToInt32(fields[2]) == id)
                    {
                        return true;
                    }

                }
            }
            return false;

        }
    }
}
