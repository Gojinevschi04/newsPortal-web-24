using System.Collections.Generic;
using Gastronique.Domain.Entities.Restaurant;
using Gastronique.Domain.Entities.User;

namespace Gastronique.BusinessLogic.Interfaces
{
    public interface IRestaurant
    {
        RServiceResponse AddRestaurantAction(NewRestaurantData data);
        RDbTable GetById(int postId);
        IEnumerable<RDbTable> GetAll();
        IEnumerable<RestaurantMinimal> GetAllByUser(int userId);
        IEnumerable<RestaurantMinimal> GetRestaurantByCategory(string category);
        IEnumerable<RestaurantMinimal> GetRestaurantByKey(string key);
        RServiceResponse EditRestaurantAction(REditData existingRestaurant);
        void Delete(int restaurantId);
        void Save();
    }
}