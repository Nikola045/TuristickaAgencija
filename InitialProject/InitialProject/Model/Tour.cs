using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Model;

internal class Tour : TravelAgency.Serializer.ISerializable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Description { get; set; }
    public string Lenguage { get; set; }
    public int MaxNumberOfGuests { get; set; }
    public List<CheckPoint> CheckPoints { get; set; }
    public DateTime StartTime { get; set; }
    public int TourDuration { get; set; }

    public Tour() { }
    public Tour(int id, string name, string city, string country, string description, string lenguage, int maxNumberOfGuests, List<CheckPoint> checkPoints, DateTime startTime, int tourDuration)
    {
        Id = id;
        Name = name;
        City = city;
        Country = country;
        Description = description;
        Lenguage = lenguage;
        MaxNumberOfGuests = maxNumberOfGuests;
        StartTime = startTime;
        TourDuration = tourDuration;
        CheckPoints = checkPoints;
    }

    public string[] ToCSV()
    {
        string[] csvValues = { Id.ToString(), Name, City, Country, Description, Lenguage, MaxNumberOfGuests.ToString(), CheckPoints.ToString(), StartTime.ToString(), TourDuration.ToString() };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = Convert.ToInt32(values[0]);
        Name = values[1];
        City = values[2];
        Country = values[3];
        Description = values[4];
        Lenguage = values[5];
        MaxNumberOfGuests = Convert.ToInt32(values[6]);
        StartTime = Convert.ToDateTime(values[7]);
        TourDuration = Convert.ToInt32(values[8]);
        for (int i = 0; i < CheckPoints.Count; i++)
        {
            CheckPoints[i].Id = Convert.ToInt32(values[i+9]);
            CheckPoints[i].Name = values[i+10];
        }
    }
}
