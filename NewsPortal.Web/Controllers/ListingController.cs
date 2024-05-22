using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Web.Models;

namespace NewsPortal.Web.Controllers
{
     public class ListingController : BaseController
     {
          private readonly ISession _session;
          private readonly IPost _post;

          public ListingController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _session = bl.GetSessionBL();
               _post = bl.GetPostBL();
          }

          public ActionResult Index()
          {
               var model = new ListingPageData();
               var sideBar = new SideBarData();

               using (var db = new PostContext())
               {
                    var categoryList = db.Posts.Select(a => a.Category).Distinct().ToList();
                    sideBar.CategoryList = categoryList;
               }

               model.SideBar = sideBar;
               var data = _post.GetAll();
               model.ListingItems = data.Select(post => new PostMinimal
               {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
               }).ToList();

               return View(model);
          }
          public ActionResult ListingSearch(string category)
          {
               if (category != null)
               {
                    var data = _post.GetPostsByCategory(category);
                    if (data.Count() > 0)
                    {
                         TempData["postsByCategory"] = data;
                         return RedirectToAction("ListingParameters");
                    }
               }

               return RedirectToAction("ListingParameters");
          }
          public ActionResult ListingParameters()
          {
               var model = new ListingPageData();
               var sideBar = new SideBarData();
               using (var db = new PostContext())
               {
                    var category = db.Posts.Select(a => a.Category).Distinct().ToList();
                    sideBar.CategoryList = category;
               }

               model.SideBar = sideBar;

               if (TempData["postsByCategory"] is List<PostMinimal> postsBySearchWrap && postsBySearchWrap.Any())

               {
                    model.ListingItems = postsBySearchWrap;
                    return View(model);
               }

               if (model.ListingItems == null) return View(model);

               return View(model);
          }
     }
}
