using FoodWasteReductionAppForRestaurants.Models.Commons;
using FoodWasteReductionAppForRestaurants.Models.Foods;
using FoodWasteReductionAppForRestaurants.Models.Restaurants;
using FoodWasteReductionAppForRestaurants.Models.Shelters;

namespace FoodWasteReductionAppForRestaurants.Extensions;

public static class UIHelperExtension
{
    public static List<string> GetNames(this IEnumerable<RestaurantViewModel> values)
        => values.Select(v => $"{v.Id} {v.Name}").ToList();

    public static List<string> GetNames(this IEnumerable<FoodViewModel> values)
        => values.Select(v => $"{v.Id} {v.Name}").ToList();

    public static List<string> GetNames(this IEnumerable<ShelterViewModel> values)
        => values.Select(v => $"{v.Id} {v.Name}").ToList();

    public static RestaurantViewModel GetByName(this IEnumerable<RestaurantViewModel> values, string name)
        => values.FirstOrDefault(v => $"{v.Id} {v.Name}".Equals(name));

    public static FoodViewModel GetByName(this IEnumerable<FoodViewModel> values, string name)
        => values.FirstOrDefault(v => $"{v.Id} {v.Name}".Equals(name));

    public static ShelterViewModel GetByName(this IEnumerable<ShelterViewModel> values, string name)
        => values.FirstOrDefault(v => $"{v.Id} {v.Name}".Equals(name));
}
