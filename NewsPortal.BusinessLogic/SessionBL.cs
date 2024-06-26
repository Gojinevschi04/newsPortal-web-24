﻿using NewsPortal.BusinessLogic.Core;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.User;
using System.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.BusinessLogic
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
          public ServiceResponse EditProfileAction(UEditData existingUser)
          {
               return ReturnEditedProfile(existingUser);
          }
          public ServiceResponse ChangePassword(UChangePasswordData password)
          {
               return ReturnChangedPassword(password);
          }
     }
}