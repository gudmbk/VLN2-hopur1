using System.Collections.Generic;
using System.Security.Policy;

namespace klukk_social.Models
{
	public class SearchViewModel
	{
		public List<Group> Groups { get; set; }
        public List<bool> IsMember { get; set; } 
		public List<User> Users { get; set; }
        public List<bool> IsFriend { get; set; } 

		public SearchViewModel()
		{
			Groups = new List<Group>();
			Users = new List<User>();
            IsMember = new List<bool>();
            IsFriend = new List<bool>();
		}
		
		public SearchViewModel(List<Group> groups, List<User> users)
		{
			Groups = groups;
			Users = users;
		}
	}
}