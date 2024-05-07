using System;
using System.Collections.Generic;
using System.Linq;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.BusinessLogic.Core
{
    public class PostApi
    {
        public ServiceResponse AddPost(NewPostData data)
        {
            var newPost = new PDbTable()
            {
                Title = data.Title,
                Content = data.Content,
                Category = data.Category,
                DateAdded = data.DateAdded,
                ImagePath = data.ImagePath,
                AuthorId = data.AuthorId,
                Author = data.Author
            };

            var response = new ServiceResponse();

            try
            {
                using (var db = new PostContext())
                {
                    db.Posts.Add(newPost);
                    db.SaveChanges();
                }

                response.StatusMessage = "Post added successfully!";
                response.Status = true;
                response.PostId = newPost.Id;
            }
            catch
            {
                response.StatusMessage = "An error occured while adding post";
                response.Status = false;
                response.PostId = 0;
            }

            return response;
        }

        public IEnumerable<PostMinimal> ReturnPostsByCategory(string category)
        {
            List<PostMinimal> list = new List<PostMinimal>();
            using (var db = new PostContext())
            {
                var results = db.Posts.Where(a => a.Category == category);

                foreach (var item in results)
                {
                    var postMinimal = new PostMinimal
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

        public IEnumerable<PostMinimal> ReturnPostsByKey(string key)
        {
            List<PostMinimal> list = new List<PostMinimal>();
            using (var db = new PostContext())
            {
                var results = db.Posts.Where(item => item.Title.Contains(key)
                                                     || item.Content.Contains(key)
                );

                foreach (var item in results)
                {
                    var postMinimal = new PostMinimal
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

        public IEnumerable<PostMinimal> ReturnPostsByAuthor(int authorId)
        {
            List<PostMinimal> list = new List<PostMinimal>();
            using (var db = new PostContext())
            {
                var results = db.Posts.Where(a => a.AuthorId == authorId);

                foreach (var item in results)
                {
                    var postMinimal = new PostMinimal
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

        public ServiceResponse ReturnEditedPost(PEditData existingPost)
        {
            var response = new ServiceResponse();
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
                        response.PostId = postToEdit.Id;
                        response.StatusMessage = "Your post was edited successfully!";
                    }
                    else
                    {
                        response.PostId = 0;
                        response.Status = false;
                        response.StatusMessage = "Post not found!";
                    }
                }
                catch (Exception ex)
                {
                    response.PostId = 0;
                    response.Status = false;
                    response.StatusMessage = "An error occurred!";
                }
            }

            return response;
        }
    }
}