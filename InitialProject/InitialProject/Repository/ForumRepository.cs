using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;

namespace TravelAgency.Repository
{
    public class ForumRepository
    {
        private readonly List<Forum> forums;
        private readonly IStorage<Forum> _storage;

        public ForumRepository(IStorage<Forum> storage)
        {
            _storage = storage;
            forums = _storage.Load();
        }

        public void Delete(Forum entity)
        {
            Forum founded = forums.Find(c => c.Id == entity.Id);
            forums.Remove(founded);
            _storage.Save(forums);
        }

        public List<Forum> GetAll()
        {
            return forums;
        }

        public int NextId()
        {
            if (forums.Count < 1)
            {
                return 1;
            }
            return forums.Max(h => h.Id) + 1;
        }

        public Forum Save(Forum entity)
        {
            forums.Add(entity);
            _storage.Save(forums);
            return entity;
        }

        public Forum Update(Forum entity)
        {
            Forum current = forums.Find(c => c.Id == entity.Id);
            int index = forums.IndexOf(current);
            forums.Remove(current);
            forums.Insert(index, entity);
            _storage.Save(forums);
            return entity;
        }
    }
}
