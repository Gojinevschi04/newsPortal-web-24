using System;
using System.Collections.Generic;
using System.IO;
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
                var postModel = new PostEditData()
                {
                    Id = postData.Id,
                    Title = postData.Title,
                    Content = postData.Content,
                    Category = postData.Category,
                    Author = postData.Author,
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
        public ActionResult EditPost(PostEditData data)
        {
            if (ModelState.IsValid)
            {
                string completeFileName = null;
                string fileName = null;

                if (data.ImageFile != null && data.ImageFile.ContentLength > 0)
                {
                    fileName = Path.GetFileNameWithoutExtension(data.ImageFile.FileName);
                    string extension = Path.GetExtension(data.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    completeFileName = "~/Content/PostsImages/" + fileName;
                }

                if (completeFileName != data.ImagePath)
                {
                    data.NewImagePath = completeFileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/PostsImages/"), fileName);
                    data.ImageFile.SaveAs(fileName);
                }
                else
                {
                    data.NewImagePath = data.ImagePath;
                }

                PEditData existingPost = new PEditData()
                {
                    Id = data.Id,
                    Title = data.Title,
                    Content = data.Content,
                    Category = data.Category,
                    ImagePath = data.NewImagePath,
                    Author = data.Author,
                    AuthorId = data.AuthorId,
                    DateAdded = data.DateAdded
                };
                var response = _post.EditPostAction(existingPost);
                if (response.Status)
                {
                    return RedirectToAction("Detail", "Post", new { PostID = response.PostId });
                }

                ModelState.AddModelError("", response.StatusMessage);
                return View(data);
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

            _post.Delete((int)postId);
            _post.Save();
            return RedirectToAction("Posts", "Reporter");
        }
    }
}