using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Gastronique.BusinessLogic.DbModel;
using Gastronique.BusinessLogic.Interfaces;
using Gastronique.Domain.Entities.Restaurant;
using Gastronique.Web.Filters;
using Gastronique.Web.Models;
using Gastronique.Web.Extension;

namespace Gastronique.Web.Controllers
{
    public class RestaurantController : BaseController
    {
        private readonly IRestaurant _restaurant;
        private readonly IComment _comment;

        public RestaurantController()
        {
            var bl = new Gastronique.BusinessLogic.BusinessLogic();
            _restaurant = bl.GetRestaurantBL();
            _comment = bl.GetCommentBL();
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ReporterMod]
        public ActionResult Index(RestaurantData restaurantData)
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    if (restaurantData.ImageFile != null && restaurantData.ImageFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(restaurantData.ImageFile.FileName);
                        string extension = Path.GetExtension(restaurantData.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        restaurantData.ImagePath = "~/Content/PostsImages/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Content/PostsImages/"), fileName);
                        restaurantData.ImageFile.SaveAs(fileName);
                    }

                    NewRestaurantData data = new NewRestaurantData()
                    {
                        Title = restaurantData.Title,
                        Content = restaurantData.Content,
                        Category = restaurantData.Category,
                        ImagePath = restaurantData.ImagePath,
                        DateAdded = DateTime.Now,
                        Author = user.Email,
                        AuthorId = user.Id
                    };

                    var response = _restaurant.AddRestaurantAction(data);
                    if (response.Status)
                    {
                        return RedirectToAction("Detail", "Restaurant", new { PostID = response.RestaurantId });
                    }

                    ModelState.AddModelError("", response.StatusMessage);
                    return View(restaurantData);
                }

                RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
        }


        [HttpGet]
        public ActionResult Detail(int restauranId)
        {
            var data = _restaurant.GetById(restauranId);
            var commentaries = _comment.GetAllCommentsByPost(restauranId);
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
                var model = new RestaurantData
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