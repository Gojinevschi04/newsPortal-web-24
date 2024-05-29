using System.Collections.Generic;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Domain.Entities.User;

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
        void Delete(int postId);
        void Save();
    }
}