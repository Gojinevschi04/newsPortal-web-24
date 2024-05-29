using System.Collections.Generic;
using NewsPortal.Domain.Entities.Comment;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.BusinessLogic.Interfaces
{
    public interface IComment
    {
        ServiceResponse AddCommentAction(NewCommentData data);
        IEnumerable<CommentMinimal> GetAllCommentsByPost(int postId);
        IEnumerable<CommentMinimal> GetAll();
        CDbTable GetById(int commentId);
        ServiceResponse EditCommentAction(CEditData existingComment);
        void Delete(int commentId);
        void Save();
    }
}