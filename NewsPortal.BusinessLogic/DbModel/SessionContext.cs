using System.Data.Entity;
using NewsPortal.Domain.Entities.User;

namespace NewsPortal.BusinessLogic.DbModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=WebDataBase")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}