using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Gastronique.Domain.Entities.Restaurant;
using Gastronique.Domain.Entities.User;

namespace Gastronique.BusinessLogic.Interfaces
{
     public interface ISession
     {
          IEnumerable<UDbTable> GetAll();
          ULoginResp UserLogin(ULoginData data);
          ULoginResp URegisterAction(URegisterData data);
          HttpCookie GenCookie(string loginCredential);
          UserMinimal GetUserByCookie(string apiCookieValue);
          UEditData GetUserById(int userId);
          UServiceResponse EditProfileAction(UEditData existingUser);
          UServiceResponse ChangePassword(UChangePasswordData password);
     }
}
