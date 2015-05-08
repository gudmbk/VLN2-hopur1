using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;
using klukk_social.Models;

namespace klukk_social.Services
{
    public class PostService
    {
        public List<Post> GetAllPostsToUser(string userId)
        {
            List<Post> listi = new List<Post>();
            using (var dbContext = new ApplicationDbContext())
            {
                listi =
                    (from p in dbContext.posts
                        where p.ToUserId == userId
                        orderby p.Date descending 
                        select p).ToList();

            }
            return listi;
        }

        public void AddPost(Post post)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.posts.Add(post);
                dbContext.SaveChanges();
            }
        }
    }
}