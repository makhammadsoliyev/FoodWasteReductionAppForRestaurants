using FoodWasteReductionAppForRestaurants.Models.Locations;
using FoodWasteReductionAppForRestaurants.Services;
using Newtonsoft.Json;



string apiKey = "6df2ee54900741e79ebc8d4b661db667";
string address = "1600 Amphitheatre Parkway, Mountain View, CA";
HttpClient httpClient = new HttpClient();
string apiUrl = $"https://api.opencagedata.com/geocode/v1/json?q={Uri.EscapeDataString(address)}&key={apiKey}";

var response = await httpClient.GetAsync(apiUrl);
var content = await response.Content.ReadAsStringAsync();

//Console.WriteLine(content);
var page = JsonConvert.DeserializeObject<Page>(content);
var locations = page.Locations;

ShelterService service = new ShelterService();

var s = await service.GetByIdAsync(2);

Console.WriteLine();

