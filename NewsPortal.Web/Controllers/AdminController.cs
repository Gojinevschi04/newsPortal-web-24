using System.Collections.Generic;
using System.Web.Mvc;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Domain.Entities.User;
using NewsPortal.Web.Extension;
using NewsPortal.Web.Filters;
using NewsPortal.Web.Models;


namespace NewsPortal.Web.Controllers
{
    [AdminMod]
    public class AdminController : BaseController
    {
        public readonly ISession _session;
        public readonly IPost _post;
        private readonly UserMinimal adminAuthenticated;

        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _post = bl.GetPostBL();
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
            else
            {
                return RedirectToAction("Index", "Login");
            }
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
                    return RedirectToAction("UserDetail", "User", new { PostID = response.PostId });
                }
                else
                {
                    ModelState.AddModelError("", response.StatusMessage);
                    return View(data);
                }
            }

            return View();
        }
        
        public ActionResult DeleteUser(int userId)
        {
            using (var db = new UserContext())
            {
                var userToDelete = db.Users.Find(userId);
                if (userToDelete == null)
                {
                    return HttpNotFound();
                }

                db.Users.Remove(userToDelete);
                db.SaveChanges();
                return RedirectToAction("Users");
            }
        }

        public ActionResult Posts()
        {
            var data = _post.GetAll();
            List<PostMinimal> allPosts = new List<PostMinimal>();
            foreach (var post in data)
            {
                var postMinimal = new PostMinimal();
                postMinimal.Id = post.Id;
                postMinimal.Title = post.Title;
                postMinimal.Content = post.Content;
                postMinimal.Category = post.Category;
                postMinimal.Author = post.Author;
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
            //SessionStatus();
            //var user = System.Web.HttpContext.Current.GetMySessionObject();

            if (ModelState.IsValid)
            {
                PEditData existingPost = new PEditData()
                {
                    Id = data.Id,
                    Title = data.Title,
                    Content = data.Content,
                    Category = data.Category,
                    Author = data.Author,
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
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var postToDelete = _post.GetById(postId);
            if (postToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                _post.Delete((int)postId);
                _post.Save();
                return RedirectToAction("Posts", "Admin");
            }
        }

        public ActionResult Comments()
        {
            return View();
        }

        public ActionResult DeleteComment(int postId)
        {
            return RedirectToAction("Comments", "Admin");
        }
    }
}