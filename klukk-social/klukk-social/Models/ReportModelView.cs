using System.Collections.Generic;

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