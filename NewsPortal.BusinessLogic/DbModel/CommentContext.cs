using System.Data.Entity;
using NewsPortal.Domain.Entities.Comment;

namespace NewsPortal.BusinessLogic.DbModel
{
    public class CommentContext : DbContext
    {
        public CommentContext() : base("name=WebDataBase")
        {
        }

        public virtual DbSet<CDbTable> Comments { get; set; }
    }
}