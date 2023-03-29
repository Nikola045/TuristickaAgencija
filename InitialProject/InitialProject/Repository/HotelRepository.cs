using TravelAgency.Model;
using TravelAgency.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Tokens;

namespace TravelAgency.Repository
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

        public List<Hotel> ReadFromHotelsCsv()
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

        public List<Hotel> FindHotel(string FileName, string name, string city, string country, string type, string max, string days)
        {
            var hotels = new List<Hotel>();
            using (var sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var fields = line.Split('|');
                    var hotel = new Hotel
                    {
                        Id = int.Parse(fields[0]),
                        Name = fields[1],
                        City = fields[2],
                        Country = fields[3],
                        TypeOfHotel = fields[4],
                        MaxNumberOfGuests = int.Parse(fields[5]),
                        MinNumberOfDays = int.Parse(fields[6]),
                        NumberOfDaysToCancel = int.Parse(fields[7])
                    };

                    bool requirementsMet = true;
                    if (!string.IsNullOrEmpty(name) && fields[1] != name)
                    {
                        requirementsMet = false;
                    }
                    if (!string.IsNullOrEmpty(city) && fields[2] != city)
                    {
                        requirementsMet = false;
                    }
                    if (!string.IsNullOrEmpty(country) && fields[3] != country)
                    {
                        requirementsMet = false;
                    }
                    if (!string.IsNullOrEmpty(type) && fields[4] != type)
                    {
                        requirementsMet = false;
                    }
                    if (!string.IsNullOrEmpty(max) && int.Parse(fields[5]) < int.Parse(max))
                    {
                        requirementsMet = false;
                    }
                    if (!string.IsNullOrEmpty(days) && int.Parse(fields[6]) > int.Parse(days))
                    {
                        requirementsMet = false;
                    }

                    if (requirementsMet)
                    {
                        hotels.Add(hotel);
                    }
                }
            }

            return hotels.Distinct().ToList();
        }

    }
}

