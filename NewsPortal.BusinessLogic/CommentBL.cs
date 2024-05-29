using System.Collections.Generic;
using NewsPortal.BusinessLogic.Core;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.Comment;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.BusinessLogic
{
    public class CommentBL : CommentApi, IComment
    {
        private readonly CommentContext _context;

        public CommentBL()
        {
            _context = new CommentContext();
        }

        public CommentBL(CommentContext context)
        {
            _context = context;
        }

        public IEnumerable<CommentMinimal> GetAll()
        {
            return ReturnAllComments();
        }

        public ServiceResponse AddCommentAction(NewCommentData data)
        {
            return AddComment(data);
        }

        public IEnumerable<CommentMinimal> GetAllCommentsByPost(int postId)
        {
            return ReturnCommentsByPost(postId);
        }

        public CDbTable GetById(int commentId)
        {
            return _context.Comments.Find(commentId);
        }

        public ServiceResponse EditCommentAction(CEditData existingComment)
        {
            return ReturnEditedComment(existingComment);
        }

        public void Delete(int commentId)
        {
            CDbTable model = _context.Comments.Find(commentId);
            if (model != null)
            {
                _context.Comments.Remove(model);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}