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
        private readonly ComplexTourRequestRepositoty complexTourRequestsRepository;
        private readonly VoucherRepository voucherRepository;

        private readonly TourRepository tourRepository;
        private readonly GuestOnTourRepository guestOnTourRepository;
        private readonly GuideReviewRepository guideReviewRepository;
        public TourService()
        {
            tourRequestsRepository = new(InjectorService.CreateInstance<IStorage<TourRequests>>());
            voucherRepository = new(InjectorService.CreateInstance<IStorage<Voucher>>());
            tourRepository = new(InjectorService.CreateInstance<IStorage<Tour>>());
            guestOnTourRepository = new(InjectorService.CreateInstance<IStorage<GuestOnTour>>());
            guideReviewRepository = new(InjectorService.CreateInstance<IStorage<TourReview1>>());
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
        /*
        public List<ComplexTourRequest> MyComplexRequests(int id)
        {
            List<ComplexTourRequest> tourRequests = complexTourRequestsRepository.GetAll();
            List<ComplexTourRequest> tourRequests1 = new List<ComplexTourRequest>();

            for (int i = 0; i < tourRequests.Count(); i++)
            {
                TourRequests request = 
                if (tourRequests[i].Guest2.Id == id && tourRequests[i].Status == "Pending")
                {
                    tourRequests1.Add(tourRequests[i]);
                }
            }

            return tourRequests1;
        }
        */
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
        public int StatisticByLocation(string city, string country)
        {
            List<TourRequests> tourRequests = tourRequestsRepository.GetAll();
            int numOfLocation = 0;
            foreach (TourRequests tour in tourRequests)
            {
                if (tour.City == city && tour.Country == country)
                {
                    numOfLocation++;
                }
            }
            return numOfLocation;
        }
        public int StatisticByLanguage(string language)
        {
            List<TourRequests> tourRequests = tourRequestsRepository.GetAll();
            int numOfLanguage = 0;
            foreach (TourRequests tour in tourRequests)
            {
                if (tour.Language == language)
                {
                    numOfLanguage++;
                }
            }
            return numOfLanguage;

        }
        public string FindMostWantedLanguage()
        {
            DateTime lastYear = DateTime.Now.AddYears(-1);
            List<TourRequests> tourRequests = tourRequestsRepository.GetAll().Where(t => t.FirstTime >= lastYear).ToList();
            string bestLanguage = tourRequests.GroupBy(l => l.Language).OrderByDescending(g => g.Count())
                                  .Select(g => g.Key).FirstOrDefault();

            return bestLanguage;


        }
        public string FindMostWantedCity()
        {
            DateTime lastYear = DateTime.Now.AddYears(-1);
            List<TourRequests> tourRequests = tourRequestsRepository.GetAll().Where(t => t.FirstTime >= lastYear).ToList();
            string bestCity = tourRequests.GroupBy(l => l.City).OrderByDescending(g => g.Count())
                                  .Select(g => g.Key).FirstOrDefault();

            return bestCity;


        }
        public string FindMostWantedCountry()
        {
            DateTime lastYear = DateTime.Now.AddYears(-1);
            List<TourRequests> tourRequests = tourRequestsRepository.GetAll().Where(t => t.FirstTime >= lastYear).ToList();
            string bestCountry = tourRequests.GroupBy(l => l.Country).OrderByDescending(g => g.Count())
                                  .Select(g => g.Key).FirstOrDefault();

            return bestCountry;


        }

        public int GetNumberOfToursLanguage(string language, User user)
        {
            List<Tour> toures = tourRepository.GetAll();
            int counter = 0;
            foreach (Tour tour in toures)
            {
                if(tour.Lenguage == language && user.Id == tour.GuideId)
                {
                    counter++;
                }
            }
            return counter;
        }

        public int GetRatingLanguageTure(string language, User user)
        {
            List<TourReview1> reviews = guideReviewRepository.GetAll();
            int cumRating = 0;
            foreach(TourReview1 review in reviews)
            {
                Tour tour = FindById(review.Tour.Id);
                if(user.Id == tour.GuideId && tour.Lenguage == language)
                {
                    cumRating = cumRating + review.GuidesLenguage;
                }
            }
            return cumRating;
        }

        public double GetAllRatings(string language, User user)
        {
            int numberOfTours = GetNumberOfToursLanguage(language, user);
            int cumRating = GetRatingLanguageTure(language, user);
            double averageGrade = cumRating / numberOfTours;
            if (numberOfTours > 1)
            {
                return averageGrade;
            }
            return 0;
        }

        public List<string> GetAllLanguages(User user)
        {
            List<Tour> tours = tourRepository.GetAll();
            List<string> languages = new List<string>();
            foreach(Tour tour in tours)
            {
                if(tour.GuideId == user.Id)
                {
                    languages.Add(tour.Lenguage);
                }
                
            }
            return languages.Distinct().ToList();
        }

        public void Refresh(int id)
        {
            List<GuestOnTour> guests = guestOnTourRepository.GetAll();
            int c = 0;
            User user = new User();
            foreach (GuestOnTour guest in guests)
            {
               
                if(guest.Guest2.Id == id)
                {
                    c = c + 1;
                    user = guest.Guest2;
                }
               
            }
            if (c > 0.5)
            {
                Voucher voucher = new Voucher(voucherRepository.NextId(), "5 tura", DateTime.Now.AddYears(1), user);
                voucherRepository.Save(voucher);
            }
            
        }

    }
}
