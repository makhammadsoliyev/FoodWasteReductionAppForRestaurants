using FoodWasteReductionAppForRestaurants.Models.Foods;

namespace FoodWasteReductionAppForRestaurants.Interfaces;

public interface IFoodService
{
    Task<FoodViewModel> AddAsync(FoodCreationModel model);
    Task<FoodViewModel> GetByIdAsync(long id);
    Task<FoodViewModel> UpdateAsync(long id, FoodUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<FoodViewModel>> GetAllAsync();
}
