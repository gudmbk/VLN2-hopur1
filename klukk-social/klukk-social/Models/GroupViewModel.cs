using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
	public class GroupViewModel
	{
		public List<Group> GroupList { get; set; }
		public Group Group { get; set; }
		public List<Post> Feed { get; set; }
		public Comment Comment { get; set; }
		public List<User> AllChildren { get; set; }
		public GroupRequest Request { get; set; }
		public bool MemberOfGroup { get; set; }

		public GroupViewModel()
		{
			GroupList = new List<Group>();
			Feed = new List<Post>();
			Comment = new Comment();
			AllChildren = new List<User>();
			Request = new GroupRequest();
			MemberOfGroup = false;
		}
	}
}