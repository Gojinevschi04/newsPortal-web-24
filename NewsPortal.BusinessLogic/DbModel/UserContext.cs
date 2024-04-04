using System.Data.Entity;
using NewsPortal.Domain.Entities.User;

namespace NewsPortal.BusinessLogic.DbModel
{
    public class UserContext: DbContext
    {
        public UserContext() : 
            base("name=WebDataBase") // connectionstring name define in your web.config
        {
        }

        public virtual DbSet<UDbTable> Users { get; set; }
    }
}