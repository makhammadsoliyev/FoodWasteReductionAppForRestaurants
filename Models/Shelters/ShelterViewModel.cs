namespace FoodWasteReductionAppForRestaurants.Models.Shelters;

public class ShelterViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public int NumberOfPeople { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}
