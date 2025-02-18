using Gastronique.BusinessLogic.Interfaces;


namespace Gastronique.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IRestaurant GetRestaurantBL()
        {
            return new RestaurantBL();
        }

        public IComment GetCommentBL()
        {
            return new CommentBL();
        }
    }
}