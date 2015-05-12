using System;
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

        public Task Like(int postId, string user)
        {
            var like = Savelike(postId, user);
            return Clients.All.updateLikeCount(like);
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
            var count = post.Likes.Count();
            

            return JSONHelper.ToJSON(count);   
        }
    }
}
namespace ExtensionMethods
{
    public static class JSONHelper
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }
}