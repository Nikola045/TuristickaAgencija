using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.HotelRepo;

namespace TravelAgency.Services
{
    internal class TourService
    {
        private readonly TourRequestsRepository tourRequestsRepository;
        private readonly TourRepository tourRepository;
        private readonly GuestOnTourRepository guestOnTourRepository;
        public TourService() 
        {
            tourRequestsRepository = new(InjectorService.CreateInstance<IStorage<TourRequests>>());
            tourRepository = new(InjectorService.CreateInstance<IStorage<Tour>>());
            guestOnTourRepository = new(InjectorService.CreateInstance<IStorage<GuestOnTour>>());
        }

        public List<Tour> GetMyTours(int id)
        {
            List<Tour> allTours = tourRepository.GetAll();
            List<GuestOnTour> guestOnTours = guestOnTourRepository.GetAll();
            List<Tour> tours = new List<Tour>();

            for (int i = 0; i < allTours.Count; i++)
            {
                for (int j = 0; j < guestOnTours.Count; j++)
                {
                    if (allTours[i].Id == guestOnTours[j].Tour.Id && guestOnTours[j].Guest2.Id == id)
                    {
                        Tour tour = allTours[i];
                        tours.Add(tour);
                    }
                }
            }
            return tours;
        }

        public List<Tour> ReadMyPastToursCsv(int id)
        {
            List<Tour> allMyTours = GetMyTours(id);
            List<Tour> tours = new List<Tour>();
            List<GuestOnTour> guestOnTours = guestOnTourRepository.GetAll();
            for (int j = 0; j < guestOnTours.Count; j++)
            {
                for (int i = 0; i < allMyTours.Count; i++)
                {
                    if (allMyTours[i].TourStatus == "Finished" && guestOnTours[j].Guest2.Id == id && guestOnTours[j].Tour.Id == allMyTours[i].Id)
                    {
                        Tour tour = allMyTours[i];
                        tours.Add(tour);
                    }
                }
            }
            return tours;
        }

        public List<TourRequests> MyRequests(int id)
        {
            List<TourRequests> tourRequests = tourRequestsRepository.GetAll();
            List<TourRequests> tourRequests1 = new List<TourRequests>();

            for (int i = 0; i < tourRequests.Count(); i++)
            {
                if (tourRequests[i].Guest2.Id == id && tourRequests[i].Status == "Pending")
                {
                    tourRequests1.Add(tourRequests[i]);
                }
            }

            return tourRequests;
        }

        public Tour FindById(int id)
        {
            Tour tour = new Tour();
            List<Tour> allTours = tourRepository.GetAll();

            for (int i = 0; i < allTours.Count; i++)
            {
                if (allTours[i].Id == id)
                {
                    tour = allTours[i];
                }
            }
            return tour;
        }

        public bool ReserveTour(int tourId, User user, int num, bool hasVoucher)
        {
            GuestOnTour guestOnTour = new GuestOnTour();
            Tour tour = FindById(tourId);
            guestOnTour.Id = guestOnTourRepository.NextId();
            guestOnTour.Guest2.Id = user.Id;
            guestOnTour.Tour.Id = tour.Id;
            guestOnTour.TourName = tour.Name;
            guestOnTour.NumOfGuests = num;
            guestOnTour.CurentCheckPoints = tour.CheckPoints;
            guestOnTour.StartingPoint = "NijePrisutan";
            guestOnTour.GuestAge = user.Age;
            if (hasVoucher)
            {
                guestOnTour.WithVoucher = "Ima";
            }
            else
            {
                guestOnTour.WithVoucher = "Nema";
            }
            guestOnTourRepository.Save(guestOnTour);

            return true;
        }

        public List<Tour> FilterTours(string city, string country, string leng, string duration, string num)
        {
            List<Tour> allTours = tourRepository.GetAll();

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
            }
            return tours;
        }

        public List<Tour> GetTodaysTours()
        {
            List<Tour> allTours = tourRepository.GetAll();
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

        public bool IsStarted()
        {
            int countStartedTours = 0;
            List<Tour> tours = tourRepository.GetAll();
            foreach (Tour tour in tours)
            {
                if (tour.TourStatus == "Zapoceta")
                    countStartedTours++;
            }
            if (countStartedTours != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Tour FindMostAttendedTour()
        {
            Tour tour = new Tour();
            List<Tour> allTours = tourRepository.GetAll();
            int maxNumOfGuest = 0;

            for (int i = 0; i < allTours.Count; i++)
            {
                if (allTours[i].CurentNumberOfGuests > maxNumOfGuest)
                {
                    maxNumOfGuest = allTours[i].CurentNumberOfGuests;
                    tour = allTours[i];
                }
            }
            return tour;
        }

        public Tour FindMostAttendedTourThisYear(string year)
        {
            Tour tour = new Tour();
            int year1 = Convert.ToInt32(year);
            List<Tour> allTours = tourRepository.GetAll();
            int maxNumOfGuest = 0;

            for (int i = 0; i < allTours.Count; i++)
            {
                if (allTours[i].CurentNumberOfGuests > maxNumOfGuest && allTours[i].StartTime.Year == year1)
                {
                    maxNumOfGuest = allTours[i].CurentNumberOfGuests;
                    tour = allTours[i];
                }
            }
            return tour;
        }
        public List<TourRequests> FindTourRequest(string dateStart,string dateEnd,string city,string country,string maxNumOfGuest,string language)
        {
            List<TourRequests> tourRequests = tourRequestsRepository.GetAll();
            List<TourRequests> findedRequests = new List<TourRequests>();
            foreach (TourRequests tour in tourRequests)
            {
                bool requirementsMet = true;
                if ((tour.FirstTime < Convert.ToDateTime(dateStart) || tour.SecondTime > Convert.ToDateTime(dateEnd)) && (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd)))
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(city) && tour.City != city)
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(country) && tour.Country != country)
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(maxNumOfGuest) && tour.MaxNumberOfGuests < int.Parse(maxNumOfGuest))
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(language) && tour.Language !=language)
                {
                    requirementsMet = false;
                }
                if (requirementsMet)
                {
                    findedRequests.Add(tour);
                }
            }
            return findedRequests;
        }


    }
}
