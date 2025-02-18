using System;
using System.Collections.Generic;
using System.Linq;
using Gastronique.BusinessLogic.DbModel;
using Gastronique.Domain.Entities.Restaurant;

namespace Gastronique.BusinessLogic.Core
{
     public class RestaurantApi
     {
          public RServiceResponse AddPost(NewRestaurantData data)
          {
               var newPost = new RDbTable
               {
                    Title = data.Title,
                    Content = data.Content,
                    Category = data.Category,
                    ImagePath = data.ImagePath,
                    DateAdded = data.DateAdded,
                    AuthorId = data.AuthorId,
                    Author = string.IsNullOrEmpty(data.Author) ? "Default Author" : data.Author
               };

               var response = new RServiceResponse();

               try
               {
                    using (var db = new PostContext())
                    {
                         db.Posts.Add(newPost);
                         db.SaveChanges();
                    }

                    response.StatusMessage = "Restaurant added successfully!";
                    response.Status = true;
                    response.RestaurantId = newPost.Id;
               }
               catch
               {
                    response.StatusMessage = "An error occured while adding restaurant";
                    response.Status = false;
                    response.RestaurantId = 0;
               }

               return response;
          }

          public IEnumerable<RestaurantMinimal> ReturnPostsByCategory(string category)
          {
               List<RestaurantMinimal> list = new List<RestaurantMinimal>();
               using (var db = new PostContext())
               {
                    var results = db.Posts.Where(a => a.Category == category);

                    foreach (var item in results)
                    {
                         var postMinimal = new RestaurantMinimal
                         {
                              Id = item.Id,
                              Title = item.Title,
                              Content = item.Content,
                              ImagePath = item.ImagePath,
                              Category = item.Category
                         };

                         list.Add(postMinimal);
                    }
               }

               return list.ToList();
          }

          public IEnumerable<RestaurantMinimal> ReturnPostsByKey(string key)
          {
               List<RestaurantMinimal> list = new List<RestaurantMinimal>();
               using (var db = new PostContext())
               {
                    var results = db.Posts.Where(item => item.Title.Contains(key)
                                                         || item.Content.Contains(key)
                    );

                    foreach (var item in results)
                    {
                         var postMinimal = new RestaurantMinimal
                         {
                              Id = item.Id,
                              Title = item.Title,
                              Content = item.Content,
                              ImagePath = item.ImagePath,
                              Category = item.Category
                         };

                         list.Add(postMinimal);
                    }
               }

               return list.ToList();
          }

          public IEnumerable<RestaurantMinimal> ReturnPostsByAuthor(int authorId)
          {
               List<RestaurantMinimal> list = new List<RestaurantMinimal>();
               using (var db = new PostContext())
               {
                    var results = db.Posts.Where(a => a.AuthorId == authorId);

                    foreach (var item in results)
                    {
                         var postMinimal = new RestaurantMinimal
                         {
                              Id = item.Id,
                              Title = item.Title,
                              Content = item.Content,
                              Category = item.Category,
                              ImagePath = item.ImagePath,
                              DateAdded = item.DateAdded
                         };

                         list.Add(postMinimal);
                    }
               }

               return list.ToList();
          }

          public RServiceResponse ReturnEditedPost(REditData existingPost)
          {
               var response = new RServiceResponse();
               using (var db = new PostContext())
               {
                    try
                    {
                         var postToEdit = db.Posts.Find(existingPost.Id);
                         if (postToEdit != null)
                         {
                              postToEdit.Id = existingPost.Id;
                              postToEdit.Title = existingPost.Title;
                              postToEdit.Content = existingPost.Content;
                              postToEdit.Category = existingPost.Category;
                              postToEdit.ImagePath = existingPost.ImagePath;
                              postToEdit.Author = existingPost.Author;
                              postToEdit.AuthorId = existingPost.AuthorId;
                              postToEdit.DateAdded = existingPost.DateAdded;

                              db.SaveChanges();
                              response.Status = true;
                              response.RestaurantId = postToEdit.Id;
                              response.StatusMessage = "Your post was edited successfully!";
                         }
                         else
                         {
                              response.RestaurantId = 0;
                              response.Status = false;
                              response.StatusMessage = "Post not found!";
                         }
                    }
                    catch (Exception ex)
                    {
                         response.RestaurantId = 0;
                         response.Status = false;
                         response.StatusMessage = "An error occurred!";
                    }
               }

               return response;
          }
     }
}