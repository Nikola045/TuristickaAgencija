using TravelAgency.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TravelAgency.Domain.Model;


namespace TravelAgency.Repository.HotelRepo
{
    public class HotelRepository
    {
        private const string FilePath = "../../../Resources/Data/hotels.csv";

        private readonly Serializer<Hotel> _serializer;

        private List<Hotel> hotels;

        public HotelRepository()
        {
            _serializer = new Serializer<Hotel>();
            hotels = _serializer.FromCSV(FilePath);
        }

        public Hotel Save(Hotel hotel)
        {
            hotel.Id = NextId();
            hotels = _serializer.FromCSV(FilePath);
            hotels.Add(hotel);
            _serializer.ToCSV(FilePath, hotels);
            return hotel;
        }

        public int NextId()
        {
            hotels = _serializer.FromCSV(FilePath);
            if (hotels.Count < 1)
            {
                return 1;
            }
            return hotels.Max(h => h.Id) + 1;
        }

        public List<Hotel> GetAll()
        {
            List<Hotel> hotels = new List<Hotel>();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] fields = line.Split('|');
                    Hotel hotel = new Hotel();
                    hotel.Id = Convert.ToInt32(fields[0]);
                    hotel.Name = fields[1];
                    hotel.City = fields[2];
                    hotel.Country = fields[3];
                    hotel.TypeOfHotel = fields[4];
                    hotel.MaxNumberOfGuests = Convert.ToInt32(fields[5]);
                    hotel.MinNumberOfDays = Convert.ToInt32(fields[6]);
                    hotel.NumberOfDaysToCancel = Convert.ToInt32(fields[7]);
                    hotels.Add(hotel);
                }
            }
            return hotels;
        }
    }
}

