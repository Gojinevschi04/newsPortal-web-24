using System.Collections.Generic;
using System.Web.Mvc;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Domain.Entities.User;
using NewsPortal.Web.Extension;

namespace NewsPortal.Web.Controllers
{
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
               var data = _post.GetAllByAuthor(reporterAuthenticated.Email);
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