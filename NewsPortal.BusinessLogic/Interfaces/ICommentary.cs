using System.Collections.Generic;
using NewsPortal.Domain.Entities.Comment;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.BusinessLogic.Interfaces
{
    public interface ICommentary
    {
        ServiceResponse AddCommentaryAction(NewCommentaryData data);
        IEnumerable<CommentaryMinimal> GetAllCommentsByPost(int postId);
        IEnumerable<CDbTable> GetAll();
        CDbTable GetById(int commentId);
        ServiceResponse EditCommentaryAction(CEditData existingCommentary);
        void Delete(int commentId);
        void Save();
    }
}