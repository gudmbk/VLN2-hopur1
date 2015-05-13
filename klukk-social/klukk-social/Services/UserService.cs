using System.Collections.Generic;
using System.Linq;
using klukk_social.Models;

namespace klukk_social.Services
{
	public class UserService
	{
		public User FindById(string userId)
		{
			var dbContext = new ApplicationDbContext();

			var user = (from u in dbContext.Users
						where u.Id == userId
						select u).FirstOrDefault();
			return user;

		}

		public List<User> Search(string prefix)
		{
			var dbContext = new ApplicationDbContext();
			var user = (from u in dbContext.Users
						where u.FirstName.Contains(prefix) && u.ParentId != null
						orderby u.FirstName
						select u).ToList();
			return user;
		}

		public List<User> GetAllChildren(string parentId)
		{
			var dbContext = new ApplicationDbContext();
			return (from p in dbContext.Users
					where p.ParentId == parentId
					orderby p.FirstName descending
					select p).ToList();
		}

		public string GetFullNameById(string getUserId)
		{
			var dbContext = new ApplicationDbContext();

			var name = (from u in dbContext.Users
						where u.Id == getUserId
						select u.FullName).FirstOrDefault();
			return name;

		}

		public string GetProfileUrl(string userId)
		{
			var dbContext = new ApplicationDbContext();
			return (from p in dbContext.Users
					where p.Id == userId
					select p.ProfilePic).FirstOrDefault();
		}

		public void SendFriendRequest(FriendRequest friendRequest)
		{
			var dbContext = new ApplicationDbContext();

			dbContext.FriendRequests.Add(friendRequest);
			dbContext.SaveChanges();

		}

		public string GetParentId(string childId)
		{
			var dbContext = new ApplicationDbContext();
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
			var dbContext = new ApplicationDbContext();

			var friends = (from fs in dbContext.Friendships
						   where fs.FromUserId == friendId && fs.ToUserId == userId
						   select fs).FirstOrDefault();
			return friends != null;

		}

		public List<User> GetFriendRequest(string userId)
		{
			var dbContext = new ApplicationDbContext();

			var requests = (from u in dbContext.Users
							join fr in dbContext.FriendRequests
							on u.Id equals fr.FromUserId
							where fr.ToUserId == userId
							select u).ToList();
			return requests;

		}

		public FriendRequest GetFriendRequest(string userId, string friendId)
		{
			var dbContext = new ApplicationDbContext();

			var request = (from fr in dbContext.FriendRequests
						   where fr.FromUserId == friendId && fr.ToUserId == userId || fr.FromUserId == userId && fr.ToUserId == friendId
						   select fr).FirstOrDefault();
			return request;

		}

		public List<User> GetFriends(string userId)
		{
			var dbContext = new ApplicationDbContext();

			var friends = (from u in dbContext.Users
						   join f in dbContext.Friendships
						   on u.Id equals f.ToUserId
						   where f.FromUserId == userId
						   select u).ToList();
			return friends;

		}

		public void DeleteFriendRequest(FriendRequest friendRequest)
		{
			var dbContext = new ApplicationDbContext();

			var toDelete = (from fr in dbContext.FriendRequests
							where fr.FromUserId == friendRequest.FromUserId && fr.ToUserId == friendRequest.ToUserId
							select fr).FirstOrDefault();

			dbContext.FriendRequests.Remove(toDelete);
			dbContext.SaveChanges();

		}

		public void MakeFriends(Friendship friends)
		{
			var dbContext = new ApplicationDbContext();

			dbContext.Friendships.Add(friends);
			dbContext.Friendships.Add(Switcheroo(friends));
			dbContext.SaveChanges();

		}


		public void DeleteKid(string userId) //NOT READY
		{
			var dbContext = new ApplicationDbContext();

			var toDelete = (from user in dbContext.Users
							where user.Id == userId
							select user).FirstOrDefault();

			dbContext.Users.Remove(toDelete);
			dbContext.SaveChanges();

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
	}
}
