using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ExtensionMethods;
using klukk_social.Models;
using Microsoft.AspNet.SignalR;
using klukk_social.Services;

namespace klukk_social.Hubs
{
    public class LikeHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public Task Like(int itemId, string user, bool type)
        {
            if (type)
            {
                var like = Savelike(itemId, user);
                return Clients.All.updateLikeCount(like);
            }
            else
            {
                var like = SaveCommentLike(itemId, user);
                return Clients.All.updateLikeCount(like);
            }

        }

        private string SaveCommentLike(int commentId, string user)
        {
            var postService = new PostService();
            var item = postService.GetCommentById(commentId);
            var liked = new CommentLikes
            {
                Id = 0,
                CommentId = item.Id,
                UserId = user,
            };
            postService.AddCommentLike(liked);
            var comment = postService.GetCommentById(commentId);
            var anom = new { id = comment.Id, count = comment.Likes.Count(), type = false };
            
            return JsonHelper.ToJson(anom);

        }

        private string Savelike(int postId, string user)
        {
            var postService = new PostService();
            var item = postService.GetPostById(postId);
            var liked = new Likes
            {
                Id = 0,
                PostId = item.Id,
                UserId = user,
            };
            postService.AddLike(liked);
            var post = postService.GetPostById(postId);
            var anom = new {id = post.Id, count = post.Likes.Count(), type = true};
            return JsonHelper.ToJson(anom);   
        }
    }
}
namespace ExtensionMethods
{
    public static class JsonHelper
    {
        public static string ToJson(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJson(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }
}