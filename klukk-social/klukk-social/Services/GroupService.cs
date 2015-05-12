using klukk_social.Models;

namespace klukk_social.Services
{
    public class GroupService
    {
        public void CreateGroup(Group group)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Groups.Add(group);
                dbContext.SaveChanges();
            }
        }

		public List<Group> GetAllGroups(string userID)
		{
			using (var dbContext = new ApplicationDbContext())
			{
				var groups = (from g in dbContext.Groups
							  join gl in dbContext.GroupUsers
							  on g.Id equals gl.GroupId
							  where gl.UserId == userID
							  select g).ToList();
				return groups;
			}
		}
    }
}