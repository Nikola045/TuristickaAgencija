﻿using TravelAgency.Serializer;
using System;
using System.DirectoryServices.ActiveDirectory;

namespace TravelAgency.Model
{
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LoginRole { get; set; }

        public User() { }

        public User(string username, string password, string loginRole)
        {
            Username = username;
            Password = password;
            LoginRole = loginRole;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, LoginRole };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            LoginRole = values[3];
        }
    }
}
