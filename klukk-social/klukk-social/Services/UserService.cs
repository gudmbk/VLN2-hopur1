﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using klukk_social.Models;

namespace klukk_social.Services
{
	public class UserService
	{
        readonly IAppDataContext _dbContext;

        public UserService(IAppDataContext context)
        {
            _dbContext = context ?? new ApplicationDbContext();
        }

		public User FindById(string userId)
		{
            var user = (from u in _dbContext.Users
						where u.Id == userId
                        select u).Include("Reports").FirstOrDefault();
			return user;

		}

	    public User GetParentByUserId(string childId)
	    {
            var user = _dbContext.Users.Single(u => u.Id == childId);
            var parent = _dbContext.Users.Single(u => u.Id == user.ParentId);
	        return parent;
	    }

		public List<User> Search(string prefix)
		{
            var user = (from u in _dbContext.Users
						where u.FullName.Contains(prefix) && u.ParentId != null
						orderby u.FullName
						select u).ToList();
			return user;
		}

		public List<User> GetAllChildren(string parentId)
		{
            return (from p in _dbContext.Users
					where p.ParentId == parentId
					orderby p.FirstName descending
					select p).ToList();
		}

		public string GetFullNameById(string getUserId)
		{
            var name = (from u in _dbContext.Users
						where u.Id == getUserId
						select u.FullName).FirstOrDefault();
			return name;

		}

		public string GetProfileUrl(string userId)
		{
            return (from p in _dbContext.Users
					where p.Id == userId
					select p.ProfilePic).FirstOrDefault();
		}

		public void SendFriendRequest(FriendRequest friendRequest)
		{
            _dbContext.FriendRequests.Add(friendRequest);
            _dbContext.SaveChanges();
		}

		public string GetParentId(string childId)
		{
            return (from user in _dbContext.Users
					where user.Id == childId
					select user.ParentId).FirstOrDefault();
		}

		public bool IsParentsChild(string parent, string childId)
		{
			return (parent == GetParentId(childId));
		}

		public bool FriendChecker(string userId, string friendId)
		{
            var friends = (from fs in _dbContext.Friendships
						   where fs.FromUserId == friendId && fs.ToUserId == userId
						   select fs).FirstOrDefault();
			return friends != null;

		}

		public List<User> GetFriendRequest(string userId)
		{
            var requests = (from u in _dbContext.Users
                            join fr in _dbContext.FriendRequests
							on u.Id equals fr.FromUserId
							where fr.ToUserId == userId
							select u).ToList();
			return requests;

		}

		public FriendRequest GetFriendRequest(string userId, string friendId)
		{
            var request = (from fr in _dbContext.FriendRequests
						   where fr.FromUserId == friendId && fr.ToUserId == userId || fr.FromUserId == userId && fr.ToUserId == friendId
						   select fr).FirstOrDefault();
			return request;

		}

		public List<User> GetFriends(string userId)
		{
            var friends = (from u in _dbContext.Users
                           join f in _dbContext.Friendships
						   on u.Id equals f.ToUserId
						   where f.FromUserId == userId
						   select u).ToList();
			return friends;

		}

		public void DeleteFriendRequest(FriendRequest friendRequest)
		{
            var toDelete = (from fr in _dbContext.FriendRequests
							where fr.FromUserId == friendRequest.FromUserId && fr.ToUserId == friendRequest.ToUserId
							select fr).FirstOrDefault();

            _dbContext.FriendRequests.Remove(toDelete);
            _dbContext.SaveChanges();

		}

		public void MakeFriends(Friendship friends)
		{
            _dbContext.Friendships.Add(friends);
            _dbContext.Friendships.Add(Switcheroo(friends));
            _dbContext.SaveChanges();

		}


		public void DeleteKid(string userId) //NOT READY
		{
            var toDelete = (from user in _dbContext.Users
							where user.Id == userId
							select user).FirstOrDefault();

            _dbContext.Users.Remove(toDelete);
			_dbContext.SaveChanges();

		}

