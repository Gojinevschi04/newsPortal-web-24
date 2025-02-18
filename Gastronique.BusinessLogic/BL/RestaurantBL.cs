using System.Collections.Generic;
using System.Linq;
using Gastronique.BusinessLogic.Core;
using Gastronique.BusinessLogic.DbModel;
using Gastronique.BusinessLogic.Interfaces;
using Gastronique.Domain.Entities.Restaurant;
using Gastronique.Domain.Entities.User;

namespace Gastronique.BusinessLogic
{
    public class RestaurantBL : RestaurantApi, IRestaurant
    {
        private readonly PostContext _context;

        public RestaurantBL()
        {
            _context = new PostContext();
        }

        public RestaurantBL(PostContext context)
        {
            _context = context;
        }

        public RServiceResponse AddRestaurantAction(NewRestaurantData data)
        {
            return AddPost(data);
        }

        public RDbTable GetById(int postId)
        {
            return _context.Posts.Find(postId);
        }

        public IEnumerable<RDbTable> GetAll()
        {
            return _context.Posts.ToList();
        }

        public IEnumerable<RestaurantMinimal> GetAllByUser(int userId)
        {
            return ReturnPostsByAuthor(userId);
        }

        public IEnumerable<RestaurantMinimal> GetRestaurantByCategory(string category)
        {
            return ReturnPostsByCategory(category);
        }

        public IEnumerable<RestaurantMinimal> GetRestaurantByKey(string key)
        {
            return ReturnPostsByKey(key);
        }

        public void Delete(int restaurantId)
        {
            RDbTable model = _context.Posts.Find(restaurantId);
            if (model != null)
            {
                _context.Posts.Remove(model);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public RServiceResponse EditRestaurantAction(REditData existingRestaurant)
        {
            return ReturnEditedPost(existingRestaurant);
        }
    }
}