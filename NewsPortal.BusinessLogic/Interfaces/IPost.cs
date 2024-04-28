using System.Collections.Generic;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.BusinessLogic.Interfaces
{
    public interface IPost
    {
        ServiceResponse AddPostAction(NewPostData data);
        PDbTable GetById(int postId);
        IEnumerable<PDbTable> GetAll();
        IEnumerable<PostMinimal> GetAllByAuthor(string author);

        IEnumerable<PostMinimal> GetPostsByCategory(string category);
        // IEnumerable<PostMinimal> GetLatestPosts();
        // IEnumerable<PostMinimal> GetPostsByType(string type);
        // void Update(PDbTable model);
        // void Delete(int postId);
        // void Save();
    }
}