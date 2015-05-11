using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
	public class FriendsViewModel
	{
		public List<User> FriendRequests = new List<User>();
		public List<User> Friends = new List<User>(); 
	}
}