using System;
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

          public PostController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _session = bl.GetSessionBL();
               _post = bl.GetPostBL();
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
                              ImagePath = postData.ImagePath,
                              DateAdded = DateTime.Now,
                              Author = user.Username,
                              AuthorId = user.Id
                         };

                         var response = _post.AddPostAction(data);
                         if (response.Status)
                         {
                              return RedirectToAction("Detail", "Post", new { PostID = response.PostId });
                         }
                         else
                         {
                              ModelState.AddModelError("", response.StatusMessage);
                              return View(postData);
                         }
                    }

                    return View();
               }

               return View();
          }


          [HttpGet]
          public ActionResult Detail(int postId)
          {
               var data = _post.GetById(postId);
               using (var db = new UserContext())
               {
                    var model = new PostData
                    {
                         Id = data.Id,
                         Title = data.Title,
                         Content = data.Content,
                         ImagePath = data.ImagePath,
                         DateAdded = data.DateAdded,
                    };

                    return View(model);
               }
          }
     }
}