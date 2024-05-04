using NewsPortal.BusinessLogic.Interfaces;


namespace NewsPortal.BusinessLogic
{
     public class BusinessLogic
     {
          public ISession GetSessionBL()
          {
               return new SessionBL();
          }
          
          public IPost GetPostBL()
          {
               return new PostBL();
          }
          
          public ICommentary GetCommentaryBL()
          {
               return new CommentaryBL();
          }
     }
}
