using FoodWasteReductionAppForRestaurants.Models.Donations;

namespace FoodWasteReductionAppForRestaurants.Interfaces;

public interface IDonationService
{
    Task<DonationViewModel> AddAsync(DonationCreationModel model);
    Task<DonationViewModel> GetByIdAsync(long id);
    Task<DonationViewModel> UpdateAsync(long id, DonationUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<DonationViewModel>> GetAllAsync();
}
