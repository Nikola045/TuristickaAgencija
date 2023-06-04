using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;

namespace TravelAgency.Repository
{
    public class ForumCommentRepository
    {
        private readonly List<ForumComment> comments;
        private readonly IStorage<ForumComment> _storage;

        public ForumCommentRepository(IStorage<ForumComment> storage)
        {
            _storage = storage;
            comments = _storage.Load();
        }

        public void Delete(ForumComment entity)
        {
            ForumComment founded = comments.Find(c => c.Id == entity.Id);
            comments.Remove(founded);
            _storage.Save(comments);
        }

        public List<ForumComment> GetAll()
        {
            return comments;
        }

        public int NextId()
        {
            if (comments.Count < 1)
            {
                return 1;
            }
            return comments.Max(h => h.Id) + 1;
        }

        public ForumComment Save(ForumComment entity)
        {
            comments.Add(entity);
            _storage.Save(comments);
            return entity;
        }

        public ForumComment Update(ForumComment entity)
        {
            ForumComment current = comments.Find(c => c.Id == entity.Id);
            int index = comments.IndexOf(current);
            comments.Remove(current);
            comments.Insert(index, entity);
            _storage.Save(comments);
            return entity;
        }
    }
}
