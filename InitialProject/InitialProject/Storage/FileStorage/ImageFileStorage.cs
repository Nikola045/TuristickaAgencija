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
    internal class ImageFileStorage : IStorage<Image>
    {
        private Serializer<Image> _serializer;
        private readonly string _file = "../../../Resources/Data/hotelsImg.csv";

        public ImageFileStorage()
        {
            _serializer = new Serializer<Image>();
        }

        public List<Image> Load()
        {
            return _serializer.FromCSV(_file);
        }

        public void Save(List<Image> images)
        {
            _serializer.ToCSV(_file, images);
        }
    }
}
