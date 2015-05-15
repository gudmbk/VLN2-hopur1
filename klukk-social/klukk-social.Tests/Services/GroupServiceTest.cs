using Microsoft.VisualStudio.TestTools.UnitTesting;
using klukk_social.Models;
using klukk_social.Services;

namespace klukk_social.Tests.Services
{
    [TestClass]
    public class GroupServiceTest
    {
        private GroupService _service;

        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDataContext();
            var gu1 = new GroupUsers
            {
                Id = 1,
                GroupId = 1,
                UserId = "gummi"

            };
            mockDb.GroupUsers.Add(gu1);
            var gu2 = new GroupUsers
            {
                Id = 2,
                GroupId = 2,
                UserId = "dabs"

            };
            mockDb.GroupUsers.Add(gu2);
            var gu3 = new GroupUsers
            {
                Id = 3,
                GroupId = 1,
                UserId = "dabs"

            };
            mockDb.GroupUsers.Add(gu3);
            var gu4 = new GroupUsers
            {
                Id = 4,
                GroupId = 3,
                UserId = "gauja"

            };
            mockDb.GroupUsers.Add(gu4);
            var gu5 = new GroupUsers
            {
                Id = 5,
                GroupId = 1,
                UserId = "gauja"

            };
            mockDb.GroupUsers.Add(gu5);

            _service = new GroupService(mockDb);
        }

        [TestMethod]
        public void TestIfUserIsGroupMember()
        {
            // Arrange:
            const string user1 = "dabs";
            const string user2 = "gummi";
            const string user3 = "gauja";

            // Note: Should test if a given user is 
            // a member in a given group in our test data.
           
            // Act:
            var result1 = _service.IsUserMember(1, user1);
            var result2 = _service.IsUserMember(2, user1);
            var result3 = _service.IsUserMember(3, user1);
            var result4 = _service.IsUserMember(1, user2);
            var result5 = _service.IsUserMember(2, user2);
            var result6 = _service.IsUserMember(3, user2);
            var result7 = _service.IsUserMember(1, user3);
            var result8 = _service.IsUserMember(2, user3);
            var result9 = _service.IsUserMember(3, user3);

            // Assert:
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsFalse(result3);
            Assert.IsTrue(result4);
            Assert.IsFalse(result5);
            Assert.IsFalse(result6);
            Assert.IsTrue(result7);
            Assert.IsFalse(result8);
            Assert.IsTrue(result9);
        }

    }
}
