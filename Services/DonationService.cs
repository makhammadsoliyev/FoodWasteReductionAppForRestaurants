using FoodWasteReductionAppForRestaurants.Configurations;
using FoodWasteReductionAppForRestaurants.Extensions;
using FoodWasteReductionAppForRestaurants.Helpers;
using FoodWasteReductionAppForRestaurants.Interfaces;
using FoodWasteReductionAppForRestaurants.Models.Donations;
using FoodWasteReductionAppForRestaurants.Models.Foods;
using FoodWasteReductionAppForRestaurants.Models.Restaurants;
using FoodWasteReductionAppForRestaurants.Models.Shelters;

namespace FoodWasteReductionAppForRestaurants.Services;

public class DonationService : IDonationService
{
    private List<Donation> donations;
    private readonly IFoodService foodService;
    private readonly IShelterService shelterService;
    private readonly IRestaurantService restaurantService;

    public DonationService(IRestaurantService restaurantService, IFoodService foodService, IShelterService shelterService)
    {
        this.foodService = foodService;
        this.shelterService = shelterService;
        this.restaurantService = restaurantService;
    }

    public async Task<DonationViewModel> AddAsync(DonationCreationModel model)
    {
        donations = await FileIO.ReadAsync<Donation>(Constants.DONATIONS_PATH);
        var food = await foodService.GetByIdAsync(model.FoodId);
        var shelter = await shelterService.GetByIdAsync(model.ShelterId);
        var restaurant = await restaurantService.GetByIdAsync(model.RestaurantId);

        var donation = model.ToMapMain();
        donation.Id = CollectionExtension.GenerateId(donations);

        await FileIO.WriteAsync(Constants.DONATIONS_PATH, donations);

        return donation.ToMapView(food, shelter, restaurant);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        donations = await FileIO.ReadAsync<Donation>(Constants.DONATIONS_PATH);
        var donation = donations.FirstOrDefault(d => d.Id == id && !d.IsDeleted)
            ?? throw new Exception($"Donation was not found with this id={id}");

        donation.IsDeleted = true;
        donation.DelatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.DONATIONS_PATH, donations);

        return true;
    }

    public async Task<IEnumerable<DonationViewModel>> GetAllAsync()
    {
        donations = await FileIO.ReadAsync<Donation>(Constants.DONATIONS_PATH);
        var availableDonations = donations.FindAll(d => !d.IsDeleted);

        return availableDonations.ToMap(foodService, shelterService, restaurantService);
    }

    public async Task<DonationViewModel> GetByIdAsync(long id)
    {
        donations = await FileIO.ReadAsync<Donation>(Constants.DONATIONS_PATH);
        var donation = donations.FirstOrDefault(d => d.Id == id && !d.IsDeleted)
            ?? throw new Exception($"Donation was not found with this id={id}");

        var food = await foodService.GetByIdAsync(donation.FoodId);
        var shelter = await shelterService.GetByIdAsync(donation.ShelterId);
        var restaurant = await restaurantService.GetByIdAsync(donation.RestaurantId);

        return donation.ToMapView(food, shelter, restaurant);
    }

    public async Task<DonationViewModel> UpdateAsync(long id, DonationUpdateModel model)
    {
        donations = await FileIO.ReadAsync<Donation>(Constants.DONATIONS_PATH);
        var donation = donations.FirstOrDefault(d => d.Id == id && !d.IsDeleted)
            ?? throw new Exception($"Donation was not found with this id={id}");

        var food = await foodService.GetByIdAsync(model.FoodId);
        var shelter = await shelterService.GetByIdAsync(model.ShelterId);
        var restaurant = await restaurantService.GetByIdAsync(model.RestaurantId);

        donation.Id = id;
        donation.FoodId = model.FoodId;
        donation.Quantity = model.Quantity;
        donation.ShelterId = model.ShelterId;
        donation.RestaurantId = model.RestaurantId;

        await FileIO.WriteAsync(Constants.DONATIONS_PATH, donations);

        return donation.ToMapView(food, shelter, restaurant);
    }

    public async Task<(IEnumerable<FoodViewModel>, IEnumerable<ShelterViewModel>, IEnumerable<RestaurantViewModel>)> GetAllModels()
        => (await foodService.GetAllAsync(), await shelterService.GetAllAsync(), await restaurantService.GetAllAsync());
}
