using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Serializer;

namespace TravelAgency.Repository.HotelRepo
{
    internal class ImageRepository
    {
        private const string FilePath = "../../../Resources/Data/hotels.csv";
        private const string FilePathForImages = "../../../Resources/Data/hotelsImg.csv";

        private readonly Serializer<Image> _serializer;

        private List<Image> images;
        private List<Image> hotels;

        public ImageRepository()
        {
            _serializer = new Serializer<Image>();
            images = _serializer.FromCSV(FilePathForImages);
            hotels = _serializer.FromCSV(FilePath);
        }

        public Image Save(Image image)
        {
            image.Id = NextId();
            images = _serializer.FromCSV(FilePathForImages);
            images.Add(image);
            _serializer.ToCSV(FilePathForImages, images);
            return image;
        }

        public void Delete(Image image)
        {
            images = _serializer.FromCSV(FilePathForImages);
            Image founded = images.Find(hi => hi.Url == image.Url);
            images.Remove(founded);
            _serializer.ToCSV(FilePathForImages, images);
        }

        public int NextId()
        {
            hotels = _serializer.FromCSV(FilePath);
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
