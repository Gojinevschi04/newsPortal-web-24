using System.Data.Entity;
using NewsPortal.Domain.Entities.Comment;

namespace NewsPortal.BusinessLogic.DbModel
{
    public class CommentaryContext: DbContext
    {
        public CommentaryContext() : base("name=WebDataBase")
        {
        }

        public virtual DbSet<CDbTable> Comments { get; set; }
    }
}