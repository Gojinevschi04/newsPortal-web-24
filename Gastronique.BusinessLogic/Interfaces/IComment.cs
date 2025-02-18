using System.Collections.Generic;
using Gastronique.Domain.Entities.Comment;
using Gastronique.Domain.Entities.Restaurant;

namespace Gastronique.BusinessLogic.Interfaces
{
    public interface IComment
    {
        RServiceResponse AddCommentAction(NewCommentData data);
        IEnumerable<CommentMinimal> GetAllCommentsByPost(int postId);
        IEnumerable<CommentMinimal> GetAll();
        CDbTable GetById(int commentId);
        RServiceResponse EditCommentAction(CEditData existingComment);
        void Delete(int commentId);
        void Save();
    }
}