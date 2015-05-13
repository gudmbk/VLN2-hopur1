﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using klukk_social.Models;

namespace klukk_social.Services
{
    public class PostService
    {

        public List<Post> GetAllPostsToUser(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var listi = (from p in dbContext.Posts
                    where p.ToUserId == userId
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

				var allPostsAndComments = (from post in authoredPosts.Union(commentedPosts)
									orderby post.Date descending
                                           select post).Include("Likes").ToList();

				foreach (var post in allPostsAndComments)
				{
					post.Comments = (from comment in dbContext.Comments
									 where comment.PostId == post.Id
									 orderby comment.Date ascending
									 select comment).ToList();
				}
                return allPostsAndComments;
			}
		}

        public List<Post> GetAllPostForUserFeed(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var list = (from post in dbContext.Posts
                    join friend in dbContext.Friendships on post.FromUserId equals friend.FromUserId
                    where friend.ToUserId == userId
                    orderby post.Date descending
                    select post).Include("Likes").ToList();

                foreach (var post in list)
                {
                    post.Comments = (from comment in dbContext.Comments
                        where comment.PostId == post.Id
                        orderby comment.Date ascending
                        select comment).ToList();
                }

                return list;
            }
        }

        public void AddLike(Likes like)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var post = dbContext.Posts.Single(p => p.Id == like.PostId);
                if (post.Likes == null)
                {
                    post.Likes = new List<Likes>();
                }
                post.Likes.Add(like);
                dbContext.SaveChanges();
            }
        }

        public Post GetPostById(int postId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var item = (from p in dbContext.Posts
                    where p.Id == postId
                    select p).Include("Likes").FirstOrDefault();
                return item;
            }
        }

        public void RemovePost(int postToDelete)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var itemToDelete = dbContext.Posts.Single(p => p.Id == postToDelete);

                var commentList = (from cmnt in dbContext.Comments
                    where cmnt.PostId == postToDelete
                    select cmnt).ToList();
                foreach (var item in commentList)
                {
                    dbContext.Comments.Remove(item);
                }
                dbContext.Posts.Remove(itemToDelete);
                dbContext.SaveChanges();
            }
        }

        public void RemoveComment(int commentId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var itemToDelete = dbContext.Comments.Single(c => c.Id == commentId);
                dbContext.Comments.Remove(itemToDelete);
                dbContext.SaveChanges();
            }
        }

        public Comment GetCommentById(int commentId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var item = dbContext.Comments.Single(c => c.Id == commentId);
                return item;
            }
        }

        public void AddCommentLike(CommentLikes liked)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var comment = dbContext.Comments.Single(c => c.Id == liked.CommentId);
                if (comment.Likes == null)
                {
                    comment.Likes = new List<CommentLikes>();
                }
                comment.Likes.Add(liked);
                dbContext.SaveChanges();
            }
        }
    }
}