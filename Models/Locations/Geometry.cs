using Newtonsoft.Json;

namespace FoodWasteReductionAppForRestaurants.Models.Locations;

public class Geometry
{
    [JsonProperty("lat")]
    public string Latitude { get; set; }

    [JsonProperty("lng")]
    public string Longitude { get; set; }
}
