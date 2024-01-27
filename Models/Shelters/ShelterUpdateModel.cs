using FoodWasteReductionAppForRestaurants.Models.Locations;

namespace FoodWasteReductionAppForRestaurants.Models.Shelters;

public class ShelterUpdateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Location Location { get; set; }
    public int NumberOfPeople { get; set; }
}
