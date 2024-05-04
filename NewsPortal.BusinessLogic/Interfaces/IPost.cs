using System.Collections.Generic;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.BusinessLogic.Interfaces
{
    public interface IPost
    {
        ServiceResponse AddPostAction(NewPostData data);
        PDbTable GetById(int postId);
        IEnumerable<PDbTable> GetAll();
        IEnumerable<PostMinimal> GetAllByAuthor(int authorId);
        IEnumerable<PostMinimal> GetPostsByCategory(string category);
        IEnumerable<PostMinimal> GetPostsByKey(string key);
        ServiceResponse EditPostAction(PEditData existingPost);
        // IEnumerable<PostMinimal> GetLatestPosts();
        // void Update(PDbTable model);
        void Delete(int postId);
        void Save();
    }
}