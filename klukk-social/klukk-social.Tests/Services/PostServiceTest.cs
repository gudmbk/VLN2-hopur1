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
            // Set up our mock database. In this case,
            // we only have to worry about one table
            // with 3 records:
            var mockDb = new MockDataContext();
            var f1 = new Comment
            {
                Id = 1,
                PostId = 1,
                Body = "Test Text String",

            };
            mockDb.Comments.Add(f1);

            // Note: you only have to add data necessary for this
            // particular service (FriendService) to run properly.
            // There will be more tables in your DB, but you only
            // need to provide the data for the methods you are
            // actually testing here.

            _service = new PostService(mockDb);
        }

        [TestMethod]
        public void TestEditComment()
        {
            // Arrange:
            const string body = "Text New Comment";
            // Note: no user with this username has an entry
            // in our test data.

            // Act:
            _service.EditComment(1, body);
            var comment = _service.GetCommentById(1);
            
            // Assert:
            Assert.AreEqual(comment.Body, body);

        }

    }
}
