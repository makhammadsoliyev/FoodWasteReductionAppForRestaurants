using FoodWasteReductionAppForRestaurants.Models.Commons;

namespace FoodWasteReductionAppForRestaurants.Models.Foods;

public class Food : Auditable
{
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Description { get; set; }
}