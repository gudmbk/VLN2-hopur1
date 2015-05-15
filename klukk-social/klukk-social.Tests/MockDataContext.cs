using System.Data.Entity;
using klukk.Tests;
using klukk_social.Models;


namespace klukk_social.Tests
{
    class MockDataContext : IAppDataContext
    {
        /// <summary>
        /// Sets up the fake database.
        /// </summary>
        public MockDataContext()
        {
            // We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
            Users = new InMemoryDbSet<User>();
            Posts = new InMemoryDbSet<Post>();
            Notifications = new InMemoryDbSet<Notification>();
            Likes = new InMemoryDbSet<Likes>();
            Groups = new InMemoryDbSet<Group>();
            GroupUsers = new InMemoryDbSet<GroupUsers>();
            GroupRequests = new InMemoryDbSet<GroupRequest>();
            Friendships = new InMemoryDbSet<Friendship>();
            FriendRequests = new InMemoryDbSet<FriendRequest>();
            Comments = new InMemoryDbSet<Comment>();
            CommentLikes = new InMemoryDbSet<CommentLikes>();
            ReportItem = new InMemoryDbSet<ReportItem>();
        }

        public IDbSet<User> Users { get; set; }
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
        // TODO: bætið við fleiri færslum hér
        // eftir því sem þeim fjölgar í AppDataContext klasanum ykkar!

        public int SaveChanges()
        {
            // Pretend that each entity gets a database id when we hit save.
            int changes = 0;

            return changes;
        }

        public void Dispose()
        {
            // Do nothing!
        }
    }
}
