using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Storage.FileStorage
{
    public class FormCommentFileStorage : IStorage<ForumComment>
    {
        private Serializer<ForumComment> _serializer;
        private readonly string _file = "../../../Resources/Data/forumComments.csv";

        public FormCommentFileStorage()
        {
            _serializer = new Serializer<ForumComment>();
        }

        public List<ForumComment> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<ForumComment> comments)
        {
            _serializer.ToCSV(_file, comments);
        }
    }
}
