using NewsPortal.BusinessLogic.Core;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.User;
using System.Web;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewsPortal.BusinessLogic
{
     public class SessionBL: UserApi, ISession    
     {
          public ULoginResp UserLogin(ULoginData data)
          {
               return UserLoginAction(data);
          }

          public ULoginResp URegisterAction(URegisterData data)
          {
               return RegisterData(data);
          }
          
          public HttpCookie GenCookie(string loginCredential)
          {
               return Cookie(loginCredential);
          }

          public UserMinimal GetUserByCookie(string apiCookieValue)
          {
               return UserCookie(apiCookieValue);
          }
     }
}
