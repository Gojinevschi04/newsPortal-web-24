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
        
        public ServiceResponse AddCommentaryAction(NewCommentaryData data)
        {
            return AddCommentary(data);
        }
        
        public IEnumerable<CommentaryMinimal> GetAllCommentsByPost(int postId)
        {
            return ReturnCommentariesByPost(postId);
        }
    }
}