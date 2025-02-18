using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Gastronique.BusinessLogic.DbModel;
using Gastronique.Domain.Entities.Restaurant;
using Gastronique.Domain.Entities.User;
using Gastronique.Domain.Enums;
using Gastronique.Helpers;

namespace Gastronique.BusinessLogic.Core
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
                    if (user.Password != LoginHelper.HashGen(data.Password))
                    {
                         return new ULoginResp { Status = false, StatusMsg = "Incorrect password or username" };
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
                    Password = LoginHelper.HashGen(data.Password),
                    Username = data.Username,
                    LastIp = data.Ip,
                    LastLogin = data.LoginDataTime,
                    Level = URole.User
               };

               using (var db = new UserContext())
               {
                    db.Users.Add(newUser);
                    db.SaveChanges();
               }


               return new ULoginResp { Status = false };
          }

          internal HttpCookie Cookie(string loginCredential)
          { 
               var apiCookie = new HttpCookie("X-KEY")
               {
                    Value = CookieGenerator.Create(loginCredential)
               };

               using (var db = new SessionContext())
               {
                    Session current;
                    var validate = new EmailAddressAttribute();
                    if (validate.IsValid(loginCredential))
                    {
                         current = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                    }
                    else
                    {
                         current = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                    }

                    if (current != null)
                    {
                         current.CookieString = apiCookie.Value;
                         current.ExpireTime = DateTime.Now.AddMinutes(60);
                         using (var todo = new SessionContext())
                         {
                              todo.Entry(current).State = EntityState.Modified;
                              todo.SaveChanges();
                         }
                    }
                    else
                    {
                         db.Sessions.Add(new Session
                         {
                              Username = loginCredential,
                              CookieString = apiCookie.Value,
                              ExpireTime = DateTime.Now.AddMinutes(60)
                         });
                         db.SaveChanges();
                    }
               }

               return apiCookie;
          }

          internal UserMinimal UserCookie(string cookie)
          {
               Session session;
               UDbTable currentUser;

               using (var db = new SessionContext())
               {
                    session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
               }

               if (session == null) return null;
               using (var db = new UserContext())
               {
                    var validate = new EmailAddressAttribute();
                    if (validate.IsValid(session.Username))
                    {
                         currentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                    }
                    else
                    {
                         currentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                    }
               }

               if (currentUser == null) return null;

               var userMinimal = new UserMinimal
               {
                    Id = currentUser.Id,
                    Email = currentUser.Email,
                    LastLogin = DateTime.Now,
                    LastIp = currentUser.LastIp,
                    Level = currentUser.Level
               };

               // Mapper.Initialize(cfg => cfg.CreateMap<UDbTable, UserMinimal>());
               // var userminimal = Mapper.Map<UserMinimal>(curentUser);

               return userMinimal;
          }
          public UEditData ReturnUserById(int userId)
          {
               using (var db = new UserContext())
               {
                    var user = db.Users.Find(userId);
                    if (user != null)
                    {
                         var foundUser = new UEditData()
                         {
                              Username = user.Username,
                              Email = user.Email,
                              FirstName = user.FirstName,
                              LastName = user.LastName,
                              Level = user.Level,
                              Id = user.Id
                         };
                         return foundUser;
                    }
                    else
                    {
                         return null;
                    }
               }
          }
          public UServiceResponse ReturnEditedProfile(UEditData existingUser)
          {
               var response = new UServiceResponse();
               using (var db = new UserContext())
               {
                    try
                    {
                         var userToEdit = db.Users.Find(existingUser.Id);
                         if (userToEdit != null)
                         {
                              userToEdit.Id = existingUser.Id;
                              userToEdit.Username = existingUser.Username;
                              userToEdit.Email = existingUser.Email;
                              userToEdit.FirstName = existingUser.FirstName;
                              userToEdit.LastName = existingUser.LastName;
                              userToEdit.Level = existingUser.Level;

                              db.SaveChanges();
                              response.Status = true;
                              response.UserId = userToEdit.Id;
                              response.StatusMessage = "User Profile was edited successfully!";
                         }
                         else
                         {
                              response.UserId = 0;
                              response.Status = false;
                              response.StatusMessage = "User not found!";
                         }
                    }
                    catch (Exception ex)
                    {
                         response.UserId = 0;
                         response.Status = false;
                         response.StatusMessage = "An error occurred!";
                    }
               }

               return response;
          }
          public UServiceResponse ReturnChangedPassword(UChangePasswordData password)
          {
               using (var db = new UserContext())
               {
                    try
                    {
                         var user = db.Users.Find(password.Id);

                         if (user == null || user.Password != LoginHelper.HashGen(password.OldPassword))
                              return new UServiceResponse { Status = false, StatusMessage = "An error occurred!" };
                         user.Password = LoginHelper.HashGen(password.NewPassword);
                         db.SaveChanges();
                         return new UServiceResponse()
                         { Status = true, StatusMessage = "Password has been changed successfully!" };
                    }
                    catch
                    {
                         return new UServiceResponse { Status = false, StatusMessage = "An error occurred!" };
                    }
               }
          }
     }
}