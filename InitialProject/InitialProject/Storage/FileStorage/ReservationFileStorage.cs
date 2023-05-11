﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Storage.FileStorage
{
    internal class ReservationFileStorage : IStorage<Reservation>
    {
        private Serializer<Reservation> _serializer;
        private readonly string _file = "../../../Resources/Data/reservations.csv";

        public ReservationFileStorage()
        {
            _serializer = new Serializer<Reservation>();
        }

        public List<Reservation> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Reservation> reservations)
        {
            _serializer.ToCSV(_file, reservations);
        }
    }
}
