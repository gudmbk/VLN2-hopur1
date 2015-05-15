using Microsoft.VisualStudio.TestTools.UnitTesting;
using klukk_social.Models;
using klukk_social.Services;

namespace klukk_social.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private UserService _service;

        [TestInitialize]
        public void Initialize()
        {
            // Set up our mock database. In this case,
            // we only have to worry about one table
            // with 3 records:
            var mockDb = new MockDataContext();
            var f1 = new Friendship
            {
                Id = 1,
                FromUserId = "dabs",
                ToUserId = "gummi"
            };
            mockDb.Friendships.Add(f1);

            var f2 = new Friendship
            {
                Id = 2,
                FromUserId = "gummi",
                ToUserId = "dabs"
            };
            mockDb.Friendships.Add(f2);
            var f3 = new Friendship
            {
                Id = 3,
                FromUserId = "gummi",
                ToUserId = "tommi"
                
            };
            mockDb.Friendships.Add(f3);
            var f4 = new Friendship
            {
                Id = 4,
                FromUserId = "tommi",
                ToUserId = "gummi"

            };
            mockDb.Friendships.Add(f4);
            var f5 = new Friendship
            {
                Id = 5,
                FromUserId = "dabs",
                ToUserId = "gauja"

            };
            mockDb.Friendships.Add(f5);
            var f6 = new Friendship
            {
                Id = 6,
                FromUserId = "gauja",
                ToUserId = "dabs"

            };
            mockDb.Friendships.Add(f6);

            var u1 = new User
            {
                Id = "gauja",
                FirstName = "Gauja",
                LastName = "Sturludóttir",
                FullName = "Gauja Sturludóttir",
                ParentId = null
            };
            mockDb.Users.Add(u1);
            var u2 = new User
            {
                Id = "briet",
                FirstName = "Briet",
                LastName = "Sævarsdóttir",
                FullName = "Briet Sævarsdóttir",
                ParentId = "gauja"
            };
            mockDb.Users.Add(u2);
            var u3 = new User
            {
                Id = "patti",
                FirstName = "Patti",
                LastName = "Ægisson",
                FullName = "Patti Ægisson",
                ParentId = "gauja"
            };
            mockDb.Users.Add(u3);
            var u4 = new User
            {
                Id = "alex",
                FirstName = "Alex",
                LastName = "Ægisson",
                FullName = "Alex Ægisson",
                ParentId = "gauja"
            };
            mockDb.Users.Add(u4);
            var u5 = new User
            {
                Id = "childless",
                FirstName = "Childless",
                LastName = "Noson",
                FullName = "Childless Noson",
                ParentId = null
            };
            mockDb.Users.Add(u5);
            var u6 = new User
            {
                Id = "gummi",
                FirstName = "Gummi",
                LastName = "Kristinsson",
                FullName = "Gummi Kristinsson",
                ParentId = null
            };
            mockDb.Users.Add(u6);
            var u7 = new User
            {
                Id = "tommi",
                FirstName = "Tommi",
                LastName = "Stefánsson",
                FullName = "Tommi Stefánsson",

                ParentId = null
            };
            mockDb.Users.Add(u7);

            // Note: you only have to add data necessary for this
            // particular service (FriendService) to run properly.
            // There will be more tables in your DB, but you only
            // need to provide the data for the methods you are
            // actually testing here.

            _service = new UserService(mockDb);
        }

        [TestMethod]
        public void TestGetAllFriendsForDabs() 
        {
            // Arrange:
            const string userName = "dabs";

            // Act:
            var friends = _service.GetFriends(userName);

            // Assert:
            Assert.AreEqual(2, friends.Count);
            foreach (var item in friends)
            {
                Assert.AreNotEqual(item, "dabs");
            }
        }

        [TestMethod]
        public void TestGetForUserWithNoFriends()
        {
            // Arrange:
            const string userWithNoFriends = "loner";
            // Note: no user with this username has an entry
            // in our test data.

            // Act:
            var friends = _service.GetFriends(userWithNoFriends);

            // Assert:
            Assert.AreEqual(0, friends.Count);
        }

        [TestMethod]
        public void TestGetForUserWithThreeChildren()
        {
            // Arrange:
            const string userWithNoFriends = "gauja";
            // Note: no user with this username has an entry
            // in our test data.

            // Act:
            var children = _service.GetAllChildren(userWithNoFriends);

            // Assert:
            Assert.AreEqual(3, children.Count);
        }

        [TestMethod]
        public void TestGetForUserWithNoChildren()
        {
            // Arrange:
            const string userWithNoChildren = "childless";
            // Note: this user exists but has no children
            // in our test data.

            // Act:
            var children = _service.GetAllChildren(userWithNoChildren);

            // Assert:
            Assert.AreEqual(0, children.Count);
        }
        [TestMethod]
        public void TestSearchForLastName()
        {
            // Arrange:
            const string lastName = "Ægisson";
            // Note: Here we have two children with
            // the same last name in our test data.

            // Act:
            var users = _service.Search(lastName);

            // Assert:
            Assert.AreEqual(2, users.Count);
        }
        [TestMethod]
        public void TestSearchForParentName()
        {
            // Arrange:
            const string lastName = "Sturludóttir";
            // Note: this user exists but is a parent 
            // so should not show in search.

            // Act:
            var users = _service.Search(lastName);

            // Assert:
            Assert.AreEqual(0, users.Count);
        }
    }
}
