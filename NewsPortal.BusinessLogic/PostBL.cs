using System.Collections.Generic;
using System.Linq;
using NewsPortal.BusinessLogic.Core;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.BusinessLogic
{
    public class PostBL : PostApi, IPost
    {
        private readonly PostContext _context;

        public PostBL()
        {
            _context = new PostContext();
        }

        public PostBL(PostContext context)
        {
            _context = context;
        }

        public ServiceResponse AddPostAction(NewPostData data)
        {
            return AddPost(data);
        }

        public PDbTable GetById(int postId)
        {
            return _context.Posts.Find(postId);
        }

        public IEnumerable<PDbTable> GetAll()
        {
            return _context.Posts.ToList();
        }

        public IEnumerable<PostMinimal> GetAllByAuthor(string author)
        {
            return ReturnPostsByAuthor(author);
        }

        public IEnumerable<PostMinimal> GetPostsByCategory(string category)
        {
            return ReturnPostsByCategory(category);
        }

        public void Delete(int PostID)
        {
            PDbTable model = _context.Posts.Find(PostID);
            if (model != null)
            {
                _context.Posts.Remove(model);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}