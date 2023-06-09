using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Serializer;

namespace TravelAgency.Domain.Model
{
    public class ForumComment : ISerializable
    {
        public int Id { get; set; }
        public Forum Forum { get; set; } = new Forum();
        public string Username { get; set; }
        public string Role { get; set; }
        public string ValidComment { get; set; }
        public string Useful { get; set; }
        public int NumberOfReport { get; set; }
        public string Comment { get; set; }

        public ForumComment() { }
        public ForumComment(Forum forum, string validComment, string useful, string comment)
        {
            Forum = forum;
            ValidComment = validComment;
            Useful = useful;
            NumberOfReport = 0;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Forum.Id.ToString(), Username, Role, ValidComment, Useful, NumberOfReport.ToString(), Comment };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Forum.Id = Convert.ToInt32(values[1]);
            Username = values[2];
            Role = values[3];
            ValidComment = values[4];
            Useful = values[5];
            NumberOfReport = Convert.ToInt32(values[6]);
            Comment = values[7];
        }

    }
}
