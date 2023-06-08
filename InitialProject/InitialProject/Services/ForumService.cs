using System;
using System.Collections.Generic;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using DialogResult = DevExpress.Utils.CommonDialogs.Internal.DialogResult;

namespace TravelAgency.Services
{
    public class ForumService
    {
        private readonly ForumRepository forumRepository;
        private readonly ForumCommentRepository forumCommentRepository;
        private readonly OwnerService ownerService;
        private readonly ReservationService reservationService;
        public ForumService()
        {
            forumRepository = new(InjectorService.CreateInstance<IStorage<Forum>>());
            forumCommentRepository = new(InjectorService.CreateInstance<IStorage<ForumComment>>());
            ownerService = new OwnerService();
            reservationService = new ReservationService();
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
                if (locations.Contains(forum.Country +"|"+ forum.City))
                {
                    string username = forum.Guest1.Username;
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

        public bool IsOwnerLocation(string OwnerUsername, Forum forum)
        {
            List<string> locations = ownerService.GetAllOwnerLocation(OwnerUsername);
            if (locations.Contains(forum.Country + "|" + forum.City))
            {
                 return true;
            }
            else return false;
        }

        public List<ForumComment> GetForumComments(Forum forum)
        {
            List<ForumComment> comments = forumCommentRepository.GetAll();
            List<ForumComment> forumComments = new List<ForumComment>();
            foreach (ForumComment comment in comments)
            {
                if(comment.Forum.Id == forum.Id)
                {
                    forumComments.Add(comment);
                }
            }
            return forumComments;
        }

        public void ReportComment(ForumComment selectedComment)
        {
            List<ForumComment> comments = forumCommentRepository.GetAll();
            foreach (ForumComment comment in comments)
            {
                if(comment.Id == selectedComment.Id)
                {
                    selectedComment.NumberOfReport++;
                    MessageBox.Show("Comment successfully reported");
                }
            }
            forumCommentRepository.Update(selectedComment);
        }
        public void UpdateUsefull()
        {
            List<Forum> forums = forumRepository.GetAll();
            foreach (Forum forum in forums)
            {
                forum.VeryUseful = IsForumVeryUsefulBool(forum);
                forumRepository.Update(forum);
            }            
        }
        public void CloseForum(Forum selectedForum)
        {
            List<Forum> forums = forumRepository.GetAll();
            for (int i = 0; i < forums.Count; i++)
            {
                if (selectedForum.Id == forums[i].Id)
                {
                    forums[i].IsActive = false;
                    forumRepository.Update(forums[i]);
                }
            }
        }

        public void CreateCommentOfOwner(User user, ForumComment comment)
        {
            comment.Id = forumCommentRepository.NextId();
            comment.Username = user.Username;
            comment.Role = user.LoginRole;
            string validation = "No";
            if (IsOwnerLocation(user.Username, comment.Forum))
                validation = "Yes";
            comment.ValidComment = validation;

            forumCommentRepository.Save(comment);

            MessageBox.Show("Comment successfully created");
        }
        public void CreateCommentOfGuest1(User user, ForumComment comment)
        {
            comment.Id = forumCommentRepository.NextId();
            comment.Username = user.Username;
            comment.Role = user.LoginRole;
            string validation = "No";
            if (HasGuest1VisitedLocation(user.Username, comment.Forum))
                validation = "Yes";
            comment.ValidComment = validation;

            forumCommentRepository.Save(comment);

            MessageBox.Show("Comment successfully created");
        }
        public bool HasGuest1VisitedLocation(string Guest1Username, Forum forum)
        {
            List<string> locations = reservationService.GetGuest1Reservations(Guest1Username);
            if (locations.Contains(forum.Country + "|" + forum.City))
            {
                return true;
            }
            else return false;
        }
        public int CountOwnerComments(Forum forum)
        {
            List<ForumComment> comments = forumCommentRepository.GetAll();
            int counter = 0;
            foreach (ForumComment comment in comments)
            {
                if (comment.Role == "Owner" && comment.Forum.Id == forum.Id)
                {
                    counter++;   
                }
            }
            return counter;
        }
        public int CountGuestComments(Forum forum)
        {
            List<ForumComment> comments = forumCommentRepository.GetAll();
            int counter = 0;
            foreach (ForumComment comment in comments)
            {
                if (comment.Role == "Guest1" && comment.Forum.Id == forum.Id)
                {
                    counter++;
                }
            }
            return counter;
        }

        public string IsForumVearyUseful(Forum forum)
        {
            if (CountGuestComments(forum) >= 20 && CountOwnerComments(forum) >= 10)
                return "Very useful";
            else
                return "";
        }
        public bool IsForumVeryUsefulBool(Forum forum)
        {
            if (CountGuestComments(forum) >= 20 && CountOwnerComments(forum) >= 10)
                return true;
            else
                return false;
        }
        public void CreateForum(User guest1,Forum forum)
        {
            forum.Id = forumRepository.NextId();
            forum.Guest1 = guest1;
            forum.NumberOfReplies = 1;
            forum.Date = DateTime.Now;
            forum.VeryUseful = IsForumVeryUsefulBool(forum);
            forumRepository.Save(forum);
        }
    }
}
