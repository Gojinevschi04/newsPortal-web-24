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

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
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
            UDbTable curentUser;

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
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                }
            }

            if (curentUser == null) return null;

            var userminimal = new UserMinimal
            {
                Id = curentUser.Id,
                Email = curentUser.Email,
                LastLogin = DateTime.Now,
                LasIp = "0.0.0.0",
                Level = URole.User
            };

            // Mapper.Initialize(cfg => cfg.CreateMap<UDbTable, UserMinimal>());
            // var userminimal = Mapper.Map<UserMinimal>(curentUser);

            return userminimal;
        }
    }
}