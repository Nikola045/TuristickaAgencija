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
    public class GuestOnTourService
    {
        private readonly GuestOnTourRepository guestOnTourRepository;
        public GuestOnTourService()
        {
            guestOnTourRepository = new(InjectorService.CreateInstance<IStorage<GuestOnTour>>());
        }

        public int[] ShowStatistic(int id)
        {
            int[] statistic = new int[4];

            List<GuestOnTour> guestOnTour = guestOnTourRepository.GetAll();

            for (int i = 0; i < guestOnTour.Count(); i++)
            {
                if (guestOnTour[i].Tour.Id == id)
                {
                    if (guestOnTour[i].GuestAge < 18)
                    {
                        statistic[0] += guestOnTour[i].NumOfGuests;
                    }
                    else if (guestOnTour[i].GuestAge < 50)
                    {
                        statistic[1] += guestOnTour[i].NumOfGuests;
                    }
                    else
                    {
                        statistic[2] += guestOnTour[i].NumOfGuests;
                    }

                    if (guestOnTour[i].WithVoucher == "Ima")
                    {
                        statistic[3] += guestOnTour[i].NumOfGuests;
                    }
                }
            }
            return statistic;
        }
    }
}
