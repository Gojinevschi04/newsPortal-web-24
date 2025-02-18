using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gastronique.BusinessLogic.DbModel;
using Gastronique.BusinessLogic.Interfaces;
using Gastronique.Domain.Entities.Comment;
using Gastronique.Domain.Entities.Restaurant;
using Gastronique.Domain.Entities.User;
using Gastronique.Web.Filters;
using Gastronique.Web.Models;
using Gastronique.Web.Extension;

namespace Gastronique.Web.Controllers
{
    [AdminMod]
    public class AdminController : BaseController
    {
        public readonly IRestaurant _restaurant;
        public readonly IComment _comment;
        private readonly UserMinimal adminAuthenticated;

        public AdminController()
        {
            var bl = new Gastronique.BusinessLogic.BusinessLogic();
            _restaurant = bl.GetRestaurantBL();
            _comment = bl.GetCommentBL();
            adminAuthenticated = System.Web.HttpContext.Current.GetMySessionObject();
        }


        public ActionResult Users()
        {
            var users = _session.GetAll();
            List<UserData> allUsers = new List<UserData>();
            foreach (var user in users)
            {
                var userData = new UserData();
                userData.Id = user.Id;
                userData.FirstName = user.FirstName;
                userData.LastName = user.LastName;
                userData.Email = user.Email;
                userData.Username = user.Username;
                userData.Level = user.Level;
                allUsers.Add(userData);
            }

            return View(allUsers);
        }

        public ActionResult EditUser(int? userId)
        {
            if (userId == null) return View();
            var userData = _session.GetUserById((int)userId);
            if (userData != null)
            {
                var userModel = new UserData()
                {
                    Id = userData.Id,
                    Username = userData.Username,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    Email = userData.Email,
                    Level = userData.Level
                };
                return View(userModel);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult EditUser(UserData data)
        {
            //SessionStatus();
            //var user = System.Web.HttpContext.Current.GetMySessionObject();

            if (ModelState.IsValid)
            {
                UEditData existingUser = new UEditData
                {
                    Id = data.Id,
                    Username = data.Username,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Email = data.Email,
                    Level = data.Level
                };
                var response = _session.EditProfileAction(existingUser);
                if (response.Status)
                {
                    return RedirectToAction("Users", "Admin", new { RestaurantID = response.UserId });
                }

                ModelState.AddModelError("", response.StatusMessage);
                return View(data);
            }

            return View();
        }

        public ActionResult DeleteUser(int? userId)
        {
            using (var db = new UserContext())
            {
                var userToDelete = db.Users.Find((int)userId);
                if (userToDelete == null)
                {
                    return HttpNotFound();
                }

                db.Users.Remove(userToDelete);
                db.SaveChanges();
                return RedirectToAction("Users");
            }
        }

        public ActionResult Restaurants()
        {
            var data = _restaurant.GetAll();
            List<RestaurantMinimal> allRestaurants = new List<RestaurantMinimal>();
            foreach (var restaurant in data)
            {
                var restaurantMinimal = new RestaurantMinimal();
                restaurantMinimal.Id = restaurant.Id;
                restaurantMinimal.Title = restaurant.Title;
                restaurantMinimal.Content = restaurant.Content;
                restaurantMinimal.Category = restaurant.Category;
                restaurantMinimal.Author = restaurant.Author;
                restaurantMinimal.AuthorId = restaurant.AuthorId;
                restaurantMinimal.DateAdded = restaurant.DateAdded;
                allRestaurants.Add(restaurantMinimal);
            }

            return View(allRestaurants);
        }

        public ActionResult EditRestaurant(int? RestaurantId)
        {
            if (RestaurantId == null) return View();
            var restaurantData = _restaurant.GetById((int)RestaurantId);
            if (restaurantData != null)
            {
                var restaurantModel = new RestaurantData()
                {
                    Id = restaurantData.Id,
                    Title = restaurantData.Title,
                    Content = restaurantData.Content,
                    Category = restaurantData.Category,
                    Author = restaurantData.Author,
                    DateAdded = restaurantData.DateAdded
                };
                return View(restaurantModel);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult EditRestaurant(RestaurantData data)
        {
            //SessionStatus();
            //var user = System.Web.HttpContext.Current.GetMySessionObject();

            if (ModelState.IsValid)
            {
                REditData existingRestaurant = new REditData()
                {
                    Id = data.Id,
                    Title = data.Title,
                    Content = data.Content,
                    Category = data.Category,
                    Author = data.Author,
                    DateAdded = data.DateAdded
                };
                var response = _restaurant.EditRestaurantAction(existingRestaurant);
                if (response.Status)
                {
                    return RedirectToAction("Detail", "Restaurant", new { RestaurantID = response.RestaurantId });
                }
                else
                {
                    ModelState.AddModelError("", response.StatusMessage);
                    return View(data);
                }
            }

            return View();
        }

        public ActionResult DeleteRestaurant(int? RestaurantId)
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var restaurantToDelete = _restaurant.GetById((int)RestaurantId);
            if (restaurantToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                _restaurant.Delete((int)RestaurantId);
                _restaurant.Save();
                return RedirectToAction("Restaurants", "Admin");
            }
        }

        public ActionResult Comments()
        {
            var data = _comment.GetAll();
            List<CommentMinimal> allCommentaries = new List<CommentMinimal>();

            if (data.Any())
            {
                foreach (var commentary in data)
                {
                    var commentaryMinimal = new CommentMinimal();
                    commentaryMinimal.Id = commentary.Id;
                    commentaryMinimal.Content = commentary.Content;
                    commentaryMinimal.Author = commentary.Author;
                    commentaryMinimal.AuthorId = commentary.AuthorId;
                    commentaryMinimal.DateAdded = commentary.DateAdded;
                    allCommentaries.Add(commentaryMinimal);
                }

                return View(allCommentaries);
            }

            return RedirectToAction("Error", "Home");
        }

        public ActionResult EditComment(int? commentaryId)
        {
            if (commentaryId == null) return View();
            var commentaryData = _comment.GetById((int)commentaryId);
            if (commentaryData != null)
            {
                var commentaryModel = new CommentData()
                {
                    Id = commentaryData.Id,
                    Content = commentaryData.Content,
                    Author = commentaryData.Author,
                    AuthorId = commentaryData.AuthorId,
                    DateAdded = commentaryData.DateAdded
                };
                return View(commentaryModel);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult EditComment(CommentData data)
        {
            //SessionStatus();
            //var user = System.Web.HttpContext.Current.GetMySessionObject();

            if (ModelState.IsValid)
            {
                CEditData existingCommentary = new CEditData()
                {
                    Id = data.Id,
                    Content = data.Content,
                    Author = data.Author,
                    AuthorId = data.AuthorId,
                    DateAdded = data.DateAdded
                };
                var response = _comment.EditCommentAction(existingCommentary);
                if (response.Status)
                {
                    return RedirectToAction("Comments", "Admin");
                }

                ModelState.AddModelError("", response.StatusMessage);
            }

            return View();
        }

        public ActionResult DeleteComment(int commentaryId)
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var commentaryToDelete = _comment.GetById(commentaryId);
            if (commentaryToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            _comment.Delete((int)commentaryId);
            _comment.Save();
            return RedirectToAction("Comments", "Admin");
        }
    }
}