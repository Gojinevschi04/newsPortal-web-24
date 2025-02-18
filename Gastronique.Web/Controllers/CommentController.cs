using System;
using System.Web.Mvc;
using Gastronique.BusinessLogic.Interfaces;
using Gastronique.Domain.Entities.Comment;
using Gastronique.Domain.Entities.Restaurant;
using Gastronique.Web.Models;
using Gastronique.Web.Extension;

namespace Gastronique.Web.Controllers
{
    public class CommentController : BaseController
    {
        private readonly IRestaurant _restaurant;
        private readonly IComment _comment;


        public CommentController()
        {
            var bl = new Gastronique.BusinessLogic.BusinessLogic();
            _restaurant = bl.GetRestaurantBL();
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
                        return RedirectToAction("Detail", "Restaurant", new { PostID = response.RestaurantId });
                    }

                    ModelState.AddModelError("", response.StatusMessage);
                    return RedirectToAction("Detail", "Restaurant", new { PostID = response.RestaurantId });
                }

                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index", "Login");
        }
    }
}