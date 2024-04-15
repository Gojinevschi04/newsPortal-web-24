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
                    user = db.Users.FirstOrDefault(u => u.Email == data.Email);
               }


               if (user == null)
               {
                    return new ULoginResp { Status = false, StatusMsg = "User not found" };
               }
               else
               {
                    if (user.Password != data.Password)
                    {
                         return new ULoginResp { Status = false, StatusMsg = "Incorect password" };
                    }
                    return new ULoginResp { Status = true };
               }


          }

          public ULoginResp RegisterData(URegisterData data)
          {

               var newUser = new UDbTable
               {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Email = data.Email,
                    Password = data.Password,
                    Username = data.Username,
                    LastIp = data.Ip,
                    LastLogin = data.LoginDataTime,
                    Level = Domain.Enums.URole.None
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
