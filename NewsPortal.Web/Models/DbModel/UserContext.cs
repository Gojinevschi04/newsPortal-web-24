using System.Data.Entity;

namespace NewsPortal.Web.Models.DbModel
{
    public class UserContext: DbContext
    {
        public UserContext() : 
            base("name=WebDataBase") // connectionstring name define in your web.config
        {
        }
    }
}