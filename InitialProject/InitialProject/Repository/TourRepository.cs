using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    internal class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private const string FilePath1 = "../../../Resources/Data/guestOnTour.csv";

        private readonly Serializer<Tour> _serializer;
        private readonly Serializer<GuestOnTour> _serializerG;

        private List<Tour> _tours;
        private List<GuestOnTour> _guestsOnTours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
            _serializerG = new Serializer<GuestOnTour>();
            _guestsOnTours = _serializerG.FromCSV(FilePath1);
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

        public int NextIdG()
        {
            _guestsOnTours = _serializerG.FromCSV(FilePath1);
            if (_guestsOnTours.Count < 1)
            {
                return 1;
            }
            return _guestsOnTours.Max(t => t.Id) + 1;
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
                    tour.CurentNumberOfGuests = Convert.ToInt32(fields[7]);
                    tour.StartTime = Convert.ToDateTime(fields[8]);
                    tour.TourDuration = Convert.ToInt32(fields[9]);
                    tour.TourStatus = fields[10];
                    int i = 11;
                    int j = 12;
                    int k = 13;
                    List<CheckPoint> checkPoints = new List<CheckPoint>();
                    while (k <= fields.Count())
                    {
                        CheckPoint checkPoint = new CheckPoint();
                        checkPoint.Id = Convert.ToInt32(fields[i]);
                        checkPoint.Name = fields[j];
                        checkPoint.Status = fields[k];
                        checkPoints.Add(checkPoint);
                        i = i + 3;
                        j = j + 3;
                        k = k + 3;
                    }
                    tour.CheckPoints = checkPoints;
                    tours.Add(tour);
                }
            }
            return tours;
        }

        public List<Tour> FilterTours(string FileName, string city, string country, string leng, string duration, string num)
        {
            List<Tour> allTours = ReadFromToursCsv(FileName);

            List<Tour> tours = new List<Tour>();

            for (int i = 0; i < allTours.Count; i++)
            {
                if (allTours[i].City == city || city == "")
                {
                    if (allTours[i].Country == country || country == "")
                    {
                        if (allTours[i].Lenguage == leng || leng == "")
                        {
                            if (Convert.ToString(allTours[i].TourDuration) == duration || duration == "")
                            {
                                if (num == "")
                                { 
                                    if (allTours[i].CurentNumberOfGuests <= allTours[i].MaxNumberOfGuests)
                                    {
                                        Tour tour = allTours[i];
                                        tours.Add(tour);
                                    }
                                }
                                else
                                {
                                    if (allTours[i].CurentNumberOfGuests + Convert.ToInt32(num) <= allTours[i].MaxNumberOfGuests)
                                    {
                                        Tour tour = allTours[i];
                                        tours.Add(tour);
                                    }
                                }
                            }
                        }
                    }
                }
                //allTours.Add(tour);
            }
            return tours;
        }

        public Tour FindById(int id)
        {
            Tour tour = new Tour();
            List<Tour> allTours = ReadFromToursCsv(FilePath);

            for (int i = 0; i<allTours.Count; i++)
            {
                if (allTours[i].Id == id)
                {
                    tour = allTours[i];
                }
            }
            return tour;
        }



        public List<Tour> GetTodaysTours(string FileName)
        {
            List<Tour> allTours = ReadFromToursCsv(FileName);
            List<Tour> tours = new List<Tour>();
            DateTime dateTime = DateTime.Today;
            for (int i = 0; i < allTours.Count; i++)
            {
                if (allTours[i].StartTime == dateTime)
                {
                    Tour tour = allTours[i];
                    tours.Add(tour);
                }
            }
            return tours;
        }


        public bool ReserveTour(int tourId, int  guestId, string fileName, int num)
        {
            GuestOnTour guestOnTour = new GuestOnTour();
            Tour tour = FindById(tourId);
            guestOnTour.Id = NextIdG();
            guestOnTour.GuestId = guestId;
            guestOnTour.TourId = tour.Id;
            guestOnTour.TourName = tour.Name;
            guestOnTour.NumOfGuests = num;
            guestOnTour.CurentCheckPoints = tour.CheckPoints;
            guestOnTour.StartingPoint = "NijePrisutan";

            ///////// upisi u guestOnTour.csv
            _guestsOnTours = _serializerG.FromCSV(fileName);
            _guestsOnTours.Add(guestOnTour);
            _serializerG.ToCSV(fileName, _guestsOnTours);

            return true;
        }



        public Tour Update(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour current = _tours.Find(c => c.Id == tour.Id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, tour); 
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public List<CheckPoint> FindAllCheckPoints()
        {
            List<CheckPoint> checkPoints= new List<CheckPoint>();
            using (StreamReader sr = new StreamReader(FilePath))
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
                    tour.CurentNumberOfGuests = Convert.ToInt32(fields[7]);
                    tour.StartTime = Convert.ToDateTime(fields[8]);
                    tour.TourDuration = Convert.ToInt32(fields[9]);
                    tour.TourStatus = fields[10];
                    int i = 11;
                    int j = 12;
                    int k = 13;


                    while (k <= fields.Count())
                    {
                        CheckPoint checkPoint = new CheckPoint();
                        checkPoint.Id = Convert.ToInt32(fields[i]);
                        checkPoint.Name = fields[j];
                        checkPoint.Status = fields[k];
                        checkPoints.Add(checkPoint);
                        i = i + 3;
                        j = j + 3;
                        k = k + 3;
                    }

                    tour.CheckPoints = checkPoints;
                    return checkPoints;
                }
            }
            return null;

        }

        public bool IsStarted()
        {
            int countStartedTours = 0;
            List<Tour> tours = ReadFromToursCsv(FilePath);
            foreach(Tour tour in tours)
            {
                if(tour.TourStatus == "Zapoceta")
                    countStartedTours++;
            }
            if(countStartedTours != 0) {
                return true;
            }
            else
            {
                return false;
            }
            
        }



    }

}
