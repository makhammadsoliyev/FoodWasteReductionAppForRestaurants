using FoodWasteReductionAppForRestaurants.Models.Donations;
using FoodWasteReductionAppForRestaurants.Models.Foods;
using FoodWasteReductionAppForRestaurants.Models.Restaurants;
using FoodWasteReductionAppForRestaurants.Models.Shelters;

namespace FoodWasteReductionAppForRestaurants.Interfaces;

public interface IDonationService
{
    Task<DonationViewModel> AddAsync(DonationCreationModel model);
    Task<DonationViewModel> GetByIdAsync(long id);
    Task<DonationViewModel> UpdateAsync(long id, DonationUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<DonationViewModel>> GetAllAsync();
    Task<(IEnumerable<FoodViewModel>, IEnumerable<ShelterViewModel>, IEnumerable<RestaurantViewModel>)> GetAllModels();
}
