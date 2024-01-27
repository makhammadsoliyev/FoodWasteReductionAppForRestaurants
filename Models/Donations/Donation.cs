using FoodWasteReductionAppForRestaurants.Models.Commons;

namespace FoodWasteReductionAppForRestaurants.Models.Donations;

public class Donation : Auditable
{
    public long RestaurantId { get; set; }
    public long FoodId { get; set; }
    public decimal Quantity { get; set; }
    public long ShelterId { get; set; }
}