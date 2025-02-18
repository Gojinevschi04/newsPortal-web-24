using System.Data.Entity;
using Gastronique.Domain.Entities.Comment;

namespace Gastronique.BusinessLogic.DbModel
{
    public class CommentContext : DbContext
    {
        public CommentContext() : base("name=WebDataBase")
        {
        }

        public virtual DbSet<CDbTable> Comments { get; set; }
    }
}