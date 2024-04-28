using System.Data.Entity;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.BusinessLogic.DbModel
{
    public class PostContext : DbContext
    {
        public PostContext() : base("name=WebDataBase")
        {
        }

        public virtual DbSet<PDbTable> Posts { get; set; }
    }
}