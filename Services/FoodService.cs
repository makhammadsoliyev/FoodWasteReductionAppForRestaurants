using FoodWasteReductionAppForRestaurants.Configurations;
using FoodWasteReductionAppForRestaurants.Extensions;
using FoodWasteReductionAppForRestaurants.Helpers;
using FoodWasteReductionAppForRestaurants.Interfaces;
using FoodWasteReductionAppForRestaurants.Models.Foods;

namespace FoodWasteReductionAppForRestaurants.Services;

public class FoodService : IFoodService
{
    private List<Food> foods;

    public async Task<FoodViewModel> AddAsync(FoodCreationModel model)
    {
        foods = await FileIO.ReadAsync<Food>(Constants.FOODS_PATH);
        var food = model.ToMapMain();
        food.Id = CollectionExtension.GenerateId(foods);

        foods.Add(food);

        await FileIO.WriteAsync(Constants.FOODS_PATH, foods);

        return food.ToMapView();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        foods = await FileIO.ReadAsync<Food>(Constants.FOODS_PATH);
        var food = foods.FirstOrDefault(f => f.Id == id && !f.IsDeleted)
            ?? throw new Exception($"Food was not found with this id={id}");

        food.IsDeleted = true;
        food.DelatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.FOODS_PATH, foods);

        return true;
    }

    public async Task<IEnumerable<FoodViewModel>> GetAllAsync()
    {
        foods = await FileIO.ReadAsync<Food>(Constants.FOODS_PATH);
        var availableFoods = foods.FindAll(f => !f.IsDeleted);

        return availableFoods.ToMap();
    }

    public async Task<FoodViewModel> GetByIdAsync(long id)
    {
        foods = await FileIO.ReadAsync<Food>(Constants.FOODS_PATH);
        var food = foods.FirstOrDefault(f => f.Id == id && !f.IsDeleted)
            ?? throw new Exception($"Food was not found with this id={id}");

        return food.ToMapView();
    }

    public async Task<FoodViewModel> UpdateAsync(long id, FoodUpdateModel model)
    {
        foods = await FileIO.ReadAsync<Food>(Constants.FOODS_PATH);
        var food = foods.FirstOrDefault(f => f.Id == id && !f.IsDeleted)
            ?? throw new Exception($"Food was not found with this id={id}");

        food.Name = model.Name;
        food.UpdatedAt = DateTime.UtcNow;
        food.Description = model.Description;

        await FileIO.WriteAsync(Constants.FOODS_PATH, foods);

        return food.ToMapView();
    }
}
