using Microsoft.Graph.Drives.Item.List.Items.Item.Fields;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;
using TravelAgency.Serializer;
using TravelAgency.View;

namespace TravelAgency.Repository
{
    internal class ReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservations.csv";

        private readonly Serializer<Reservation> _serializer;

        private List<Reservation> _reservations;

        public ReservationRepository()
        {
            _serializer = new Serializer<Reservation>();
            _reservations = _serializer.FromCSV(FilePath);
        }

        public Reservation Save(Reservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return reservation;
        }

        public int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(r => r.Id) + 1;
        }

        public List<Reservation> ReadFromReservationsCsv(string FileName)
        {
            List<Reservation> reservations = new List<Reservation>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    Reservation reservation = new Reservation();
                    reservation.Id = Convert.ToInt32(fields[0]);
                    reservation.GuestUserName = fields[1];
                    reservation.HotelName = fields[2];
                    reservation.StartDate = Convert.ToDateTime(fields[3]);
                    reservation.EndDate = Convert.ToDateTime(fields[4]);
                    reservation.NumberOfDays = Convert.ToInt32(fields[5]);
                    reservation.NumberOfGuests = Convert.ToInt32(fields[6]);
                    reservations.Add(reservation);

                }
            }
            return reservations;
        }

    }
}
