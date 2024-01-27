using FoodWasteReductionAppForRestaurants.Helpers;
using FoodWasteReductionAppForRestaurants.Interfaces;
using FoodWasteReductionAppForRestaurants.Models.Locations;
using FoodWasteReductionAppForRestaurants.Models.Shelters;
using Spectre.Console;

namespace FoodWasteReductionAppForRestaurants.Display;

public class ShelterMenu
{
    private readonly IShelterService shelterService;

    public ShelterMenu(IShelterService shelterService)
    {
        this.shelterService = shelterService;
    }

    private async Task Add()
    {
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]").Trim();
        string description = AnsiConsole.Ask<string>("[cyan1]Description: [/]").Trim();
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

        int numberOfPeople = AnsiConsole.Ask<int>("[aqua]NumberOfPeople: [/]");
        while (numberOfPeople <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            numberOfPeople = AnsiConsole.Ask<int>("[aqua]NumberOfPeople: [/]");
        }

        var shelter = new ShelterCreationModel()
        {
            Name = name,
            Location = location,
            Description = description,
            NumberOfPeople = numberOfPeople,
        };

        var addedShelter = await shelterService.AddAsync(shelter);
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
            var shelter = await shelterService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("Shelter", shelter);
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
        string description = AnsiConsole.Ask<string>("[cyan1]Description: [/]").Trim();
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

        int numberOfPeople = AnsiConsole.Ask<int>("[aqua]NumberOfPeople: [/]");
        while (numberOfPeople <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            numberOfPeople = AnsiConsole.Ask<int>("[aqua]NumberOfPeople: [/]");
        }

        var shelter = new ShelterUpdateModel()
        {
            Name = name,
            Location = location,
            Description = description,
            NumberOfPeople = numberOfPeople,
        };

        try
        {
            var updateShelter = await shelterService.UpdateAsync(id, shelter);
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
            bool isDeleted = await shelterService.DeleteAsync(id);
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
        var shelters = await shelterService.GetAllAsync();
        var table = new SelectionMenu().DataTable("Shelters", shelters.ToArray());
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
