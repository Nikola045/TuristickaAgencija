using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal class GuestGrade : TravelAgency.Serializer.ISerializable
    {
        public int GuestId { get; set; }
        public int Cleanliness { get; set; }
        public int Respecting { get; set; }
        public string CommentText { get; set; }

        public GuestGrade() { }

        public GuestGrade(int guestId, int cleanilness, int respecting, string commentText)
        {
            GuestId = guestId;
            Cleanliness = cleanilness;  
            Respecting = respecting;
            CommentText = commentText;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { GuestId.ToString(), Cleanliness.ToString(), Respecting.ToString(), CommentText};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            GuestId = Convert.ToInt32(values[0]);
            Cleanliness = Convert.ToInt32(values[1]);
            Respecting = Convert.ToInt32(values[2]);
            CommentText = values[3];
        }
    }
}
