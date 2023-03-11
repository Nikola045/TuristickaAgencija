﻿using TravelAgency.Model;
using TravelAgency.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

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

        public List<Hotel> ReadFromHotelsCsv(string FileName)
        {
            List<Hotel> hotels = new List<Hotel>();

            using (StreamReader sr = new StreamReader(FileName))
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

        public List<Hotel> FindHotelByName(string FileName,string name,string city)
        {
            List<Hotel> hotels = new List<Hotel>();

            using (StreamReader sr = new StreamReader(FileName))
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

                    if (fields[1].Equals(name) || fields[2].Equals(city))
                    {
                        hotels.Add(hotel);
                    }
                }
            }
            return hotels;
        }
        
    }
}

