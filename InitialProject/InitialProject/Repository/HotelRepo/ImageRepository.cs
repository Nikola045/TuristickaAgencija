using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Serializer;

namespace TravelAgency.Repository.HotelRepo
{
    public class ImageRepository
    {
        private const string FilePath = "../../../Resources/Data/hotels.csv";

        private readonly Serializer<Image> _serializer;

        private List<Image> images;
        private List<Image> hotels;
        private IStorage<Image> _storage;

        public ImageRepository(IStorage<Image> storage)
        {
            _storage = storage;
            _serializer = new Serializer<Image>();
            images = _storage.Load();
            hotels = _serializer.FromCSV(FilePath);
        }

        public Image Save(Image image)
        {
            image.Id = NextId();
            images.Add(image);
            _storage.Save(images);
            return image;
        }

        public void Delete(Image image)
        {
            Image founded = images.Find(hi => hi.Url == image.Url);
            images.Remove(founded);
            _storage.Save(images);
        }

        public int NextId()
        {
            if (hotels.Count < 1)
            {
                return 1;
            }
            return hotels.Count + 1;
        }

        public List<Image> GetAll()
        {
            return images;
        }
    }
}
