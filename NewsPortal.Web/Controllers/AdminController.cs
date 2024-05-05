using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.Comment;
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
        public readonly ICommentary _commentary;

        private readonly UserMinimal adminAuthenticated;

        public AdminController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _post = bl.GetPostBL();
            _commentary = bl.GetCommentaryBL();
            adminAuthenticated = System.Web.HttpContext.Current.GetMySessionObject();
        }

        public ActionResult Users()
        {
            var users = _session.GetAll();
            List<UserData> allUsers = new List<UserData>();

            if (users.Any())
            {
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

            return RedirectToAction("Error", "Home");
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
                    return RedirectToAction("Users", "Admin", new { PostID = response.PostId });
                }

                ModelState.AddModelError("", response.StatusMessage);
                return View(data);
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

            if (data.Any())
            {
                foreach (var post in data)
                {
                    var postMinimal = new PostMinimal();
                    postMinimal.Id = post.Id;
                    postMinimal.Title = post.Title;
                    postMinimal.Content = post.Content;
                    postMinimal.Category = post.Category;
                    postMinimal.Author = post.Author;
                    postMinimal.AuthorId = post.AuthorId;
                    postMinimal.DateAdded = post.DateAdded;
                    allPosts.Add(postMinimal);
                }

                return View(allPosts);
            }

            return RedirectToAction("Error", "Home");
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

            return RedirectToAction("Index", "Login");
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

                ModelState.AddModelError("", response.StatusMessage);
                return View(data);
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

            _post.Delete((int)postId);
            _post.Save();
            return RedirectToAction("Posts", "Admin");
        }

        public ActionResult Comments()
        {
            var data = _commentary.GetAll();
            List<CommentaryMinimal> allCommentaries = new List<CommentaryMinimal>();

            if (data.Any())
            {
                foreach (var commentary in data)
                {
                    var commentaryMinimal = new CommentaryMinimal();
                    commentaryMinimal.Id = commentary.Id;
                    commentaryMinimal.Content = commentary.Content;
                    commentaryMinimal.Author = commentary.Author;
                    commentaryMinimal.AuthorId = commentary.AuthorId;
                    commentaryMinimal.PostId = commentary.PostId;
                    commentaryMinimal.DateAdded = commentary.DateAdded;
                    allCommentaries.Add(commentaryMinimal);
                }

                return View(allCommentaries);
            }

            return RedirectToAction("Error", "Home");
        }

        public ActionResult EditCommentary(int? commentaryId)
        {
            if (commentaryId == null) return View();
            var commentaryData = _commentary.GetById((int)commentaryId);
            if (commentaryData != null)
            {
                var commentaryModel = new CommentaryData()
                {
                    Id = commentaryData.Id,
                    Content = commentaryData.Content,
                    Author = commentaryData.Author,
                    AuthorId = commentaryData.AuthorId,
                    PostId = commentaryData.PostId,
                    DateAdded = commentaryData.DateAdded
                };
                return View(commentaryModel);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult EditCommentary(CommentaryData data)
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
                    PostId = data.PostId,
                    DateAdded = data.DateAdded
                };
                var response = _commentary.EditCommentaryAction(existingCommentary);
                if (response.Status)
                {
                    return RedirectToAction("Comments", "Admin");
                }

                ModelState.AddModelError("", response.StatusMessage);
            }

            return View();
        }

        public ActionResult DeleteCommentary(int commentaryId)
        {
            SessionStatus();
            var user = System.Web.HttpContext.Current.GetMySessionObject();
            var commentaryToDelete = _commentary.GetById(commentaryId);
            if (commentaryToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            _commentary.Delete((int)commentaryId);
            _commentary.Save();
            return RedirectToAction("Comments", "Admin");
        }
    }
}