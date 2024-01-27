namespace FoodWasteReductionAppForRestaurants.Models.Donations;

public class DonationUpdateModel
{
    public long RestaurantId { get; set; }
    public long FoodId { get; set; }
    public decimal Quantity { get; set; }
    public long ShelterId { get; set; }
}
