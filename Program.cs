using FoodWasteReductionAppForRestaurants.Models.Locations;
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

var result = JsonConvert.SerializeObject(locations, Formatting.Indented);
await File.WriteAllTextAsync(@"C:\Users\User\Desktop\dotnet\FoodWasteReductionAppForRestaurants\DataBase\foods.json", result);

