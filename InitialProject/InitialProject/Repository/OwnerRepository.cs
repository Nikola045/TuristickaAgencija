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
    
    public class OwnerRepository
    {
        private const string FilePath = "../../../Resources/Data/OwnerRating.csv";

        private readonly Serializer<Owner> _serializer;

        public OwnerRepository()
        {
            _serializer = new Serializer<Owner>();
        }

        public int CountGradesFromOwnerRating(string OwnerUserName)
        {
            int count = 0;
            string Owner = "";
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    Owner = fields[1];
                    if (OwnerUserName == Owner)
                        count++;
                }
            }
            return count;
        }
        public int GetAverageOwnerRating(string OwnerUserName)
        {
            int count = 0;
            string Owner = "";
            int Grade = 0;
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    Owner = fields[1];
                    Grade = Grade + Convert.ToInt32(fields[4]);
                    if (OwnerUserName == Owner)
                        count++;
                }
            }
            return Grade/count;
        }

        public string SuperOwner(string username)
        {
            if(CountGradesFromOwnerRating(username) >= 50)
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
