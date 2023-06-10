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
    class NotificationFileStorage : IStorage<Notification>
    {
        private Serializer<Notification> _serializer;
        private readonly string _file = "../../../Resources/Data/notifications.csv";

        public NotificationFileStorage()
        {
            _serializer = new Serializer<Notification>();
        }

        public List<Notification> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Notification> notifications)
        {
            _serializer.ToCSV(_file, notifications);
        }
    }
}
