using NewsPortal.BusinessLogic.Interfaces;


namespace NewsPortal.BusinessLogic
{
     public class BusinessLogic
     {
          public ISession GetSessionBL()
          {
               return new SessionBL();

          }
     }
}
