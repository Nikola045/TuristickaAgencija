using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;

namespace TravelAgency.Services
{
    internal class TourService
    {
        private readonly TourRequestsRepository tourRequestsRepository;
        public TourService() 
        {
            tourRequestsRepository = new(InjectorService.CreateInstance<IStorage<TourRequests>>());
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
    }
}
