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

          public ULoginResp RegisterData(URegisterData data)
          {

               var newUser = new UDbTable
               {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password
               };

               using (var db = new UserContext())
               {
                    db.Users.Add(newUser);
                    db.SaveChanges();
               }



               return new ULoginResp { Status = false };
          }

     }
}
