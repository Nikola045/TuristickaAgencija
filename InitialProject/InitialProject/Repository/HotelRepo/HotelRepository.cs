using TravelAgency.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TravelAgency.Domain.Model;
using TravelAgency.Services;
using TravelAgency.Domain.RepositoryInterfaces;

namespace TravelAgency.Repository.HotelRepo
{
    public class HotelRepository
    {
        private readonly List<Hotel> hotels;
        private readonly IStorage<Hotel> _storage;

        public HotelRepository(IStorage<Hotel> storage)
        {
            _storage = storage;
            hotels = _storage.Load();
        }

        public void Delete(Hotel entity)
        {
            Hotel founded = hotels.Find(c => c.Id == entity.Id);
            hotels.Remove(founded);
            _storage.Save(hotels);
        }

        public List<Hotel> GetAll()
        {
            return hotels;
        }

        public int NextId()
        {
            if (hotels.Count < 1)
            {
                return 1;
            }
            return hotels.Max(h => h.Id) + 1;
        }

        public Hotel Save(Hotel entity)
        {
            hotels.Add(entity);
            _storage.Save(hotels);
            return entity;
        }

        public Hotel Update(Hotel entity)
        {
            Hotel current = hotels.Find(c => c.Id == entity.Id);
            int index = hotels.IndexOf(current);
            hotels.Remove(current);
            hotels.Insert(index, entity);
            _storage.Save(hotels);
            return entity;
        }
    }
}

