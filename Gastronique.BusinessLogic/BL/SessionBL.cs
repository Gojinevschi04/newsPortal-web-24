using Gastronique.Domain.Entities.User;
using System.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Gastronique.BusinessLogic.Core;
using Gastronique.BusinessLogic.DbModel;
using Gastronique.BusinessLogic.Interfaces;
using Gastronique.Domain.Entities.Restaurant;

namespace Gastronique.BusinessLogic
{
     public class SessionBL : UserApi, ISession
     {
          private readonly UserContext _context;
          public SessionBL()
          {
               _context = new UserContext();
          }
          public SessionBL(UserContext context)
          {
               _context = context;
          }
          public IEnumerable<UDbTable> GetAll()
          {
               return _context.Users.ToList();
          }
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
          public UEditData GetUserById(int userId)
          {
               return ReturnUserById(userId);
          }
          public UServiceResponse EditProfileAction(UEditData existingUser)
          {
               return ReturnEditedProfile(existingUser);
          }
          public UServiceResponse ChangePassword(UChangePasswordData password)
          {
               return ReturnChangedPassword(password);
          }
     }
}