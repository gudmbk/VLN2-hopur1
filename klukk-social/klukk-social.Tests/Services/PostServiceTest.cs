using Microsoft.VisualStudio.TestTools.UnitTesting;
using klukk_social.Models;
using klukk_social.Services;

namespace klukk_social.Tests.Services
{
    [TestClass]
    public class PostServiceTest
    {
        private PostService _service;

        [TestInitialize]
        public void Initialize()
        {

            var mockDb = new MockDataContext();

            // Users
            var u1 = new User { Id = "dabs", ParentId = "gummi" };
            mockDb.Users.Add(u1);
            var u2 = new User { Id = "gummi", ParentId = null };
            mockDb.Users.Add(u2);
            var u3 = new User { Id = "tommi", ParentId = "gummi" };
            mockDb.Users.Add(u3);

            // Friendships
            var f1 = new Friendship { Id = 1, FromUserId = "dabs", ToUserId = "tommi" };
            mockDb.Friendships.Add(f1);
            var f2 = new Friendship { Id = 2, FromUserId = "tommi", ToUserId = "dabs" };
            mockDb.Friendships.Add(f2);
            var f3 = new Friendship { Id = 1, FromUserId = "gauja", ToUserId = "tommi" };
            mockDb.Friendships.Add(f3);
            var f4 = new Friendship { Id = 1, FromUserId = "tommi", ToUserId = "gauja" };
            mockDb.Friendships.Add(f4);

            // Posts
            var p1 = new Post { Id = 1, FromUserId = "dabs", ToUserId = "dabs" };
            mockDb.Posts.Add(p1);
            var p2 = new Post { Id = 2, FromUserId = "dabs", ToUserId = "tommi" };
            mockDb.Posts.Add(p2);
            var p3 = new Post { Id = 3, FromUserId = "tommi", ToUserId = "dabs" };
            mockDb.Posts.Add(p3);

            // Comments
            var c1 = new Comment { Id = 1, PostId = 1, Body = "Test Text String" };
            mockDb.Comments.Add(c1);
            var c2 = new Comment { Id = 2, PostId = 1, Body = "Another text" };
            mockDb.Comments.Add(c2);
            var c3 = new Comment { Id = 2, PostId = 3, Body = "Another text" };
            mockDb.Comments.Add(c3);

            _service = new PostService(mockDb);
        }

        [TestMethod]
        public void TestEditComment()
        {
            // Arrange:
            const string body = "Text New Comment";

            // Act:
            _service.EditComment(1, body);
            var comment = _service.GetCommentById(1);
            
            // Assert:
            Assert.AreEqual(comment.Body, body);

        }
        [TestMethod]
        public void TestForGetPostsToUser()
        {
            // Arrange:
            const string user1 = "dabs";
            const string user2 = "tommi";
            const string user3 = "gummi";

            // Act:
            var posts = _service.GetAllPostsToUser(user1);
            var posts2 = _service.GetAllPostsToUser(user2);
            var posts3 = _service.GetAllPostsToUser(user3);

            // Assert:
            Assert.AreEqual(2, posts.Count);
            Assert.AreEqual(1, posts2.Count);
            Assert.AreNotEqual(1, posts3.Count);
        }

        [TestMethod]
        public void TestForGetAllChildenPosts()
        {
            // Arrange:
            const string user1 = "gummi";
            const string user2 = "dabs";
            const string user3 = "noone";

            // Note: no user with the username noone has an entry
            // in our test data.

            // Act:
            var posts = _service.GetAllChildrenPosts(user1);
            var posts2 = _service.GetAllChildrenPosts(user2);
            var posts3 = _service.GetAllChildrenPosts(user3);


            // Assert:
            Assert.AreEqual(3, posts.Count);
            Assert.AreEqual(0, posts2.Count);
            Assert.AreEqual(0, posts3.Count);
        }
        [TestMethod]
        public void TestForGetAllPostForUserFeed()
        {
            // Arrange:
            const string user1 = "dabs";
            const string user2 = "tommi";
            const string user3 = "gummi";

            // Note: User does not get his own posts in his feed
            // Note: This is not getting posts for activity feed og parents so gummi should get no posts

            // Act:
            var posts = _service.GetAllPostForUserFeed(user1);
            var posts2 = _service.GetAllPostForUserFeed(user2);
            var posts3 = _service.GetAllPostForUserFeed(user3);

            // Assert:
            Assert.AreEqual(1, posts.Count); 
            Assert.AreEqual(2, posts2.Count);
            Assert.AreNotEqual(1, posts3.Count);
        }
        [TestMethod]
        public void TestForGetPostByIdAndCommentCount()
        {
            // Arrange:
            const int post = 1;
            const int post2 = 2;
            const int post3 = 3;

            // Act:
            var posts = _service.GetPostById(post);
            var posts2 = _service.GetPostById(post2);
            var posts3 = _service.GetPostById(post3);

            // Assert:
            Assert.AreEqual(1, posts.Id);
            Assert.AreEqual(2, posts.Comments.Count);
            Assert.AreEqual(post2, posts2.Id);
            Assert.AreEqual(0, posts2.Comments.Count);
            Assert.AreEqual(post3, posts3.Id);
            Assert.AreEqual(1, posts3.Comments.Count);
        }

    }
}
