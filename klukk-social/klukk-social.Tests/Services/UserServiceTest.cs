using Microsoft.VisualStudio.TestTools.UnitTesting;
using klukk_social.Models;
using klukk_social.Services;

namespace klukk_social.Tests.Services
{
    [TestClass]
    public class FriendServiceTest
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
                ToUserId = "nonni"
            };
            mockDb.Friendships.Add(f1);

            var f2 = new Friendship
            {
                Id = 2,
                FromUserId = "nonni",
                ToUserId = "dabs"
            };
            mockDb.Friendships.Add(f2);
            var f3 = new Friendship
            {
                Id = 3,
                FromUserId = "nonni",
                ToUserId = "gunna"
                
            };
            mockDb.Friendships.Add(f3);
            var f4 = new Friendship
            {
                Id = 4,
                FromUserId = "gunna",
                ToUserId = "nonni"

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
    }
}
