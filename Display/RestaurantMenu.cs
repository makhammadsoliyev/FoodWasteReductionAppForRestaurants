using FoodWasteReductionAppForRestaurants.Helpers;
using FoodWasteReductionAppForRestaurants.Interfaces;
using FoodWasteReductionAppForRestaurants.Models.Locations;
using FoodWasteReductionAppForRestaurants.Models.Restaurants;
using Spectre.Console;

namespace FoodWasteReductionAppForRestaurants.Display;

public class RestaurantMenu
{
    private readonly IRestaurantService restaurantService;

    public RestaurantMenu(IRestaurantService restaurantService)
    {
        this.restaurantService = restaurantService;
    }

    private async Task Add()
    {
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]").Trim();
    key:
        string address = AnsiConsole.Ask<string>("[cyan3]Address: [/]").Trim();
        var locations = await GeoLocation.GetLocations(address);
        var names = await GeoLocation.GetLocationsNames(locations);
        names.Add("Change address");

        var selectionDisplay = new SelectionMenu();
        var selection = selectionDisplay.ShowSelectionMenu("Choose one of locations", names.ToArray());
        Location location;

        if (selection.Equals("Change address"))
            goto key;
        else
            location = await GeoLocation.GetLocationByName(locations, selection);

        var restaurant = new RestaurantCreationModel()
        {
            Name = name,
            Location = location,
        };

        var addedRestaurant = await restaurantService.AddAsync(restaurant);
        AnsiConsole.MarkupLine("[green]Successfully added...[/]");
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
            var restaurant = await restaurantService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("Restaurant", restaurant);
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
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]").Trim();
    key:
        string address = AnsiConsole.Ask<string>("[cyan3]Address: [/]").Trim();
        var locations = await GeoLocation.GetLocations(address);
        var names = await GeoLocation.GetLocationsNames(locations);
        names.Add("Change address");

        var selectionDisplay = new SelectionMenu();
        var selection = selectionDisplay.ShowSelectionMenu("Choose one of locations", names.ToArray());
        Location location;

        if (selection.Equals("Change address"))
            goto key;
        else
            location = await GeoLocation.GetLocationByName(locations, selection);

        var restaurant = new RestaurantUpdateModel()
        {
            Name = name,
            Location = location,
        };

        try
        {
            var updateRestaurant = await restaurantService.UpdateAsync(id, restaurant);
            AnsiConsole.MarkupLine("[green]Successfully updated...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
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
            bool isDeleted = await restaurantService.DeleteAsync(id);
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
        var restaurants = await restaurantService.GetAllAsync();
        var table = new SelectionMenu().DataTable("Restaurants", restaurants.ToArray());
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
