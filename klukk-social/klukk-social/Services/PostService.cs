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
			
			List<Post> allPostsAndComments = new List<Post>();

			using (var dbContext = new ApplicationDbContext())
			{
				// Load all posts my children have made
				var authoredPosts = from post in dbContext.Posts
									join postAuthor in dbContext.Users on post.FromUserId equals postAuthor.Id
									where postAuthor.ParentId == parentId
									select post;

				// Load all posts my children have commented on
				var commentedPosts = from post in dbContext.Posts
									 join comment in dbContext.Comments on post.Id equals comment.PostId
									 join commentAuthor in dbContext.Users on comment.UserId equals commentAuthor.Id
									 where commentAuthor.ParentId == parentId
									 select post;

				allPostsAndComments = (from post in authoredPosts.Union(commentedPosts)
									orderby post.Date descending
									select post).ToList();


									  

				foreach (var post in allPostsAndComments)
				{
					post.Comments = (from comment in dbContext.Comments
									 where comment.PostId == post.Id
									 orderby comment.Date ascending
									 select comment).ToList();
				}
			}
			return allPostsAndComments;
		}
    }
}