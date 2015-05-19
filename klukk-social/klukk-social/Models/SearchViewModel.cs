using System.Collections.Generic;

namespace klukk_social.Models
{
	public class GroupWithMembership
	{
		public Group Group { get; set; }
		public bool IsMember { get; set; }
		public bool AskedForAccess { get; set; }
	}

	public class UserWithFriendship
	{
		public User User { get; set; }
		public bool IsFriends { get; set; }
		public bool HasSentRequest { get; set; }
		public bool HasUnansweredRequest { get; set; }
	}

	public class SearchViewModel
	{
		public List<GroupWithMembership> Groups { get; set; }
 		public List<UserWithFriendship> Users { get; set; }
		public string searchString { get; set; }

		public SearchViewModel()
		{
			Groups = new List<GroupWithMembership>();
			Users = new List<UserWithFriendship>();
			searchString = "";
		}

		public SearchViewModel(List<GroupWithMembership> groups, List<UserWithFriendship> users, string search)
		{
			Groups = groups;
			Users = users;
			searchString = search;
		}
	}
}