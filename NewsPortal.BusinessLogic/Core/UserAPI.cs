using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NewsPortal.BusinessLogic.DbModel;
using NewsPortal.Domain.Entities.User;
using NewsPortal.Domain.Enums;
using NewsPortal.Helpers;

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
                Level = Domain.Enums.URole.Reporter
            };

            using (var db = new UserContext())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }


            return new ULoginResp { Status = false };
        }

        public HttpCookie Cookie(string loginCredential)
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

        public UserMinimal UserCookie(string cookie)
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
                LasIp = currentUser.LastIp,
                Level = currentUser.Level
            };

            // Mapper.Initialize(cfg => cfg.CreateMap<UDbTable, UserMinimal>());
            // var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userMinimal;
        }
    }
}