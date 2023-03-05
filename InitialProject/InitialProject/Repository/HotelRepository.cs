using TravelAgency.Model;
using TravelAgency.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TravelAgency.Repository
{
    public class HotelRepository
    {
        private const string FilePath = "../../../Resources/Data/hotels.csv";

        private readonly Serializer<Hotel> _serializer;

        private List<Hotel> _hotels;

        public HotelRepository()
        {
            _serializer = new Serializer<Hotel>();
            _hotels = _serializer.FromCSV(FilePath);
        }

        public List<Hotel> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Hotel Save(Hotel hotel)
        {
            hotel.Id = NextId();
            _hotels = _serializer.FromCSV(FilePath);
            _hotels.Add(hotel);
            _serializer.ToCSV(FilePath, _hotels);
            return hotel;
        }

        public int NextId()
        {
            _hotels = _serializer.FromCSV(FilePath);
            if (_hotels.Count < 1)
            {
                return 1;
            }
            return _hotels.Max(h => h.Id) + 1;
        }

        public void Delete(Hotel hotel)
        {
            _hotels = _serializer.FromCSV(FilePath);
            Hotel founded = _hotels.Find(c => c.Name == hotel.Name);
            _hotels.Remove(founded);
            _serializer.ToCSV(FilePath, _hotels);
        }

        public Hotel Update(Hotel hotel)
        {
            _hotels = _serializer.FromCSV(FilePath);
            Hotel current = _hotels.Find(c => c.Name == hotel.Name);
            int index = _hotels.IndexOf(current);
            _hotels.Remove(current);
            _hotels.Insert(index, hotel);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _hotels);
            return hotel;
        }



    }
}

