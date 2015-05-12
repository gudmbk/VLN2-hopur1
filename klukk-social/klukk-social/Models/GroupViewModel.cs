using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
	public class GroupViewModel
	{
		public List<Group> GroupList { get; set; }

		public GroupViewModel()
		{
			GroupList = new List<Group>();
		}
	}
}