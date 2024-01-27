using FoodWasteReductionAppForRestaurants.Extensions;
using FoodWasteReductionAppForRestaurants.Interfaces;
using FoodWasteReductionAppForRestaurants.Models.Donations;
using FoodWasteReductionAppForRestaurants.Models.Foods;
using FoodWasteReductionAppForRestaurants.Models.Restaurants;
using FoodWasteReductionAppForRestaurants.Models.Shelters;
using Spectre.Console;

namespace FoodWasteReductionAppForRestaurants.Display;

public class DonationMenu
{
    private readonly IDonationService donationService;
    private IEnumerable<FoodViewModel> foods;
    private IEnumerable<ShelterViewModel> shelters;
    private IEnumerable<RestaurantViewModel> restaurants;

    public DonationMenu(IDonationService donationService)
    {
        this.donationService = donationService;
    }

    private async Task Add()
    {
        (foods, shelters, restaurants) = await donationService.GetAllModels();
        var foodNames = foods.GetNames();
        var shelterNames = shelters.GetNames();
        var restaurantNames = restaurants.GetNames();

        foodNames.Add("Change food");
        shelterNames.Add("Change shelter");
        restaurantNames.Add("Change restaurant");

        var selectionDisplay = new SelectionMenu();
        RestaurantViewModel restaurant;
    key1:
        var selection1 = selectionDisplay.ShowSelectionMenu("Choose one of restaurants", restaurantNames.ToArray());

        if (selection1.Equals("Change restaurant"))
            goto key1;
        else
            restaurant = restaurants.GetByName(selection1);

        FoodViewModel food;
    key2:
        var selection2 = selectionDisplay.ShowSelectionMenu("Choose one of foods", foodNames.ToArray());

        if (selection2.Equals("Change food"))
            goto key2;
        else
            food = foods.GetByName(selection2);

        ShelterViewModel shelter;
    key3:
        var selection3 = selectionDisplay.ShowSelectionMenu("Choose one of foods", foodNames.ToArray());

        if (selection3.Equals("Change shelter"))
            goto key3;
        else
            shelter = shelters.GetByName(selection3);

        var donation = new DonationCreationModel()
        {
            FoodId = food.Id,
            ShelterId = shelter.Id,
            Quantity = food.Quantity,
            RestaurantId = restaurant.Id,
        };

        try
        {
            var addedDonation = await donationService.AddAsync(donation);
            AnsiConsole.MarkupLine("[green]Successfully added...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        await Task.Delay(2000);

    }

    private async Task GetById()
    {

    }

    private async Task Update()
    {

    }

    private async Task Delete()
    {

    }

    private async Task GetAll()
    {

    }

    public async Task Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "Back" });

            switch (selection)
            {
                case "Add":
                    await Add();
                    break;
                case "GetById":
                    await GetById();
                    break;
                case "Update":
                    await Update();
                    break;
                case "Delete":
                    await Delete();
                    break;
                case "GetAll":
                    await GetAll();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
