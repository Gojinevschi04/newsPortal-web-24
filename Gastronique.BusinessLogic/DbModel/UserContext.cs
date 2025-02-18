using System.Data.Entity;
using Gastronique.Domain.Entities.User;

namespace Gastronique.BusinessLogic.DbModel
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