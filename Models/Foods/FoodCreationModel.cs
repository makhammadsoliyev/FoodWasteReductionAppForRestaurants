namespace FoodWasteReductionAppForRestaurants.Models.Foods;

public class FoodCreationModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Quantity { get; set; }
    public long RestaurantId { get; set; }
}
