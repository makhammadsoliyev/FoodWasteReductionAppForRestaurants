using FoodWasteReductionAppForRestaurants.Models.Commons;
using FoodWasteReductionAppForRestaurants.Models.Locations;

namespace FoodWasteReductionAppForRestaurants.Models.Restaurants;

public class Restaurant : Auditable
{
    public string Name { get; set; }
    public Location Location { get; set; }
}


