using FoodWasteReductionAppForRestaurants.Models.Foods;
using FoodWasteReductionAppForRestaurants.Models.Restaurants;
using FoodWasteReductionAppForRestaurants.Models.Shelters;

namespace FoodWasteReductionAppForRestaurants.Models.Donations;

public class DonationViewModel
{
    public long Id { get; set; }
    public RestaurantViewModel Restaurant { get; set; }
    public FoodViewModel Food { get; set; }
    public decimal Quantity { get; set; }
    public ShelterViewModel Shelter { get; set; }
}
