using System.Linq;
using NewsPortal.Domain.Entities.User;

namespace NewsPortal.BusinessLogic.Core
{
     public class UserApi
     {
          public ULoginResp UserLoginAction(ULoginData data)
          {
               return new ULoginResp();
          }
     }
}
