using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NewsPortal.Domain.Entities.Post;
using NewsPortal.Domain.Entities.User;

namespace NewsPortal.BusinessLogic.Interfaces
{
     public interface ISession
     {
          IEnumerable<UDbTable> GetAll();
          ULoginResp UserLogin(ULoginData data);
          ULoginResp URegisterAction(URegisterData data);
          HttpCookie GenCookie(string loginCredential);
          UserMinimal GetUserByCookie(string apiCookieValue);
          UEditData GetUserById(int userId);
          ServiceResponse EditProfileAction(UEditData existingUser);
          ServiceResponse ChangePassword(UChangePasswordData password);
     }
}
