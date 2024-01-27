using FoodWasteReductionAppForRestaurants.Models.Restaurants;

namespace FoodWasteReductionAppForRestaurants.Models.Foods;

public class FoodViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Quantity { get; set; }
    public Restaurant Restaurant { get; set; }
}
