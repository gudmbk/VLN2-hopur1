using System;
using System.IO;
using System.Net.Mime;
using System.Web.Mvc;
using klukk_social.Models;
using klukk_social.Services;
using Microsoft.AspNet.Identity;

namespace klukk_social.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        readonly PostService _postService = new PostService();
        readonly UserService _userService = new UserService();

        [HttpPost]
        public ActionResult PostStatus(FormCollection collection)
        {
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
				return RedirectToAction("Profile", "User", new { userId = _postService.GetToUserIdPostId(postId)});
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

        public ActionResult EditPost(int itemId)
        {
            Post postToEdit = _postService.GetPostById(itemId);
            var anom = new
            {
                id = postToEdit.Id,
                text = postToEdit.Text
            };
            
            
            return Json(anom, JsonRequestBehavior.AllowGet);
            //_postService.EditPost(changedItem);

        }
        public ActionResult EditComment(int itemId)
        {
            //_postService.EditComment(changedItem);

            return null;
        }
        /*
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }*/
    }
}