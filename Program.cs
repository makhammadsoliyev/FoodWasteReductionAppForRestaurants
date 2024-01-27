using Newtonsoft.Json;
using System.Net;

string apiKey = "6df2ee54900741e79ebc8d4b661db667";
string address = "Haad LC";
HttpClient httpClient = new HttpClient();
string apiUrl = $"https://api.opencagedata.com/geocode/v1/json?q={Uri.EscapeDataString(address)}&key={apiKey}";

var response = await httpClient.GetAsync(apiUrl);
var content = await response.Content.ReadAsStringAsync();

var result = JsonConvert.SerializeObject(content, Formatting.Indented);
await File.WriteAllTextAsync(@"C:\Users\User\Desktop\dotnet\FoodWasteReductionAppForRestaurants\DataBase\foods.json", result);

