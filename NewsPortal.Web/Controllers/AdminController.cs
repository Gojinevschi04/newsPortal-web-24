using System.Collections.Generic;
using System.Web.Mvc;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Domain.Entities.User;
using NewsPortal.Web.Extension;
using NewsPortal.Web.Models;


namespace NewsPortal.Web.Controllers
{
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


        public ActionResult Posts()
        {
            var data = _post.GetAll();
            List<PostMinimal> allPosts = new List<PostMinimal>();
            foreach (var post in data)
            {
                var postMinimal = new PostMinimal();
                postMinimal.Title = post.Title;
                postMinimal.Content = post.Content;
                postMinimal.Category = post.Category;
                postMinimal.DateAdded = post.DateAdded;
                allPosts.Add(postMinimal);
            }

            return View(allPosts);
        }

        public ActionResult Comments()
        {
            return View();
        }
    }
}