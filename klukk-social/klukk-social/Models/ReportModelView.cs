using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
	public class ReportModelView
	{
		public User Parent { get; set; }
		public List<GroupRequest> GroupRequests { get; set; }

		public ReportModelView()
		{
			Parent = new User();
			GroupRequests = new List<GroupRequest>();
		}
	}
}