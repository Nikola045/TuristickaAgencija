using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Domain.Model;
using TravelAgency.Forms;
using TravelAgency.Repository.HotelRepo;
using Image = TravelAgency.Domain.Model.Image;

namespace TravelAgency.Services
{
    internal class HotelService
    {
        private readonly App app = (App)App.Current;
        private ImageRepository imageRepository { get; }
        private HotelRepository hotelRepository { get; }

        public HotelService() 
        {
            imageRepository = app.ImageRepository;
            hotelRepository = app.HotelRepository;
        }

        public List<Hotel> FindHotel(string name, string city, string country, string type, string max, string days)
        {
            List<Hotel> hotels = hotelRepository.GetAll();
            List<Hotel> findedHotels = new List<Hotel>();

            foreach(Hotel hotel in hotels)
            {
                bool requirementsMet = true;
                if (!string.IsNullOrEmpty(name) && hotel.Name != name)
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(city) && hotel.City != city)
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(country) && hotel.Country != country)
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(type) && hotel.TypeOfHotel != type)
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(max) && hotel.MaxNumberOfGuests < int.Parse(max))
                {
                    requirementsMet = false;
                }
                if (!string.IsNullOrEmpty(days) && hotel.MinNumberOfDays > int.Parse(days))
                {
                    requirementsMet = false;
                }

                if (requirementsMet)
                {
                    findedHotels.Add(hotel);
                }
            }
                    
            return SortBySuperOwner(findedHotels.Distinct().ToList());
        }

        public Hotel GetHotelByName(string name)
        {
            List<Hotel> hotelList = hotelRepository.GetAll();
            foreach(Hotel hotel in hotelList)
            {
                if(hotel.Name == name)
                    return hotel;
            }
            return null;
        }

        public Hotel GetHotelById(int id)
        {
            List<Hotel> hotelList = hotelRepository.GetAll();
            foreach (Hotel hotel in hotelList)
            {
                if (hotel.Id == id)
                    return hotel;
            }
            return null;
        }

        public List<Hotel> GetHotelByOwner(string username)
        {
            List<Hotel> hotelList = hotelRepository.GetAll();
            List<Hotel> findedHotels = new List<Hotel>();
            foreach (Hotel hotel in hotelList)
            {
                if (hotel.OwnerUsername == username)
                    findedHotels.Add(hotel);        
            }
            return findedHotels;
        }

        public Image FindByUrl(string url)
        {
            List<Image> hotelImages = imageRepository.GetAll();
            foreach (Image hotelImage in hotelImages)
            {
                if (hotelImage.Url == url)
                {
                    return hotelImage;
                }
            }
            return null;
        }

        public List<Image> FindAllById(int id)
        {
            List<Image> hotelImages = imageRepository.GetAll();
            List<Image> findedImages = new List<Image>();
            foreach (Image hotelImage  in hotelImages)
            {
                if (hotelImage.Id == id)
                {
                    findedImages.Add(hotelImage);
                }
            }
            return findedImages;
        }

        public void SaveHotel(bool validator, string username)
        {
            if (validator) 
            {
                Hotel newHotel = new Hotel(
                    
                     hotelRepository.NextId(),
                     OwnerForm.ownerForm.txtName.Text,
                     OwnerForm.ownerForm.txtCity.Text,
                     OwnerForm.ownerForm.txtCountry.Text,
                     OwnerForm.ownerForm.Type.Text,
                     Convert.ToInt32(OwnerForm.ownerForm.brMax.Text),
                     Convert.ToInt32(OwnerForm.ownerForm.brMin.Text),
                     Convert.ToInt32(OwnerForm.ownerForm.brDaysLeft.Text));
                newHotel.OwnerUsername = username;
                Hotel savedHotel = hotelRepository.Save(newHotel);

                MessageBox.Show("Accommodation successfully created");

                OwnerForm.ownerForm.txtName.Clear();
                OwnerForm.ownerForm.txtCity.Clear();
                OwnerForm.ownerForm.txtCountry.Clear();
                OwnerForm.ownerForm.brMax.Clear();
                OwnerForm.ownerForm.brMin.Clear();
                OwnerForm.ownerForm.brDaysLeft.Clear();
                OwnerForm.ownerForm.ImageList.Items.Clear();
            }
            else
            {
                MessageBox.Show("Please check your input datas");
            }
        }

        public List<Hotel> SortBySuperOwner(List<Hotel> hotels)
        {
            List<Hotel> sortedHotels = new List<Hotel>();
            hotels.Reverse();
            foreach (Hotel hotel in hotels)
            {
                if(Regex.IsMatch(hotel.OwnerUsername, @"^[a-zA-Z0-9\s]+\sSuper-Owner$"))
                    sortedHotels.Insert(0, hotel);
            }
            foreach (Hotel hotel in hotels)
            {
                if (!Regex.IsMatch(hotel.OwnerUsername, @"^[a-zA-Z0-9\s]+\sSuper-Owner$"))
                    sortedHotels.Add(hotel);
            }

            return sortedHotels;
        }

        public void AddHotelImage()
        {
            Image newImage = new Image(
               imageRepository.NextId(),
               OwnerForm.ownerForm.txtImg.Text);

            Image savedImage = imageRepository.Save(newImage);
            OwnerForm.ownerForm.ImageList.Items.Add(OwnerForm.ownerForm.txtImg.Text);
            MessageBox.Show("You have successfully added an image");
            OwnerForm.ownerForm.LabelImgValidator.Content = "";
            OwnerForm.ownerForm.txtImg.Clear();
        }

        public void DeleteHotelImage()
        {
            object selectedItem = OwnerForm.ownerForm.ImageList.SelectedItem;
            Image hotelImage = FindByUrl(selectedItem.ToString());

            OwnerForm.ownerForm.ImageList.Items.Remove(selectedItem);
            imageRepository.Delete(hotelImage);
            if (OwnerForm.ownerForm.ImageList.Items.IsEmpty == true)
            {
                OwnerForm.ownerForm.LabelImgValidator.Content = "Please add at least one image";
            }
        }

        public void ClearListOfImage()
        {
            foreach (object item in OwnerForm.ownerForm.ImageList.Items)
            {
                Image hotelImage = FindByUrl(item.ToString());
                imageRepository.Delete(hotelImage);
            }

        }

        public List<ComboBoxItem> FillForComboBoxHotels(User user)
        {
            List<ComboBoxItem> hotelsCB = new List<ComboBoxItem>();
            List<Hotel> hotels = GetHotelByOwner(user.Username);
            foreach (Hotel hotel in hotels)
            {
                ComboBoxItem cbItem = new ComboBoxItem();
                cbItem.Tag = hotel.Id.ToString();
                cbItem.Content = hotel.Name;
                hotelsCB.Add(cbItem);
            }
            return hotelsCB;
        }
    }
}
