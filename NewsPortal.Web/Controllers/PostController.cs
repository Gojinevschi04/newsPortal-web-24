using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Web.Extension;
using NewsPortal.Web.Filters;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers
{
    public class PostController : BaseController
    {
        private readonly ISession _session;
        private readonly IPost _post;
        private readonly IComment _comment;

        public PostController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _post = bl.GetPostBL();
            _comment = bl.GetCommentBL();
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ReporterMod]
        public ActionResult Index(PostData postData)
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    if (postData.ImageFile != null && postData.ImageFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(postData.ImageFile.FileName);
                        string extension = Path.GetExtension(postData.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        postData.ImagePath = "~/Content/PostsImages/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Content/PostsImages/"), fileName);
                        postData.ImageFile.SaveAs(fileName);
                    }

                    NewPostData data = new NewPostData()
                    {
                        Title = postData.Title,
                        Content = postData.Content,
                        Category = postData.Category,
                        ImagePath = postData.ImagePath,
                        DateAdded = DateTime.Now,
                        Author = user.Email,
                        AuthorId = user.Id
                    };

                    var response = _post.AddPostAction(data);
                    if (response.Status)
                    {
                        return RedirectToAction("Detail", "Post", new { PostID = response.PostId });
                    }

                    ModelState.AddModelError("", response.StatusMessage);
                    return View(postData);
                }

                RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }


        [HttpGet]
        public ActionResult Detail(int postId)
        {
            var data = _post.GetById(postId);
            var commentaries = _comment.GetAllCommentsByPost(postId);
            List<CommentData> commentaryList = new List<CommentData>();

            foreach (var commentary in commentaries)
            {
                var commentaryData = new CommentData()
                {
                    Id = commentary.Id,
                    Content = commentary.Content,
                    Author = commentary.Author,
                    AuthorId = commentary.AuthorId,
                    PostId = commentary.AuthorId,
                    DateAdded = commentary.DateAdded,
                };

                commentaryList.Add(commentaryData);
            }

            using (var db = new UserContext())
            {
                var model = new PostData
                {
                    Id = data.Id,
                    Title = data.Title,
                    Content = data.Content,
                    ImagePath = data.ImagePath,
                    DateAdded = data.DateAdded,
                    Comments = commentaryList
                };

                return View(model);
            }
        }
    }
}