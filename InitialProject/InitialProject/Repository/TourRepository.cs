using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    internal class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(t => t.Id) + 1;
        }




        public List<Tour> ReadFromToursCsv(string FileName)
        {
            List<Tour> tours = new List<Tour>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    Tour tour = new Tour();


                    tour.Id = Convert.ToInt32(fields[0]);
                    tour.Name = fields[1];
                    tour.City = fields[2];
                    tour.Country = fields[3];
                    tour.Description = fields[4];
                    tour.Lenguage = fields[5];
                    tour.MaxNumberOfGuests = Convert.ToInt32(fields[6]);
                    tour.MaxNumberOfGuests = Convert.ToInt32(fields[7]);
                    tour.StartTime = Convert.ToDateTime(fields[8]);
                    tour.TourDuration = Convert.ToInt32(fields[9]);
                    int i = 10;
                    int j = 11;
                    List<CheckPoint> checkPoints = new List<CheckPoint>();
                    while (j <= fields.Count())
                    {
                        CheckPoint checkPoint = new CheckPoint();
                        checkPoint.Id = Convert.ToInt32(fields[i]);
                        checkPoint.Name = fields[j];
                        checkPoints.Add(checkPoint);
                        i = i + 2;
                        j = j + 2;
                    }
                    tour.CheckPoints = checkPoints;
                    tours.Add(tour);
                }
            }
            return tours;
        }

        public List<Tour> FindTour(string FileName, string city, string country, string leng, string duration, string num)
        {
            List<Tour> allTours = ReadFromToursCsv(FileName);
            List<Tour> tours = new List<Tour>();

            for (int i = 0; i < allTours.Count; i++)
            {
                /*
                if (city == "" || country == "" || leng == "" || duration == "" || num == "")
                {

                }
                */

                if (allTours[i].City == city || city == "")
                {
                    if (allTours[i].Country == country || country == "")
                    {
                        if (allTours[i].Lenguage == leng || leng == "")
                        {
                            if (Convert.ToString(allTours[i].TourDuration) == duration || duration == "")
                            {
                                if (allTours[i].CurentNumberOfGuests + Convert.ToInt32(num) <= allTours[i].MaxNumberOfGuests || num == "")//////uslov
                                {
                                    Tour tour = allTours[i];
                                    tours.Add(tour);
                                }
                            }
                        }
                    }
                }
                //allTours.Add(tour);
            }
            return tours;
        }

    }

}
