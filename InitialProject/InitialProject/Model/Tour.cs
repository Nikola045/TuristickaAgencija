using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    public Tour(int id, string name, string city, string country, string description, string lenguage, int maxNumberOfGuests, DateTime startTime, int tourDuration, List<CheckPoint> checkPoints)
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
        string CheckPointsList = null;
        foreach (CheckPoint point in CheckPoints) //pravi problem posle prvog unosa
        {
            CheckPointsList = CheckPointsList + point.Id.ToString() + "|" + point.Name + "|"; // nekako na kraju da se ne unese poslednja "|"
        }
        string[] csvValues = { Id.ToString(), Name, City, Country, Description, Lenguage, MaxNumberOfGuests.ToString(), StartTime.ToString(), TourDuration.ToString(), CheckPointsList };
        return csvValues;
    }

    public CheckPoint ConvertToCheckPoint(int id, string name)
    {
        CheckPoint checkPoint = new CheckPoint();
        checkPoint.Id = id;
        checkPoint.Name = name;
        return checkPoint;
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
        //fali da se uvede petlja ali foreach ne radi
        CheckPoints.Add(ConvertToCheckPoint(Convert.ToInt32(values[9]), values[10]));
    }
}
