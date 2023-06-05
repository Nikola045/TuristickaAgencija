using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using Cake.Core.IO;
using System.IO;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;
using TravelAgency.View.Guest2;

namespace TravelAgency.Repository
{
    class NotificationsRepository
    {

        private IStorage<Notification> _storage;
        private List<Notification> _notifications;

        public NotificationsRepository(IStorage<Notification> storage)
        {
            _storage = storage;
            _notifications = _storage.Load();
        }

        public Notification Save(Notification entity)
        {
            _notifications.Add(entity);
            _storage.Save(_notifications);
            return entity;
        }

        public int NextId()
        {
            if (_notifications.Count < 1)
            {
                return 1;
            }
            return _notifications.Max(h => h.Id) + 1;
        }

        public List<Notification> GetAll()
        {
            return _notifications;
        }
    }
}
