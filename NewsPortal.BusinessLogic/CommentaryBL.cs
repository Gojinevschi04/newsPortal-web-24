using System.Collections.Generic;
using System.Linq;
using NewsPortal.BusinessLogic.Core;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.Comment;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.BusinessLogic
{
    public class CommentaryBL : CommentaryApi, ICommentary
    {
        private readonly CommentaryContext _context;

        public CommentaryBL()
        {
            _context = new CommentaryContext();
        }

        public CommentaryBL(CommentaryContext context)
        {
            _context = context;
        }

        public IEnumerable<CommentaryMinimal> GetAll()
        {
            return ReturnAllCommentaries();
        }

        public ServiceResponse AddCommentaryAction(NewCommentaryData data)
        {
            return AddCommentary(data);
        }

        public IEnumerable<CommentaryMinimal> GetAllCommentsByPost(int postId)
        {
            return ReturnCommentariesByPost(postId);
        }

        public CDbTable GetById(int commentId)
        {
            return _context.Comments.Find(commentId);
        }
        
        public ServiceResponse EditCommentaryAction(CEditData existingCommentary)
        {
            return ReturnEditedCommentary(existingCommentary);
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