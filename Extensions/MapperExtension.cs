using FoodWasteReductionAppForRestaurants.Interfaces;
using FoodWasteReductionAppForRestaurants.Models.Donations;
using FoodWasteReductionAppForRestaurants.Models.Foods;
using FoodWasteReductionAppForRestaurants.Models.Restaurants;
using FoodWasteReductionAppForRestaurants.Models.Shelters;

namespace FoodWasteReductionAppForRestaurants.Extensions;

public static class MapperExtension
{
    #region Restaurant mappers
    public static List<RestaurantViewModel> ToMap(this List<Restaurant> models)
        => models.Select(m => m.ToMapView()).ToList();

    public static Restaurant ToMapMain(this RestaurantCreationModel model)
    {
        return new Restaurant()
        {
            Name = model.Name,
            Location = model.Location,
        };
    }

    public static Restaurant ToMapMain(this RestaurantUpdateModel model)
    {
        return new Restaurant()
        {
            Name = model.Name,
            Location = model.Location,
        };
    }

    public static RestaurantViewModel ToMapView(this Restaurant model)
    {
        return new RestaurantViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            Address = model.Location.Formatted,
            Latitude = model.Location.Geometry.Latitude,
            Longitude = model.Location.Geometry.Longitude,
        };
    }
    #endregion

    #region Shelter mappers
    public static List<ShelterViewModel> ToMap(this List<Shelter> models)
        => models.Select(m => m.ToMapView()).ToList();

    public static Shelter ToMapMain(this ShelterCreationModel model)
    {
        return new Shelter()
        {
            Name = model.Name,
            Location = model.Location,
            Description = model.Description,
            NumberOfPeople = model.NumberOfPeople,
        };
    }

    public static Shelter ToMapMain(this ShelterUpdateModel model)
    {
        return new Shelter()
        {
            Name = model.Name,
            Location = model.Location,
            Description = model.Description,
            NumberOfPeople = model.NumberOfPeople,
        };
    }

    public static ShelterViewModel ToMapView(this Shelter model)
    {
        return new ShelterViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Address = model.Location.Formatted,
            NumberOfPeople = model.NumberOfPeople,
            Latitude = model.Location.Geometry.Latitude,
            Longitude = model.Location.Geometry.Longitude,
        };
    }
    #endregion

    #region Food mappers
    public static List<FoodViewModel> ToMap(this List<Food> models)
        => models.Select(m => m.ToMapView()).ToList();

    public static Food ToMapMain(this FoodCreationModel model)
    {
        return new Food()
        {
            Name = model.Name,
            Quantity = model.Quantity,
            Description = model.Description,
        };
    }

    public static Food ToMapMain(this FoodUpdateModel model)
    {
        return new Food()
        {
            Name = model.Name,
            Quantity = model.Quantity,
            Description = model.Description,
        };
    }

    public static FoodViewModel ToMapView(this Food model)
    {
        return new FoodViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            Quantity = model.Quantity,
            Description = model.Description,
        };
    }
    #endregion

    #region Donation mappers
    public static List<DonationViewModel> ToMap(
        this List<Donation> models,
        IFoodService foodService,
        IShelterService shelterService,
        IRestaurantService restaurantService)
    {
        var result = new List<DonationViewModel>();
        foreach(var m in models)
        {
            var food = foodService.GetByIdAsync(m.FoodId);
            var shelter = shelterService.GetByIdAsync(m.ShelterId);
            var restaurant = restaurantService.GetByIdAsync(m.RestaurantId);

            result.Add(m.ToMapView(food.Result, shelter.Result, restaurant.Result));
        }

        return result;
    }


    public static Donation ToMapMain(this DonationCreationModel model)
    {
        return new Donation()
        {
            FoodId = model.FoodId,
            Quantity = model.Quantity,
            ShelterId = model.ShelterId,
            RestaurantId = model.RestaurantId,
        };
    }

    public static Donation ToMapMain(this DonationUpdateModel model)
    {
        return new Donation()
        {
            FoodId = model.FoodId,
            Quantity = model.Quantity,
            ShelterId = model.ShelterId,
            RestaurantId = model.RestaurantId,
        };
    }

    public static DonationViewModel ToMapView(
        this Donation donationModel,
        FoodViewModel foodModel,
        ShelterViewModel shelterModel,
        RestaurantViewModel restaurantModel)
    {
        return new DonationViewModel()
        {
            Food = foodModel,
            Id = donationModel.Id,
            Shelter = shelterModel,
            Restaurant = restaurantModel,
            Quantity = donationModel.Quantity,
        };
    }
    #endregion
}
