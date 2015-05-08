using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text;

namespace klukk_social.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public int ParentId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ProfilePic { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreationDate { get; set; }
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
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUsers> GroupUsers { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}