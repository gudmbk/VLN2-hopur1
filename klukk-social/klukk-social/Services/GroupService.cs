using klukk_social.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data.Entity;

namespace klukk_social.Services
{
    public class GroupService
    {
        public void CreateGroup(Group group)
        {
			var dbContext = new ApplicationDbContext();
			dbContext.Groups.Add(group);
			dbContext.SaveChanges();
        }

		public List<Group> GetAllGroups(string userID)
		{
			var dbContext = new ApplicationDbContext();
			var groups = (from g in dbContext.Groups
							  join gl in dbContext.GroupUsers
							  on g.Id equals gl.GroupId
							  where gl.UserId == userID
							  select g).ToList();
			return groups;

		}
		
		//FindById(groupId)
		public Group FindById(int ?groupId)
		{
			var dbContext = new ApplicationDbContext();
			
			var group = (from g in dbContext.Groups
						 where g.Id == groupId
						 select g).FirstOrDefault();
			return group;
			
		}
		
		public List<Post> GetAllGroupPostsToGroup(int? groupId)
        {
			var dbContext = new ApplicationDbContext();
            
			var listi = (from p in dbContext.Posts
			    where p.GroupId == groupId
			    orderby p.Date descending
						 select p).Include("Likes").ToList();
			
			foreach (var item in listi)
			{
			    item.Comments = (from comment in dbContext.Comments
			        where comment.PostId == item.Id
			        orderby comment.Date ascending
			        select comment).ToList();
			}
			return listi;
            
        }

		public List<Group> Search(string prefix)
		{
			var dbContext = new ApplicationDbContext();
			var group = (from g in dbContext.Groups
						where g.Name.Contains(prefix)
						orderby g.Name
						select g).ToList();
			return group;
		}

		public void SendGroupRequest(GroupRequest groupRequest)
		{
			var dbContext = new ApplicationDbContext();

			dbContext.GroupRequests.Add(groupRequest);
			dbContext.SaveChanges();

		}

		public GroupRequest getGroupRequest(string userId, int? groupId)
		{
			var dbContext = new ApplicationDbContext();

			var request = (from gr in dbContext.GroupRequests
						   where gr.FromUserId == userId || gr.GroupId == groupId
						   select gr).FirstOrDefault();
			return request;
		}

		public void DeleteGroupRequest(GroupRequest groupRequest)
		{
			var dbContext = new ApplicationDbContext();

			var toDelete = (from gr in dbContext.GroupRequests
							where gr.GroupId == groupRequest.GroupId || gr.FromUserId == groupRequest.FromUserId
							select gr).FirstOrDefault();

			dbContext.GroupRequests.Remove(toDelete);
			dbContext.SaveChanges();
		}

		public void AcceptGroupRequest(GroupUsers groupUsers)
		{
			var dbContext = new ApplicationDbContext();
			dbContext.GroupUsers.Add(groupUsers);
			dbContext.SaveChanges();
		}
	}
}