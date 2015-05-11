using System.Collections.Generic;
using System.Linq;
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
    }
}