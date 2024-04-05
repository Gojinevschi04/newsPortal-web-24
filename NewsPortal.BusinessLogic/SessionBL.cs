using NewsPortal.BusinessLogic.Core;
using NewsPortal.BusinessLogic.Interfaces;
using NewsPortal.Domain.Entities.User;


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
     }
}
