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
        
        
    }
}