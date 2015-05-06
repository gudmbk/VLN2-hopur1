using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using klukk_social.Models;

namespace klukk_social.DAL
{
    public class KlukkContext : DbContext
    {
        public KlukkContext()
            : base("KlukkContext")
        {
            
        }

        public DbSet<User> user { get; set; }
        public DbSet<Post> post { get; set; }
        public DbSet<Settings> settings { get; set; }
        public DbSet<Notification> notification { get; set; }
        public DbSet<Likes> likes { get; set; }
        public DbSet<Group> group { get; set; }
        public DbSet<GroupUsers> groupUsers { get; set; }
        public DbSet<Friendship> friendships { get; set; }
        public DbSet<FriendRequest> friendRequests { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }


}