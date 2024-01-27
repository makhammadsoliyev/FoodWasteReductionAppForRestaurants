using FoodWasteReductionAppForRestaurants.Configurations;
using FoodWasteReductionAppForRestaurants.Extensions;
using FoodWasteReductionAppForRestaurants.Helpers;
using FoodWasteReductionAppForRestaurants.Interfaces;
using FoodWasteReductionAppForRestaurants.Models.Restaurants;

namespace FoodWasteReductionAppForRestaurants.Services;

public class RestaurantService : IRestaurantService
{
    private List<Restaurant> restaurants;

    public async Task<RestaurantViewModel> AddAsync(RestaurantCreationModel model)
    {
        restaurants = await FileIO.ReadAsync<Restaurant>(Constants.RESTAURANTS_PATH);
        var restaurant = model.ToMapMain();
        restaurant.Id = CollectionExtension.GenerateId(restaurants);

        restaurants.Add(restaurant);

        await FileIO.WriteAsync(Constants.RESTAURANTS_PATH, restaurants);

        return restaurant.ToMapView();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        restaurants = await FileIO.ReadAsync<Restaurant>(Constants.RESTAURANTS_PATH);
        var restaurant = restaurants.FirstOrDefault(r => r.Id == id && !r.IsDeleted)
            ?? throw new Exception($"Restaurant was not found with this id={id}");

        restaurant.IsDeleted = true;
        restaurant.DelatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.RESTAURANTS_PATH, restaurants);

        return true;
    }

    public async Task<IEnumerable<RestaurantViewModel>> GetAllAsync()
    {
        restaurants = await FileIO.ReadAsync<Restaurant>(Constants.RESTAURANTS_PATH);
        var availableRestaurants = restaurants.FindAll(r => !r.IsDeleted);

        return availableRestaurants.ToMap();
    }

    public async Task<RestaurantViewModel> GetByIdAsync(long id)
    {
        restaurants = await FileIO.ReadAsync<Restaurant>(Constants.RESTAURANTS_PATH);
        var restaurant = restaurants.FirstOrDefault(r => r.Id == id && !r.IsDeleted)
            ?? throw new Exception($"Restaurant was not found with this id={id}");

        return restaurant.ToMapView();
    }

    public async Task<RestaurantViewModel> UpdateAsync(long id, RestaurantUpdateModel model)
    {
        restaurants = await FileIO.ReadAsync<Restaurant>(Constants.RESTAURANTS_PATH);
        var restaurant = restaurants.FirstOrDefault(r => r.Id == id && !r.IsDeleted)
            ?? throw new Exception($"Restaurant was not found with this id={id}");

        restaurant.Id = id;
        restaurant.Name = model.Name;
        restaurant.Location = model.Location;
        restaurant.UpdatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.RESTAURANTS_PATH, restaurants);

        return restaurant.ToMapView();
    }
}
