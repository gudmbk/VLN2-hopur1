using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using klukk_social.Models;

namespace klukk_social.Services
{
    public class PostService
    {
        readonly IAppDataContext _dbContext;

        public PostService(IAppDataContext context)
        {
            _dbContext = context ?? new ApplicationDbContext();
        }

        public List<Post> GetAllPostsToUser(string userId)
        {
			var listi = (from p in _dbContext.Posts
                where p.ToUserId == userId
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
 
        public void AddPost(Post post)
        {
			_dbContext.Posts.Add(post);
			_dbContext.SaveChanges();
        }

        public void AddComment(Comment comment)
        {
			_dbContext.Comments.Add(comment);
                _dbContext.SaveChanges();

        }
		public List<Post> GetAllChildrenPosts(string parentId)
		{
			// Load all posts my children have made
			var authoredPosts = from post in _dbContext.Posts
								join postAuthor in _dbContext.Users on post.FromUserId equals postAuthor.Id
								where postAuthor.ParentId == parentId
								select post;

			// Load all posts my children have commented on
			var commentedPosts = from post in _dbContext.Posts
									join comment in _dbContext.Comments on post.Id equals comment.PostId
									join commentAuthor in _dbContext.Users on comment.UserId equals commentAuthor.Id
									where commentAuthor.ParentId == parentId
									select post;

			var allPostsAndComments = (from post in authoredPosts.Union(commentedPosts)
								orderby post.Date descending
                                        select post).Include("Likes").ToList();

			foreach (var post in allPostsAndComments)
			{
				post.Comments = (from comment in _dbContext.Comments
									where comment.PostId == post.Id
									orderby comment.Date ascending
									select comment).ToList();
			}
            return allPostsAndComments;
			
		}

        public List<Post> GetAllPostForUserFeed(string userId)
        {
            var list = (from post in _dbContext.Posts
                join friend in _dbContext.Friendships on post.FromUserId equals friend.FromUserId
                where friend.ToUserId == userId
                orderby post.Date descending
                select post).Include("Likes").ToList();

            foreach (var post in list)
            {
                post.Comments = (from comment in _dbContext.Comments
                    where comment.PostId == post.Id
                    orderby comment.Date ascending
                    select comment).ToList();
            }

            return list; 
        }

        public void AddLike(Likes like)
        {
			var post = _dbContext.Posts.Single(p => p.Id == like.PostId);
			if (post.Likes == null)
			{
			    post.Likes = new List<Likes>();
			}
			post.Likes.Add(like);
			_dbContext.SaveChanges();
        }

		public Post GetPostById(int postId)
		{
			var item = (from p in _dbContext.Posts
						where p.Id == postId
						select p).Include("Likes").FirstOrDefault();
		    if (item != null)
		    {
		        item.Comments = (from c in _dbContext.Comments
		            where c.PostId == postId
		            select c).Include("Likes").ToList();
		    }
            return item;
		}
		public string GetToUserIdPostId(int postId)
		{
			var item = (from u in _dbContext.Posts
						where u.Id == postId
						select u.ToUserId).FirstOrDefault();
			return item;

		}

        public void RemovePost(int postToDelete)
        {
			var itemToDelete = _dbContext.Posts.Single(p => p.Id == postToDelete);
			
			var commentList = (from cmnt in _dbContext.Comments
			    where cmnt.PostId == postToDelete
			    select cmnt).ToList();
			foreach (var item in commentList)
			{
			    _dbContext.Comments.Remove(item);
			}
			_dbContext.Posts.Remove(itemToDelete);
			_dbContext.SaveChanges();
        }

        public void RemoveComment(int commentId)
        {
            var itemToDelete = _dbContext.Comments.Single(c => c.Id == commentId);
            _dbContext.Comments.Remove(itemToDelete);
            _dbContext.SaveChanges();
        }

        public Comment GetCommentById(int commentId)
        {
            var item = (from c in _dbContext.Comments
				where c.Id == commentId
				select c).Include("Likes").FirstOrDefault();
				return item;
        }

        public void AddCommentLike(CommentLikes liked)
        {
            var comment = _dbContext.Comments.Single(c => c.Id == liked.CommentId);
            if (comment.Likes == null)
            {
                comment.Likes = new List<CommentLikes>();
            }
            comment.Likes.Add(liked);
            _dbContext.SaveChanges();
        }

        public void EditComment(int commentId, string body)
        {
			var itemToChange = _dbContext.Comments.Single(c => c.Id == commentId);
            itemToChange.Body = body;
            _dbContext.SaveChanges();
        }

        public void EditPost(int postId, string text, string htmlText)
        {
            var itemToChange = _dbContext.Posts.Single(p => p.Id == postId);
            itemToChange.Text = text;
            itemToChange.HtmlText = htmlText;
            _dbContext.SaveChanges();
        }

        public void AddReportPost(int itemId, string reporterId)
        {
            Post postReported = _dbContext.Posts.Single(p => p.Id == itemId);
            User poster = _dbContext.Users.Single(u => u.Id == postReported.FromUserId);
            User parent = _dbContext.Users.Single(u => u.Id == poster.ParentId);
            ReportItem report = new ReportItem();
            report.Id = 0;
            report.IsPost = true;
            report.ReportedById = reporterId;
            report.ParentId = parent.Id;
            report.PostItem = postReported;
            report.CommentItem = null;
            report.Date = DateTime.Now;

            parent.Reports.Add(report);
            _dbContext.SaveChanges();
        }

        public void AddReportComment(int itemId, string reporterId)
        {
            Comment commentReported = _dbContext.Comments.Single(c => c.Id == itemId);
            User poster = _dbContext.Users.Single(u => u.Id == commentReported.UserId);
            User parent = _dbContext.Users.Single(u => u.Id == poster.ParentId);

            ReportItem report = new ReportItem();
            report.Id = 0;
            report.IsPost = false;
            report.ReportedById = reporterId;
            report.ParentId = parent.Id;
            report.PostItem = null;
            report.CommentItem = commentReported;
            report.Date = DateTime.Now;

            parent.Reports.Add(report);
            _dbContext.SaveChanges();
        }

		public bool OnUserWall(int postId)
		{
			return GetPostById(postId).GroupId == null;
		}

        public void DeleteReport(int reportId)
        {
            ReportItem itemToRemove = _dbContext.ReportItem.Single(r => r.Id == reportId);
            _dbContext.ReportItem.Remove(itemToRemove);
            _dbContext.SaveChanges();
        }

        public void DeleteReportedItem(int reportId)
        {
            ReportItem itemToRemove = _dbContext.ReportItem.Single(r => r.Id == reportId);

            if (itemToRemove.IsPost)
            {
                RemovePost(itemToRemove.PostItem.Id);
            }
            else
            {
                RemoveComment(itemToRemove.CommentItem.Id);
            }
            _dbContext.ReportItem.Remove(itemToRemove);
            _dbContext.SaveChanges();
        }
    }
}