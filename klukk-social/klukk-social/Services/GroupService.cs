using klukk_social.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace klukk_social.Services
{
    public class GroupService
    {
        readonly IAppDataContext _dbContext;

        public GroupService(IAppDataContext context)
        {
            _dbContext = context ?? new ApplicationDbContext();
        }

        public void CreateGroup(Group group)
        {
            _dbContext.Groups.Add(group);
            _dbContext.SaveChanges();
        }

		public List<Group> GetAllGroups(string userId)
		{
            var groups = (from g in _dbContext.Groups
                          join gl in _dbContext.GroupUsers
							  on g.Id equals gl.GroupId
							  where gl.UserId == userId
							  select g).ToList();
			return groups;
		}

		public List<Group> GetAllParentGroups(string userId)
		{
            var groups = (from g in _dbContext.Groups
						  where g.UserId == userId
						  select g).ToList();
			return groups;
		}
		
		//FindById(groupId)
		public Group FindById(int ?groupId)
		{
            var group = (from g in _dbContext.Groups
						 where g.Id == groupId
						 select g).FirstOrDefault();
			return group;
		}
		
		public List<Post> GetAllGroupPostsToGroup(int? groupId)
        {
            var listi = (from p in _dbContext.Posts
			    where p.GroupId == groupId
			    orderby p.Date descending
						 select p).Include("Likes").ToList();
			
			foreach (var item in listi)
			{
                item.Comments = (from comment in _dbContext.Comments
			        where comment.PostId == item.Id
			        orderby comment.Date ascending
			        select comment).ToList();
			}
			return listi;
            
        }

		public List<Group> Search(string prefix)
		{
            var group = (from g in _dbContext.Groups
						where g.Name.Contains(prefix)
						orderby g.Name
						select g).ToList();
			return group;
		}

		public void SendGroupRequest(GroupRequest groupRequest)
		{
            _dbContext.GroupRequests.Add(groupRequest);
            _dbContext.SaveChanges();

		}

		public GroupRequest GetGroupRequest(string userId, int? groupId)
		{
            var request = (from gr in _dbContext.GroupRequests
						   where gr.FromUserId == userId || gr.GroupId == groupId
						   select gr).FirstOrDefault();
			return request;
		}

		public void DeleteGroupRequest(int requestId)
		{
            var toDelete = (from gr in _dbContext.GroupRequests
							where gr.Id == requestId
							select gr).FirstOrDefault();

            _dbContext.GroupRequests.Remove(toDelete);
            _dbContext.SaveChanges();
		}

		public void AcceptGroupRequest(GroupUsers groupUsers)
		{
            _dbContext.GroupUsers.Add(groupUsers);
            _dbContext.SaveChanges();
		}

		public bool IsUserMember(int groupId, string userId)
		{
            var request = (from user in _dbContext.GroupUsers
						   where user.UserId == userId && user.GroupId == groupId
						   select user).FirstOrDefault();
			return request != null;
		}

		public GroupUsers FindGroupUserById(string userId, int? groupId)
		{
		    if (!groupId.HasValue) return null;
            var request = (from user in _dbContext.GroupUsers
		        where user.UserId == userId
		        select user).FirstOrDefault();
		    return request;
		}

		public void LeaveGroup(string userId, int groupId)
		{
            var request = (from user in _dbContext.GroupUsers
						   where user.UserId == userId
						   select user).FirstOrDefault();

            _dbContext.GroupUsers.Remove(request);
            _dbContext.SaveChanges();
		}

		public void UpdateGroup(Group changedGroup)
		{
            var foundGroup = (from gr in _dbContext.Groups
								where gr.Id == changedGroup.Id
								select gr).FirstOrDefault();
		    if (foundGroup != null)
		    {
		        foundGroup.Description = changedGroup.Description;
		        foundGroup.Name = changedGroup.Name;
		        foundGroup.ProfilePic = changedGroup.ProfilePic;
		        foundGroup.OpenGroup = changedGroup.OpenGroup;
		    }
		    _dbContext.SaveChanges();
		}
        
		public int FindGroupId(int postId)
		{
			var group = (from p in _dbContext.Posts
						 where p.Id == postId
						 select p).FirstOrDefault();
		    // ReSharper disable once PossibleNullReferenceException
		    return group.GroupId.Value;
		}

		public void DeleteGroupRequest(int p, string userId)
		{
            var request = (from r in _dbContext.GroupRequests
									where r.GroupId == p && r.FromUserId == userId
									select r).FirstOrDefault();
			_dbContext.GroupRequests.Remove(request);
			_dbContext.SaveChanges();
		}

		public List<GroupRequest> GetAllRequests(string userId)
		{
			return (from grReq in _dbContext.GroupRequests
						join grp in _dbContext.Groups
						on grReq.GroupId equals grp.Id
						where grp.UserId == userId
						//select grReq).Include("Groups").ToList();
						select grReq).ToList();
		}

		public string GetRequestUserId(int requestId)
		{
			return (from reqId in _dbContext.GroupRequests
					where reqId.Id == requestId
					select reqId.FromUserId).FirstOrDefault();
		}

		public int GetGroupRequestGroupId(int? requestId)
		{
			return (from reqId in _dbContext.GroupRequests
					where reqId.Id == requestId
					select reqId.GroupId).FirstOrDefault();
		}
	}
}