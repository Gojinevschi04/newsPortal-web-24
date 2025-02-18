using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gastronique.BusinessLogic.DbModel;
using Gastronique.BusinessLogic.Interfaces;
using Gastronique.Domain.Entities.Restaurant;
using Gastronique.Web.Models;

namespace Gastronique.Web.Controllers
{
    public class ListingController : BaseController
    {
        private readonly ISession _session;
        private readonly IRestaurant _restaurant;

        public ListingController()
        {
            var bl = new Gastronique.BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _restaurant = bl.GetRestaurantBL();
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
            var data = _restaurant.GetAll();
            model.ListingItems = data.Select(post => new RestaurantMinimal
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
                var data = _restaurant.GetRestaurantByKey(key);
                if (data.Count() > 0)
                {
                    TempData["posts"] = data;
                    return RedirectToAction("ListingParameters", new { key = key });
                }
            }

               return RedirectToAction("Error", "Home");
          }

        public ActionResult ListingSearch(string category)
        {
            if (category != null)
            {
                var data = _restaurant.GetRestaurantByCategory(category);
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

            if (TempData["posts"] is List<RestaurantMinimal> postsBySearchWrap && postsBySearchWrap.Any())
            {
                model.ListingItems = postsBySearchWrap;
                return View(model);
            }

            return RedirectToAction("Error", "Home");
        }
    }
}