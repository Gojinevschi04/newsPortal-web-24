using System.Collections.Generic;
using NewsPortal.Domain.Entities.Comment;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.BusinessLogic.Interfaces
{
    public interface ICommentary
    {
        ServiceResponse AddCommentaryAction(NewCommentaryData data);

        IEnumerable<CommentaryMinimal> GetAllCommentsByPost(int postId);
        // CDbTable GetById(int commentId);
        // IEnumerable<CommentMinimal> GetAllByAuthor(string author);
    }
}