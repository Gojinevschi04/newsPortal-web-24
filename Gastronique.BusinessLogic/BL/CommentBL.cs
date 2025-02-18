using System.Collections.Generic;
using Gastronique.BusinessLogic.Core;
using Gastronique.BusinessLogic.DbModel;
using Gastronique.BusinessLogic.Interfaces;
using Gastronique.Domain.Entities.Comment;
using Gastronique.Domain.Entities.Restaurant;

namespace Gastronique.BusinessLogic
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

        public RServiceResponse AddCommentAction(NewCommentData data)
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

        public RServiceResponse EditCommentAction(CEditData existingComment)
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