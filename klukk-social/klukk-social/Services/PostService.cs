using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls.Expressions;
using klukk_social.Models;

namespace klukk_social.Services
{
    public class PostService
    {
		UserSerice us = new UserSerice();

        public List<Post> GetAllPostsToUser(string userId)
        {
            List<Post> listi = new List<Post>();
            using (var dbContext = new ApplicationDbContext())
            {
                listi = (from p in dbContext.Posts
                    where p.ToUserId == userId
                    orderby p.Date descending
                    select p).ToList();
                foreach (var item in listi)
                {
                    item.Comments = (from comment in dbContext.Comments
                        where comment.PostId == item.Id
                        orderby comment.Date ascending
                        select comment).ToList();
                }
            }
            return listi;
        }
 
        public void AddPost(Post post)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Posts.Add(post);
                dbContext.SaveChanges();
            }
        }

        public void AddComment(Comment comment)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Comments.Add(comment);
                dbContext.SaveChanges();
            }
        }
		public List<Post> GetAllChildrenPosts(string parentId)
		{
			using (var dbContext = new ApplicationDbContext())
			
			return (from post in dbContext.Posts
				join user in dbContext.Users on post.FromUserId equals user.Id
				where user.ParentId == parentId
				orderby post.Date descending
				select post).ToList();
		}
    }
}