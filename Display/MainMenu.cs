using FoodWasteReductionAppForRestaurants.Interfaces;
using FoodWasteReductionAppForRestaurants.Services;
using Spectre.Console;

namespace FoodWasteReductionAppForRestaurants.Display;

public class MainMenu
{
    private readonly IFoodService foodService;
    private readonly IShelterService shelterService;
    private readonly IDonationService donationService;
    private readonly IRestaurantService restaurantService;

    private readonly FoodMenu foodMenu;
    private readonly ShelterMenu shelterMenu;
    private readonly DonationMenu donationMenu;
    private readonly RestaurantMenu restaurantMenu;

    public MainMenu()
    {
        foodService = new FoodService();
        shelterService = new ShelterService();
        restaurantService = new RestaurantService();
        donationService = new DonationService(restaurantService, foodService, shelterService);

        foodMenu = new FoodMenu(foodService);
        shelterMenu = new ShelterMenu(shelterService);
        donationMenu = new DonationMenu(donationService);
        restaurantMenu = new RestaurantMenu(restaurantService);
    }

    public async Task Main()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", new string[] { "Food", "Shelter", "Donation", "Restaurant", "Exit" });

            switch (selection)
            {
                case "Food":
                    await foodMenu.Display();
                    break;
                case "Shelter":
                    await shelterMenu.Display();
                    break;
                case "Donation":
                    await donationMenu.Display();
                    break;
                case "Restaurant":
                    await restaurantMenu.Display();
                    break;
                case "Exit":
                    circle = false;
                    break;
            }
        }
    }
}
