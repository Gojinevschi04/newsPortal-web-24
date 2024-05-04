using System;
using System.Collections.Generic;
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
        private readonly ICommentary _commentary;

        public PostController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _post = bl.GetPostBL();
            _commentary = bl.GetCommentaryBL();
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
                    NewPostData data = new NewPostData()
                    {
                        Title = postData.Title,
                        Content = postData.Content,
                        Category = postData.Category,
                        DateAdded = DateTime.Now,
                        Author = user.Username,
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
            var commentaries = _commentary.GetAllCommentsByPost(postId);
            List<CommentaryData> commentaryList = new List<CommentaryData>();

            foreach (var commentary in commentaries)
            {
                var commentaryData = new CommentaryData()
                {
                    Id = commentary.Id,
                    Content = commentary.Content,
                    Author = commentary.Author,
                    AuthorId = commentary.AuthorId,
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
                    DateAdded = data.DateAdded,
                    Commentaries = commentaryList
                };

                return View(model);
            }
        }
    }
}