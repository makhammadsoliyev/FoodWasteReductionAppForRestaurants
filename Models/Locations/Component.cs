using Newtonsoft.Json;

namespace FoodWasteReductionAppForRestaurants.Models.Locations;

public class Component
{
    [JsonProperty("building")]
    public string Building { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("continent")]
    public string Continent { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("country_code")]
    public string CountryCode { get; set; }

    [JsonProperty("county")]
    public string County { get; set; }

    [JsonProperty("house_number")]
    public int HouseNumber { get; set; }

    [JsonProperty("neighbourhood")]
    public string Neighborhood { get; set; }

    [JsonProperty("postcode")]
    public string Postcode { get; set; }

    [JsonProperty("road")]
    public string Road { get; set; }
}

