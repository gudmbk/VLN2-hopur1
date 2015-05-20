using System.Collections.Generic;

namespace klukk_social.Models
{
	public class FriendsViewModel
	{
		public List<UserWithFriendship> Friends { get; set; }
		public List<UserWithFriendship> Requests { get; set; }
		public List<UserWithFriendship> UnasweredRequests { get; set; }

		public FriendsViewModel()
		{
			Friends = new List<UserWithFriendship>();
			Requests = new List<UserWithFriendship>();
			UnasweredRequests = new List<UserWithFriendship>();
		}
	}
}