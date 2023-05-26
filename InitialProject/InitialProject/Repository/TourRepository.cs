using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    public class TourRepository
    {
        private IStorage<Tour> _storage;

        private List<Tour> _tours;
        private List<GuestOnTour> _guestsOnTours;

        public TourRepository(IStorage<Tour> storage)
        {
            _storage = storage;
            _tours = _storage.Load();
        }

        public Tour Save(Tour entity)
        {
            _tours.Add(entity);
            _storage.Save(_tours);
            return entity;
        }

        public int NextId()
        {
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
                    tour.GuideId = Convert.ToInt32(fields[11]); 
                    int i = 12;
                    int j = 13;
                    int k = 14;
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

        public List<Tour> ReadMyPastToursCsv(string Filename, int id)
        {
            List<Tour> allMyTours = GetMyTours(Filename, id);
            List<Tour> tours = new List<Tour>();
            List<GuestOnTour> guestOnTours = ReadFromGuestOnTour(FilePath1);
            for (int j = 0; j < guestOnTours.Count; j++)
            {
                for (int i = 0; i < allMyTours.Count; i++)
                {
                    if (allMyTours[i].TourStatus == "Finished" && guestOnTours[j].GuestId == id && guestOnTours[j].TourId == allMyTours[i].Id)
                    {
                        Tour tour = allMyTours[i];
                        tours.Add(tour);
                    }
                }
            }
           
         

            return tours;
        }

        public List<GuestOnTour> ReadFromGuestOnTour(string FileName)
        {
            List<GuestOnTour> guestsOnTour = new List<GuestOnTour>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    GuestOnTour guest = new GuestOnTour();


                    guest.Id = Convert.ToInt32(fields[0]);
                    guest.GuestId = Convert.ToInt32(fields[1]);
                    guest.TourId = Convert.ToInt32(fields[2]);
                    guest.TourName = fields[3];
                    guest.StartingPoint = fields[4];
                    guest.NumOfGuests = Convert.ToInt32(fields[5]);
                    guest.GuestAge = Convert.ToInt32(fields[6]);
                    guest.WithVoucher = fields[7];
                    int i = 8;
                    int j = 9;
                    int k = 10;
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
                    guest.CurentCheckPoints = checkPoints;
                    guestsOnTour.Add(guest);
                }
            }
            return guestsOnTour;
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
        public GuestOnTour FindGuestByTourIdAndGuestId(int idT, int idG)
        {
            GuestOnTour guest = new GuestOnTour();
            List<GuestOnTour> allguests = ReadFromGuestOnTour(FilePath1);

            for (int i = 0; i < allguests.Count; i++)
            {
                if (allguests[i].TourId == idT && allguests[i].GuestId == idG)
                {
                    guest = allguests[i];
                }
            }
            return guest;
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

        public List<Tour> GetMyTours(string FileName, int id)
        {
            List<Tour> allTours = ReadFromToursCsv(FileName);
            List<GuestOnTour> guestOnTours = ReadFromGuestOnTour(FilePath1);
            List<Tour> tours = new List<Tour>();

            for (int i = 0; i < allTours.Count; i++)
            {
                for (int j = 0; j < guestOnTours.Count; j++)
                {
                    if (allTours[i].Id == guestOnTours[j].TourId && guestOnTours[j].GuestId == id)
                    {
                        Tour tour = allTours[i];
                        tours.Add(tour);
                    }
                }
            }
            return tours;

        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public Tour Update(Tour entity)
        {
            Tour current = _tours.Find(c => c.Id == entity.Id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, entity);
            _storage.Save(_tours);
            return entity;
        }

        public void Delete(Tour entity)
        {
            Tour founded = _tours.Find(c => c.Id == entity.Id);
            _tours.Remove(founded);
            _storage.Save(_tours);
        }

    }

}
