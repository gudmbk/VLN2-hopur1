using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace klukk_social.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public string ParentId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ProfilePic { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<ReportItem> Reports { get; set; }

        public User()
        {
            ProfilePic = "\\Content\\Images\\EmptyProfilePicture.gif";
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string GetFullName()
        {
            return FirstName + " " + MiddleName + " " + LastName;
        }
		public void SetProfilePic(string newProfileUrl)
		{
			ProfilePic = newProfileUrl;
		}
	}

    public interface IAppDataContext
    {
        IDbSet<User> Users { get; set; }
        IDbSet<Post> Posts { get; set; }
        IDbSet<Settings> Settings { get; set; }
        IDbSet<Notification> Notifications { get; set; }
        IDbSet<Likes> Likes { get; set; }
        IDbSet<Group> Groups { get; set; }
        IDbSet<GroupUsers> GroupUsers { get; set; }
        IDbSet<GroupRequest> GroupRequests { get; set; }
        IDbSet<Friendship> Friendships { get; set; }
        IDbSet<FriendRequest> FriendRequests { get; set; }
        IDbSet<Comment> Comments { get; set; }
        IDbSet<CommentLikes> CommentLikes { get; set; }
        IDbSet<ReportItem> ReportItem { get; set; }
        int SaveChanges();
    }
    public class ApplicationDbContext : IdentityDbContext<User>, IAppDataContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = true;
        }
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Settings> Settings { get; set; }
        public IDbSet<Notification> Notifications { get; set; }
        public IDbSet<Likes> Likes { get; set; }
        public IDbSet<Group> Groups { get; set; }
        public IDbSet<GroupUsers> GroupUsers { get; set; }
        public IDbSet<GroupRequest> GroupRequests { get; set; }
        public IDbSet<Friendship> Friendships { get; set; }
        public IDbSet<FriendRequest> FriendRequests { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<CommentLikes> CommentLikes { get; set; }
        public IDbSet<ReportItem> ReportItem { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
		
    }
}