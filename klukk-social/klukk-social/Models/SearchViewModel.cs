using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
	public class SearchViewModel
	{
		public List<Group> Groups { get; set; }
		public List<User> Users { get; set; }

		public SearchViewModel()
		{
			Groups = new List<Group>();
			Users = new List<User>();
		}
		
		public SearchViewModel(List<Group> groups, List<User> users)
		{
			Groups = groups;
			Users = users;
		}
	}
}