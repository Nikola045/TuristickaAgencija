﻿using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    tour.StartTime = Convert.ToDateTime(fields[7]);
                    tour.TourDuration = Convert.ToInt32(fields[8]);
                    int i = 9;
                    int j = 10;
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

        public List<Tour> FindTour(string FileName, string city, string country, string leng, string duration, string num) //ne radi kako treba
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
                    tour.StartTime = Convert.ToDateTime(fields[7]);
                    tour.TourDuration = Convert.ToInt32(fields[8]);
                    
                    int i = 9;
                    int j = 10;
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

                    if (city != "")
                    {
                        if (fields[1] == city)
                        {
                            tours.Add(tour);
                        }
                    }
                    if (country != "")
                    {
                        if (fields[3] == country)
                        {
                            tours.Add(tour);
                        }
                    }
                    if (duration != "")
                    {
                        if (fields[4] == duration)
                        {
                            tours.Add(tour);
                        }
                    }
                    if (leng != "")
                    {
                        if (fields[5] == leng)
                        {
                            tours.Add(tour);
                        }
                    }
                    if (num != "")
                    {
                        if (fields[7] == num)
                        {
                            tours.Add(tour);
                        }
                    }
                }
            }
            tours = tours.Distinct().ToList();
            return tours;
        }


    }


}
