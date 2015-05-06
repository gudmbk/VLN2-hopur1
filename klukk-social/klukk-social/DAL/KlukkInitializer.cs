using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using klukk_social.Models;

namespace klukk_social.DAL
{
    public class KlukkInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<KlukkContext>
    {
        protected override void Seed(KlukkContext context)
        {
            List<User> users = new List<User>();
            List<Friendship> friendships = new List<Friendship>();
            List<FriendRequest> friendRequests = new List<FriendRequest>();
            List<Group> groups = new List<Group>();
            List<GroupUsers> groupUsers = new List<GroupUsers>();
            List<Likes> likes = new List<Likes>();
            List<Post> posts = new List<Post>();
            List<Notification> notifications = new List<Notification>();
            List<Settings> settings = new List<Settings>();

            context.SaveChanges();
        }
    }
}