		// Helper functions
		private Friendship Switcheroo(Friendship friends)
		{
			Friendship switched = new Friendship();
			switched.Date = friends.Date;
			switched.FromUserId = friends.ToUserId;
			switched.ToUserId = friends.FromUserId;
			return switched;
		}

		public List<UserWithFriendship> SearchUsersWithFriendship(string prefix, string currUser)
		{
			List<UserWithFriendship> returnlist = new List<UserWithFriendship>();

			var users = (from u in _dbContext.Users
						where u.FullName.Contains(prefix) && u.ParentId != null && u.Id != currUser
						orderby u.FullName
						select u).ToList();
			
			foreach (var user in users)
			{
				var areFriends = (from friendshp in _dbContext.Friendships
								  where friendshp.FromUserId == currUser && friendshp.ToUserId == user.Id
								  select friendshp).FirstOrDefault();
				
				UserWithFriendship uwf = new UserWithFriendship(user, areFriends != null);
				if (!uwf.IsFriends)
				{
					var requestSent = (from request in _dbContext.FriendRequests
									   where request.FromUserId == currUser && request.ToUserId == user.Id
									   select request).FirstOrDefault();
					uwf.HasSentRequest = requestSent != null;
					if (!uwf.HasSentRequest)
					{
						var requestRecieved = (from request in _dbContext.FriendRequests
											   where request.ToUserId == currUser && request.FromUserId == user.Id
											   select request).FirstOrDefault();
						uwf.HasUnansweredRequest = requestRecieved != null;
					}
				}
				uwf.User = user;
				returnlist.Add(uwf);
			}

			return returnlist;
		}
		
	    public void RemoveFriendship(string userId, string friendId)
	    {
	        var fs1 = (from fs in _dbContext.Friendships
	            where fs.FromUserId == friendId && fs.ToUserId == userId
	            select fs).FirstOrDefault();
            var fs2 = (from fs in _dbContext.Friendships
                       where fs.FromUserId == userId && fs.ToUserId == friendId
                       select fs).FirstOrDefault();
	        _dbContext.Friendships.Remove(fs1);
            _dbContext.Friendships.Remove(fs2);
	        _dbContext.SaveChanges();
	    }


		public List<UserWithFriendship> GetFriendsForCard(string currUser)
		{
			List<UserWithFriendship> returnlist = new List<UserWithFriendship>();

			var friends = (from f in _dbContext.Friendships
						 join u in _dbContext.Users
						 on f.ToUserId equals u.Id
						 where f.FromUserId == currUser && f.ToUserId != currUser
						 orderby u.FullName
						 select u).ToList();

			foreach (var user in friends)
			{
				UserWithFriendship uwf = new UserWithFriendship(user, true);
				uwf.HasUnansweredRequest = false;
				uwf.HasSentRequest = false;
				returnlist.Add(uwf);
			}
			return returnlist;
		}

		public List<UserWithFriendship> GetRequestsForCard(string currUser)
		{
			List<UserWithFriendship> returnlist = new List<UserWithFriendship>();

			var recivedRequests = (from f in _dbContext.FriendRequests
								   join u in _dbContext.Users
								   on f.FromUserId equals u.Id
								   where f.ToUserId == currUser
								   orderby u.FullName
								   select u).ToList();

			foreach (var user in recivedRequests)
			{
				UserWithFriendship uwf = new UserWithFriendship(user, false);
				uwf.HasUnansweredRequest = true;
				uwf.HasSentRequest = false;
				returnlist.Add(uwf);
			}
			return returnlist;
		}

		public List<UserWithFriendship> GetUnansweredRequestsForCard(string currUser)
		{
			List<UserWithFriendship> returnlist = new List<UserWithFriendship>();

			var requests = (from u in _dbContext.Users
							join f in _dbContext.FriendRequests
							on u.Id equals f.ToUserId
							where f.FromUserId == currUser
							orderby u.FullName
							select u).ToList();

			foreach (var user in requests)
			{
				UserWithFriendship uwf = new UserWithFriendship(user, false);
				uwf.HasUnansweredRequest = false;
				uwf.HasSentRequest = true;
				returnlist.Add(uwf);
			}
			return returnlist;
		}
	}
}
