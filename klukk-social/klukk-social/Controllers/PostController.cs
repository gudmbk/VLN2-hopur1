using System;
using System.Web.Mvc;
using klukk_social.Models;
using klukk_social.Services;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Drawing;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.AspNet.Identity.EntityFramework;

namespace klukk_social.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        readonly PostService _postService = new PostService(null);
        readonly UserService _userService = new UserService(null);
		readonly GroupService _groupService = new GroupService(null);
        HelperController _helper = new HelperController();

        [HttpPost]
        public ActionResult PostStatus(FormCollection collection)
        {/*
            post.HtmlText = Helpers.ParseText(post.Text);
            post.FromUserId = User.Identity.GetUserId();
            post.PosterName = _userService.GetFullNameById(User.Identity.GetUserId());
            _postService.AddPost(post);
            InteractionBarViewModel model = new InteractionBarViewModel();
            model.IsPost = true;
            model.Feed.Add(post);
            model.Post = post;
            var postHtml = Helpers.RenderViewToString(this.ControllerContext, "PostPartial", model);
            return Json(postHtml, JsonRequestBehavior.AllowGet);
            */

            
            
            Post post = new Post();
            string text = collection["status"];
            if (String.IsNullOrEmpty(text))
            {
				return RedirectToAction("Profile", "User", new { userId = post.ToUserId });
            }
            post.Text = text;
            post.HtmlText = Helpers.ParseText(text);
            post.FromUserId = User.Identity.GetUserId();
            post.ToUserId = collection["toUserId"];
            post.PosterName = _userService.GetFullNameById(User.Identity.GetUserId());
            if (post.FromUserId != null)
            {
                _postService.AddPost(post);
				return RedirectToAction("Profile", "User", new { userId = post.ToUserId });
            }
            
            return View("Error");
            
        }

        public ActionResult PostComment(FormCollection collection)
        {
            Comment comment = new Comment();
            string text = collection["comment"];
            string id = collection["PostId"];

            int postId = Int32.Parse(id);
            if (String.IsNullOrEmpty(text))
            {
                return RedirectToAction("ChildHome", "User");
            }
            comment.Body = text;
            comment.UserId = User.Identity.GetUserId();
            comment.PostId = postId;
            comment.PosterName = _userService.GetFullNameById(User.Identity.GetUserId());
            if (comment.UserId != null)
            {
                _postService.AddComment(comment);
				if (_postService.OnUserWall(postId))
				{
					return RedirectToAction("Profile", "User", new { userId = _postService.GetToUserIdPostId(postId) });
				}
				return RedirectToAction("Profile", "Group", new { groupId = _groupService.FindGroupId(postId) });
            }
            return View("Error");
        }

        public ActionResult RemovePost(int itemId)
        {
            _postService.RemovePost(itemId);
            return null;
        }
        public ActionResult RemoveComment(int itemId)
        {
            _postService.RemoveComment(itemId);
            return null;
        }
        public ActionResult ReportItem(int itemId, bool isPost)
        {
            string reporterId = User.Identity.GetUserId();
            if (User.IsInRole("Parent"))
            {
                var user = _userService.FindById(reporterId);
                string message = "Another parent reported a post made by you child on Klukk, please take a moment to view the post here. http://localhost:5080/User/Reports?userId=" + user.Id;
                Helpers.LogMessage(message, user.Email);
            }
            if (isPost)
            {
                _postService.AddReportPost(itemId, reporterId);
            }
            else
            {
                _postService.AddReportComment(itemId, reporterId);
            }

            return null;
        }

        public ActionResult IgnoreReport(int reportId)
        {
            _postService.DeleteReport(reportId);
            return null;
        }

        public ActionResult DeleteReportedItem(int reportId)
        {
            _postService.DeleteReportedItem(reportId);
            return null;
        }

        public ActionResult EditPost(int itemId, string newPost)
        {
            string htmlText = Helpers.ParseText(newPost);
            _postService.EditPost(itemId, newPost, htmlText);

            return Json(htmlText, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditComment(int itemId, string newPost)
        {
            _postService.EditComment(itemId, newPost);

            return Json(newPost, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
        public ActionResult PostPicture(FormCollection collection)
        {
			var file = Request.Files["file"];
			if (file != null && file.ContentLength > 0)
			{
				Post post = new Post();
				post.FromUserId = User.Identity.GetUserId();
				if (post.FromUserId != null)
				{
					post.ToUserId = collection["toUserId"];
					post.PosterName = _userService.GetFullNameById(User.Identity.GetUserId());
					_postService.AddPost(post);
					string url = _helper.UploadPicture(file, post.Id.ToString(), "post", new Size(1024, 768));
					_postService.AddPicUrl(post.Id, url);
					return RedirectToAction("Profile", "User", new { userId = post.ToUserId });
				}
			}



            return View("Error");
        }

		[HttpPost]
        public ActionResult PostVideo(FormCollection collection)
        {
			string link = collection["link"];

			string frame = Helpers.ParseText(link);
			Post post = new Post();
			post.FromUserId = User.Identity.GetUserId();
			post.ToUserId = collection["toUserId"];
			post.HtmlText = frame;
			post.VideoUrl = frame;
			post.Text = link;
			if (post.ToUserId != null)
			{
				_postService.AddPost(post);
				return RedirectToAction("Profile", "User", new { userId = post.ToUserId });
			}
			return View("Error");
        }
    }
}