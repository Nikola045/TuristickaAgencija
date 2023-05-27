using System.Collections.Generic;
using System.Linq;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository.GradeRepo;
using TravelAgency.Repository.UserRepo;

namespace TravelAgency.Services
{
    internal class OwnerService
    {
        private readonly OwnerGradeRepository ownerGradeRepository;
        private readonly UserRepository userRepository;
        private readonly HotelService hotelService;
        public OwnerService()
        {
            ownerGradeRepository = new(InjectorService.CreateInstance<IStorage<OwnerGrade>>());
            userRepository = new(InjectorService.CreateInstance<IStorage<User>>());
            hotelService = new HotelService();
        }

        public int CountGradesFromOwnerRating(string OwnerUserName)
        {
            int count = 0;
            List<OwnerGrade> grades = ownerGradeRepository.GetAll();
            foreach(OwnerGrade grade in grades)
            {
                if (grade.Owner.Username == OwnerUserName)
                    count++;
            }
            return count;
        }
        public int GetAverageOwnerRating(string OwnerUserName)
        {
            int Grade = 0;
            List<OwnerGrade> grades = ownerGradeRepository.GetAll();
            foreach (OwnerGrade grade in grades)
            {
                if (grade.Owner.Username == OwnerUserName)
                    Grade = Grade + grade.OwnerRating;
            }
            return Grade / CountGradesFromOwnerRating(OwnerUserName);
        }

        public string SuperOwner(string username)
        {
            if (CountGradesFromOwnerRating(username) >= 50)
            {
                if (GetAverageOwnerRating(username) < 4.5)
                {
                    return "Owner";
                }
                else
                {
                    return "Super-Owner";
                }
            }
            else
            {
                return "Owner";
            }

        }

        public List<User> GetAllOwners()
        {
            List<User> users = userRepository.GetAll();
            List<User> owners = new List<User>();
            foreach (User user in users)
            {
                if(user.LoginRole == "Owner")
                    owners.Add(user);
            }
            return owners;

        }

        public List<User> GetAllSuperOwners()
        {
            List<User> owners = GetAllOwners();
            List<User> superOwners = new List<User>();
            foreach (User owner in owners)
            {
                if (SuperOwner(owner.Username) == "Super-Owner")
                    superOwners.Add(owner);
            }
            return superOwners.Distinct().ToList();
        }

        public void UpadateUsername(User user)
        {
            userRepository.Update(user); 
        }

        public User GetOwnerByUsername(string username)
        {
            List<User> users = userRepository.GetAll();
            foreach(User user in users)
            {
                if(username == user.Username) return user;
            }
            return null;
        }

        public List<string> GetAllOwnerLocation(string OwnerUsername)
        {
            List<Hotel> hotels = hotelService.GetHotelByOwner(OwnerUsername);
            List<string> locations = new List<string>();
            foreach(Hotel hotel in hotels)
            {
                locations.Add(hotel.Country + hotel.City);
            }
            return locations;
        }
    }
}
