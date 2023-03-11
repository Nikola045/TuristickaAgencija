using TravelAgency.Serializer;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TravelAgency.Model
{
    public class Owner : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Owner() { }

        public Owner(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
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
                    hotel.MaxNumberOfGusets = Convert.ToInt32(fields[5]);
                    hotel.MinNumberOfGusets = Convert.ToInt32(fields[6]);
                    hotel.NumberOfDaysToCancel = Convert.ToInt32(fields[7]);

                    hotels.Add(hotel);


                }
            }
            return hotels;
        }
    }
}
