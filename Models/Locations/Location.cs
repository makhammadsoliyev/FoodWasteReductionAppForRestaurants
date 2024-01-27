using Newtonsoft.Json;

namespace FoodWasteReductionAppForRestaurants.Models.Locations;

public class Location
{
    [JsonProperty("components")]
    public List<Component> Components { get; set; }

    [JsonProperty("confidence")]
    public int Confidence { get; set; }

    [JsonProperty("formatted")]
    public string Formatted { get; set; }

    [JsonProperty("geometry")]
    public Geometry Geometry { get; set; }
}