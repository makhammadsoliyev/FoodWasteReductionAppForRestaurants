using FoodWasteReductionAppForRestaurants.Models.Restaurants;

namespace FoodWasteReductionAppForRestaurants.Interfaces;

public interface IRestaurantService
{
    Task<RestaurantViewModel> AddAsync(RestaurantCreationModel model);
    Task<RestaurantViewModel> GetByIdAsync(long id);
    Task<RestaurantViewModel> UpdateAsync(long id, RestaurantUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<RestaurantViewModel>> GetAllAsync();

}
