using System;
using System.Web.Mvc;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.Comment;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Web.Extension;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ISession _session;
        private readonly IPost _post;
        private readonly IComment _comment;


        public CommentController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _post = bl.GetPostBL();
            _comment = bl.GetCommentBL();
        }

        [HttpPost]
        public ActionResult AddComment(CommentData commentaryData)
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    NewCommentData data = new NewCommentData()
                    {
                        Content = commentaryData.Content,
                        DateAdded = DateTime.Now,
                        Author = user.Username,
                        AuthorId = user.Id,
                        PostId = commentaryData.PostId
                    };

                    var response = _comment.AddCommentAction(data);
                    if (response.Status)
                    {
                        return RedirectToAction("Detail", "Post", new { PostID = response.PostId });
                    }

                    ModelState.AddModelError("", response.StatusMessage);
                    return RedirectToAction("Detail", "Post", new { PostID = response.PostId });
                }

                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index", "Login");
        }
    }
}