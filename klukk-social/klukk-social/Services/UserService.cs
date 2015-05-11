﻿using System.Collections.Generic;
using System.Linq;
using klukk_social.Models;
using klukk_social.Services;

namespace klukk_social.Services
{
	public class UserService
    {
        public User FindById(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var user = (from u in dbContext.Users
                    where u.Id == userId
                    select u).FirstOrDefault();
                return user;
            }
        }

        public List<User> Search(string prefix)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var user = (from u in dbContext.Users
                            where u.FirstName.Contains(prefix) && u.ParentId != null
                            orderby u.FirstName
                            select u).ToList();
                return user;
            }
        }

		public List<User> GetAllChildren(string parentId)
		{
			using (var dbContext = new ApplicationDbContext())
			return (from p in dbContext.Users
					where p.ParentId == parentId
					orderby p.FirstName descending
					select p).ToList();
		}

	    public string GetFullNameById(string getUserId)
	    {
            using (var dbContext = new ApplicationDbContext())
            {
                var name = (from u in dbContext.Users
                    where u.Id == getUserId 
                    select u.FullName).FirstOrDefault();
                return name;
            }
	    }

		public string GetProfileUrl(string userId)
		{
			using (var dbContext = new ApplicationDbContext())
				return (from p in dbContext.Users
						where p.Id == userId
						select p.ProfilePic).FirstOrDefault();
		}

	    public void SendFriendRequest(FriendRequest friendRequest)
	    {
	        using (var dbContext = new ApplicationDbContext())
	        {
	            dbContext.FriendRequests.Add(friendRequest);
	            dbContext.SaveChanges();
	        }
	    }

		public string GetParentId(string childId)
		{
			using (var dbContext = new ApplicationDbContext())
				return (from user in dbContext.Users
						where user.Id == childId
						select user.ParentId).FirstOrDefault();
			
		}
		
		public bool IsParentsChild(string parent, string childId)
		{
			return (parent == GetParentId(childId));
		}

		public bool FriendChecker(string userId, string friendId)
		{

			return true;
		}
    }
}