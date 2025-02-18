// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Web.Mvc;
// using Gastronique.BusinessLogic.Interfaces;
// using Gastronique.Domain.Entities.Restaurant;
// using Gastronique.Domain.Entities.User;
// using Gastronique.Web.Filters;
// using Gastronique.Web.Models;
// using Gastronique.Web.Extension;
//
// namespace Gastronique.Web.Controllers
// {
//     [ReporterMod]
//     public class ReporterController : BaseController
//     {
//         public readonly ISession _session;
//         public readonly IRestaurant Restaurant;
//         private readonly UserMinimal reporterAuthenticated;
//
//         public ReporterController()
//         {
//             var bl = new Gastronique.BusinessLogic.BusinessLogic();
//             _session = bl.GetSessionBL();
//             Restaurant = bl.GetRestaurantBL();
//             reporterAuthenticated = System.Web.HttpContext.Current.GetMySessionObject();
//         }
//
//         public ActionResult Posts()
//         {
//             var data = Restaurant.GetAllByUser(reporterAuthenticated.Id);
//             List<RestaurantMinimal> allPosts = new List<RestaurantMinimal>();
//             foreach (var post in data)
//             {
//                 var postMinimal = new RestaurantMinimal();
//                 postMinimal.Id = post.Id;
//                 postMinimal.Title = post.Title;
//                 postMinimal.Content = post.Content;
//                 postMinimal.Category = post.Category;
//                 postMinimal.ImagePath = post.ImagePath;
//                 postMinimal.DateAdded = post.DateAdded;
//                 allPosts.Add(postMinimal);
//             }
//
//             return View(allPosts);
//         }
//
//         public ActionResult EditPost(int? postId)
//         {
//             if (postId == null) return View();
//             var postData = Restaurant.GetById((int)postId);
//             if (postData != null)
//             {
//                 var postModel = new PostEditData()
//                 {
//                      Id = postData.Id,
//                      Title = postData.Title,
//                      Content = postData.Content,
//                      Category = postData.Category,
//                      Author = postData.Author,
//                      ImagePath = postData.ImagePath,
//                      AuthorId = postData.AuthorId,
//                      DateAdded = postData.DateAdded
//                 };
//                 return View(postModel);
//             }
//             else
//             {
//                 return RedirectToAction("Index", "Login");
//             }
//         }
//
//         [HttpPost]
//         public ActionResult EditPost(PostEditData data)
//         {
//             if (ModelState.IsValid)
//             {
//                 string completeFileName = null;
//                 string fileName = null;
//
//                 if (data.ImageFile != null && data.ImageFile.ContentLength > 0)
//                 {
//                     fileName = Path.GetFileNameWithoutExtension(data.ImageFile.FileName);
//                     string extension = Path.GetExtension(data.ImageFile.FileName);
//                     fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
//                     completeFileName = "~/Content/PostsImages/" + fileName;
//                 }
//
//                 if (completeFileName != data.ImagePath)
//                 {
//                     data.NewImagePath = completeFileName;
//                     fileName = Path.Combine(Server.MapPath("~/Content/PostsImages/"), fileName);
//                     data.ImageFile.SaveAs(fileName);
//                 }
//                 else
//                 {
//                     data.NewImagePath = data.ImagePath;
//                 }
//
//                 REditData existingPost = new REditData()
//                 {
//                     Id = data.Id,
//                     Title = data.Title,
//                     Content = data.Content,
//                     Category = data.Category,
//                     ImagePath = data.NewImagePath,
//                     Author = data.Author,
//                     AuthorId = data.AuthorId,
//                     DateAdded = data.DateAdded
//                 };
//                 var response = Restaurant.EditRestaurantAction(existingPost);
//                 if (response.Status)
//                 {
//                     return RedirectToAction("Detail", "Restaurant", new { PostID = response.PostId });
//                 }
//
//                 ModelState.AddModelError("", response.StatusMessage);
//                 return View(data);
//             }
//             return View();
//         }
//
//         public ActionResult DeletePost(int postId)
//         {
//             SessionStatus();
//             var postToDelete = Restaurant.GetById(postId);
//             if (postToDelete == null)
//             {
//                 return HttpNotFound();
//             }
//
//             Restaurant.Delete((int)postId);
//             Restaurant.Save();
//             return RedirectToAction("Posts", "Reporter");
//         }
//     }
// }