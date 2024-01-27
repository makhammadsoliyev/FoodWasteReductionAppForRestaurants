using FoodWasteReductionAppForRestaurants.Models.Shelters;

namespace FoodWasteReductionAppForRestaurants.Interfaces;

public interface IShelterService
{
    Task<ShelterViewModel> AddAsync(ShelterCreationModel model);
    Task<ShelterViewModel> GetByIdAsync(long id);
    Task<ShelterViewModel> UpdateAsync(long id, ShelterUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<ShelterViewModel>> GetAllAsync();
}
