using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository
{
    internal class MoveReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/moveReservationRequest.csv";
        private readonly Serializer<MoveReservation> _serializer;
        private List<MoveReservation> _reservations;

        public MoveReservationRepository() 
        {
            _serializer = new Serializer<MoveReservation>();
            _reservations = _serializer.FromCSV(FilePath);
        }
        public List<MoveReservation> GetAll()
        {
            List<MoveReservation> reservations = new List<MoveReservation>();

            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    MoveReservation reservation = new MoveReservation();
                    reservation.ReservationId = Convert.ToInt32(fields[0]);
                    reservation.GuestUsername = fields[1];
                    reservation.HotelName = fields[2];
                    reservation.OldStartDate = Convert.ToDateTime(fields[3]);
                    reservation.OldEndDate = Convert.ToDateTime(fields[4]);
                    reservation.NewStartDate = Convert.ToDateTime(fields[5]);
                    reservation.NewEndDate = Convert.ToDateTime(fields[6]);
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }

        public MoveReservation GetById(int id)
        {
            List<MoveReservation> reservations = GetAll();
            foreach(MoveReservation reservation in reservations)
            {
                if (reservation.ReservationId == id)
                    return reservation;
            }
            return null;
        }

        public void Delete(MoveReservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            MoveReservation founded = _reservations.Find(c => c.ReservationId == reservation.ReservationId);
            _reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _reservations);
        }
    }
}
