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
        if (!restaurants.Any())
        {
            AnsiConsole.MarkupLine($"[red]First, There must be restaurants[/]");
            goto key;
        }

        if (!foods.Any())
        {
            AnsiConsole.MarkupLine($"[red]First, There must be foods[/]");
            goto key;
        }

        if (!shelters.Any())
        {
            AnsiConsole.MarkupLine($"[red]First, There must be shelters[/]");
            goto key;
        }

        var foodNames = foods.GetNames();
        var shelterNames = shelters.GetNames();
        var restaurantNames = restaurants.GetNames();

        var selectionDisplay = new SelectionMenu();
        RestaurantViewModel restaurant;
        var selection1 = selectionDisplay.ShowSelectionMenu("Choose one of restaurants", restaurantNames.ToArray());

        restaurant = restaurants.GetByName(selection1);

        FoodViewModel food;
        var selection2 = selectionDisplay.ShowSelectionMenu("Choose one of foods", foodNames.ToArray());

        food = foods.GetByName(selection2);

        ShelterViewModel shelter;
        var selection3 = selectionDisplay.ShowSelectionMenu("Choose one of foods", shelterNames.ToArray());

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
    key:
        await Task.Delay(2000);

    }

    private async Task GetById()
    {
        long id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        }

        try
        {
            var donation = await donationService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("Donation", donation);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            await Task.Delay(2000);
        }
    }

    private async Task Update()
    {
        long id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        }

        (foods, shelters, restaurants) = await donationService.GetAllModels();
        if (!restaurants.Any())
        {
            AnsiConsole.MarkupLine($"[red]First, There must be restaurants[/]");
            goto key;
        }

        if (!foods.Any())
        {
            AnsiConsole.MarkupLine($"[red]First, There must be foods[/]");
            goto key;
        }

        if (!shelters.Any())
        {
            AnsiConsole.MarkupLine($"[red]First, There must be shelters[/]");
            goto key;
        }

        var foodNames = foods.GetNames();
        var shelterNames = shelters.GetNames();
        var restaurantNames = restaurants.GetNames();

        var selectionDisplay = new SelectionMenu();
        RestaurantViewModel restaurant;
        var selection1 = selectionDisplay.ShowSelectionMenu("Choose one of restaurants", restaurantNames.ToArray());

        restaurant = restaurants.GetByName(selection1);

        FoodViewModel food;
        var selection2 = selectionDisplay.ShowSelectionMenu("Choose one of foods", foodNames.ToArray());

        food = foods.GetByName(selection2);

        ShelterViewModel shelter;
        var selection3 = selectionDisplay.ShowSelectionMenu("Choose one of foods", shelterNames.ToArray());

        shelter = shelters.GetByName(selection3);

        var donation = new DonationUpdateModel()
        {
            FoodId = food.Id,
            ShelterId = shelter.Id,
            Quantity = food.Quantity,
            RestaurantId = restaurant.Id,
        };

        try
        {
            var updateDonation = await donationService.UpdateAsync(id, donation);
            AnsiConsole.MarkupLine("[green]Successfully updated...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        key:
        await Task.Delay(2000);
    }

    private async Task Delete()
    {
        long id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        }

        try
        {
            bool isDeleted = await donationService.DeleteAsync(id);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        await Task.Delay(2000);
    }

    private async Task GetAll()
    {
        var donations = await donationService.GetAllAsync();
        var table = new SelectionMenu().DataTable("Donations", donations.ToArray());
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
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
