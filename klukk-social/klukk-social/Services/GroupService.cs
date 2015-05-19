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

		/// <summary>
		/// Adds a Group to the database
		/// </summary>
		/// <param name="group"></param>
        public void CreateGroup(Group group)
        {
            _dbContext.Groups.Add(group);
            _dbContext.SaveChanges();
        }
		
		/// <summary>
		/// Fetches all groups for the given userId
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public List<Group> GetAllGroups(string userId)
		{
            var groups = (from g in _dbContext.Groups
                          join gl in _dbContext.GroupUsers
							  on g.Id equals gl.GroupId
							  where gl.UserId == userId
							  select g).ToList();
			return groups;
		}

		/// <summary>
		/// Returnes all groups owned by userId(Parent)
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public List<Group> GetAllParentGroups(string userId)
		{
            var groups = (from g in _dbContext.Groups
						  where g.UserId == userId
						  select g).ToList();
			return groups;
		}
		
		/// <summary>
		/// Finds a Group by its groupId
		/// </summary>
		/// <param name="groupId"></param>
		/// <returns></returns>
		public Group FindById(int ?groupId)
		{
            var group = (from g in _dbContext.Groups
						 where g.Id == groupId
						 select g).FirstOrDefault();
			return group;
		}
		
		/// <summary>
		/// Returns a list of Posts belonging to a group
		/// </summary>
		/// <param name="groupId"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Adds a group request to the database
		/// </summary>
		/// <param name="groupRequest"></param>
		public void SendGroupRequest(GroupRequest groupRequest)
		{
            _dbContext.GroupRequests.Add(groupRequest);
            _dbContext.SaveChanges();
		}

		/// <summary>
		/// Fetches a group request form the database
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="groupId"></param>
		/// <returns></returns>
		public GroupRequest GetGroupRequest(string userId, int? groupId)
		{
			var request = (from gr in _dbContext.GroupRequests
						   where gr.FromUserId == userId && gr.GroupId == groupId
						   select gr).FirstOrDefault();
			return request;
		}

		/// <summary>
		/// Deletes a group request
		/// </summary>
		/// <param name="requestId"></param>
		public void DeleteGroupRequest(int requestId)
		{
            var toDelete = (from gr in _dbContext.GroupRequests
							where gr.Id == requestId
							select gr).FirstOrDefault();

            _dbContext.GroupRequests.Remove(toDelete);
            _dbContext.SaveChanges();
		}

		/// <summary>
		/// Adds a user to the GroupUsers table
		/// </summary>
		/// <param name="groupUsers"></param>
		public void AcceptGroupRequest(GroupUsers groupUsers)
		{
            _dbContext.GroupUsers.Add(groupUsers);
            _dbContext.SaveChanges();
		}

		/// <summary>
		/// Checks whether or not a user is a member of a specific group
		/// </summary>
		/// <param name="groupId"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public bool IsUserMember(int groupId, string userId)
		{
            var request = (from user in _dbContext.GroupUsers
						   where user.UserId == userId && user.GroupId == groupId
						   select user).FirstOrDefault();
			return request != null;
		}

		/// <summary>
		/// Deletes the refenance to the user from the GroupsUsers table
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="groupId"></param>
		public void LeaveGroup(string userId, int groupId)
		{
            var request = (from user in _dbContext.GroupUsers
						   where user.UserId == userId && user.GroupId == groupId
						   select user).FirstOrDefault();

            _dbContext.GroupUsers.Remove(request);
            _dbContext.SaveChanges();
		}

		/// <summary>
		/// Replaces current group data by input data
		/// </summary>
		/// <param name="changedGroup"></param>
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
        
		/// <summary>
		/// Returns the groupId the postId belongs to
		/// </summary>
		/// <param name="postId"></param>
		/// <returns></returns>
		public int FindGroupId(int postId)
		{
			var group = (from p in _dbContext.Posts
						 where p.Id == postId
						 select p).FirstOrDefault();
		    // ReSharper disable once PossibleNullReferenceException
		    // ReSharper disable once PossibleInvalidOperationException
		    return group.GroupId.Value;
		}

		/// <summary>
		/// Deletes group request from table
		/// </summary>
		/// <param name="p"></param>
		/// <param name="userId"></param>
		public void DeleteGroupRequest(int p, string userId)
		{
            var request = (from r in _dbContext.GroupRequests
									where r.GroupId == p && r.FromUserId == userId
									select r).FirstOrDefault();
			_dbContext.GroupRequests.Remove(request);
			_dbContext.SaveChanges();
		}

		/// <summary>
		/// Returns all requests made my a user
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public List<GroupRequest> GetAllRequests(string userId)
		{
			return (from grReq in _dbContext.GroupRequests
						join grp in _dbContext.Groups
						on grReq.GroupId equals grp.Id
						where grp.UserId == userId
						//select grReq).Include("Groups").ToList();
						select grReq).ToList();
		}

		/// <summary>
		/// Returns the userId of the user that made the group request
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public string GetRequestUserId(int requestId)
		{
			return (from reqId in _dbContext.GroupRequests
					where reqId.Id == requestId
					select reqId.FromUserId).FirstOrDefault();
		}

		/// <summary>
		/// Returns the group-request-Id by the request-Id
		/// </summary>
		/// <param name="requestId"></param>
		/// <returns></returns>
		public int GetGroupRequestGroupId(int? requestId)
		{
			return (from reqId in _dbContext.GroupRequests
					where reqId.Id == requestId
					select reqId.GroupId).FirstOrDefault();
		}

		/// <summary>
		/// Creates a list of "GroupWithMembership" which stores every group a user is in and wether he is a member of that group
		/// </summary>
		/// <param name="prefix"></param>
		/// <param name="currUser"></param>
		/// <returns></returns>
		public List<GroupWithMembership> SearchGroupsWithMemership(string prefix, string currUser)
		{
			List<GroupWithMembership> returnlist = new List<GroupWithMembership>();

			var groups = (from g in _dbContext.Groups
						where g.Name.Contains(prefix)
						orderby g.Name
						select g).ToList();

			foreach (var group1 in groups)
			{
				var inGroup = (from groupUser in _dbContext.GroupUsers
								  where groupUser.GroupId == group1.Id && groupUser.UserId == currUser
								  select groupUser).FirstOrDefault();

				GroupWithMembership gwm = new GroupWithMembership();
				gwm.IsMember = inGroup != null;
				gwm.Group = group1;
				if (!gwm.IsMember)
				{
					var request = (from r in _dbContext.GroupRequests
								   where r.FromUserId == currUser && r.GroupId == group1.Id
								   select r).FirstOrDefault();
					gwm.AskedForAccess = request != null;
				}
				returnlist.Add(gwm);
			}

			return returnlist;
		}
	}
}