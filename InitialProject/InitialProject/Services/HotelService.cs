using System.Collections.Generic;
using System.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Repository.HotelRepo;

namespace TravelAgency.Services
{
    internal class HotelService
    {
        private readonly HotelImageRepository hotelImageRepository;
        private readonly HotelRepository hotelRepository;

        public HotelService() 
        {
            hotelImageRepository = new HotelImageRepository();
            hotelRepository = new HotelRepository();
        }

        public List<Hotel> FindHotel(string name, string city, string country, string type, string max, string days)
        {
            List<Hotel> hotels = hotelRepository.GetAll();
            List<Hotel> findedHotels = new List<Hotel>();

            foreach(Hotel hotel in hotels)
            {
                bool requirementsMet = true;
                if (!string.IsNullOrEmpty(name) && hotel.Name != name)
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(city) && hotel.City != city)
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(country) && hotel.Country != country)
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(type) && hotel.TypeOfHotel != type)
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(max) && hotel.MaxNumberOfGuests < int.Parse(max))
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(days) && hotel.MinNumberOfDays > int.Parse(days))
                {
                    requirementsMet = false;
                }

                if (requirementsMet)
                {
                    findedHotels.Add(hotel);
                }
            }
                    
            return findedHotels.Distinct().ToList();
        }

        public Hotel GetHotelByName(string name)
        {
            List<Hotel> hotelList = hotelRepository.GetAll();
            foreach(Hotel hotel in hotelList)
            {
                if(hotel.Name == name)
                    return hotel;
            }
            return null;
        }

        public HotelImage FindByUrl(string url)
        {
            List<HotelImage> hotelImages = hotelImageRepository.GetAll();
            foreach (HotelImage hotelImage in hotelImages)
            {
                if (hotelImage.Url == url)
                {
                    return hotelImage;
                }
            }
            return null;
        }

        public List<HotelImage> FindAllById(int id)
        {
            List<HotelImage> hotelImages = hotelImageRepository.GetAll();
            foreach (HotelImage hotelImage in hotelImages)
            {
                if (hotelImage.HotelId == id)
                {
                    hotelImages.Add(hotelImage);
                }
            }
            return hotelImages;
        }
    }
}
