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
        private readonly ICommentary _commentary;

        public ListingController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _post = bl.GetPostBL();
            _commentary = bl.GetCommentaryBL();
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
                ImagePath = post.ImagePath,
                Content = post.Content,
            }).ToList();

            return View(model);
        }

        public ActionResult ListingSearchByKey(string key)
        {
            if (key != null)
            {
                var data = _post.GetPostsByKey(key);
                if (data.Count() > 0)
                {
                    TempData["posts"] = data;
                    return RedirectToAction("ListingParameters", new { key = key });
                }
            }

            return RedirectToAction("Index", "Listing");
        }

        public ActionResult ListingSearch(string category)
        {
            if (category != null)
            {
                var data = _post.GetPostsByCategory(category);
                if (data.Count() > 0)
                {
                    TempData["posts"] = data;
                    return RedirectToAction("ListingParameters");
                }

                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Error", "Home");
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

            if (TempData["posts"] is List<PostMinimal> postsBySearchWrap && postsBySearchWrap.Any())
            {
                model.ListingItems = postsBySearchWrap;
                return View(model);
            }

            return RedirectToAction("Error", "Home");
        }
    }
}