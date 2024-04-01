using System.Linq;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.Domain.Entities.User;

namespace NewsPortal.BusinessLogic.Core
{
     public class UserApi
     {
          public ULoginResp UserLoginAction(ULoginData data)
          {
               UDbTable user;

               using (var db = new UserContext())
               {
                    user = db.Users.FirstOrDefault(u => u.Username == data.Credential);
               }

               return new ULoginResp();
          }
     }
}
