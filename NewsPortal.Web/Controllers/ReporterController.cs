using System.Collections.Generic;
using System.Web.Mvc;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Domain.Entities.User;
using NewsPortal.Web.Extension;
using NewsPortal.Web.Filters;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers
{
    [ReporterMod]
    public class ReporterController : BaseController
    {
        public readonly ISession _session;
        public readonly IPost _post;
        private readonly UserMinimal reporterAuthenticated;

        public ReporterController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _post = bl.GetPostBL();
            reporterAuthenticated = System.Web.HttpContext.Current.GetMySessionObject();
        }

        public ActionResult Posts()
        {
            var data = _post.GetAllByAuthor(reporterAuthenticated.Id);
            List<PostMinimal> allPosts = new List<PostMinimal>();
            foreach (var post in data)
            {
                var postMinimal = new PostMinimal();
                postMinimal.Id = post.Id;
                postMinimal.Title = post.Title;
                postMinimal.Content = post.Content;
                postMinimal.Category = post.Category;
                postMinimal.DateAdded = post.DateAdded;
                allPosts.Add(postMinimal);
            }

            return View(allPosts);
        }

        public ActionResult EditPost(int? postId)
        {
            if (postId == null) return View();
            var postData = _post.GetById((int)postId);
            if (postData != null)
            {
                var postModel = new PostData()
                {
                    Id = postData.Id,
                    Title = postData.Title,
                    Content = postData.Content,
                    Category = postData.Category,
                    Author = postData.Author,
                    AuthorId = postData.AuthorId,
                    DateAdded = postData.DateAdded
                };
                return View(postModel);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult EditPost(PostData data)
        {
            if (ModelState.IsValid)
            {
                PEditData existingPost = new PEditData()
                {
                    Id = data.Id,
                    Title = data.Title,
                    Content = data.Content,
                    Category = data.Category,
                    Author = data.Author,
                    AuthorId = data.AuthorId,
                    DateAdded = data.DateAdded
                };
                var response = _post.EditPostAction(existingPost);
                if (response.Status)
                {
                    return RedirectToAction("Detail", "Post", new { PostID = response.PostId });
                }
                else
                {
                    ModelState.AddModelError("", response.StatusMessage);
                    return View(data);
                }
            }

            return View();
        }

        public ActionResult DeletePost(int postId)
        {
            SessionStatus();
            var postToDelete = _post.GetById(postId);
            if (postToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                _post.Delete((int)postId);
                _post.Save();
                return RedirectToAction("Posts", "Reporter");
            }
        }

        public ActionResult Comments()
        {
            return View();
        }
    }
}