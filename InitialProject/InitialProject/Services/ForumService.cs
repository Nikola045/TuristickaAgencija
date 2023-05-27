using DevExpress.Utils.CommonDialogs.Internal;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.View.Owner;
using DialogResult = DevExpress.Utils.CommonDialogs.Internal.DialogResult;

namespace TravelAgency.Services
{
    public class ForumService
    {
        private readonly ForumRepository forumRepository;
        private readonly OwnerService ownerService;
        public ForumService()
        {
            forumRepository = new(InjectorService.CreateInstance<IStorage<Forum>>());
            ownerService = new OwnerService();
        }

        public List<Forum> GetNewForums()
        {
            List<Forum> fourms = forumRepository.GetAll();
            List<Forum> newForums = new List<Forum>();
            DateTime dateTime = DateTime.Now;
            foreach(Forum forum in fourms) 
            {
                if(dateTime.Subtract(forum.Date) <= TimeSpan.FromDays(10))
                {
                    newForums.Add(forum);
                }
            }
            return newForums;
        }

        public Forum ShowMessageForForums(string OwnerUsername)
        {
            List<string> locations = ownerService.GetAllOwnerLocation(OwnerUsername);
            foreach(Forum forum in GetNewForums())
            {
                if (locations.Contains(forum.Country + forum.City))
                {
                    string username = forum.Username;
                    string country = forum.Country;
                    string city = forum.City;
                    DialogResult result = (DialogResult)MessageBox.Show($"Guest {username} created new forum on location {country} {city}", "Do you want to see the new forum?", MessageBoxButton.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        return forum;
                    }
                    else
                    {
                        
                    }
                }
                   
            }
            return null;
        }
        
    }
}
