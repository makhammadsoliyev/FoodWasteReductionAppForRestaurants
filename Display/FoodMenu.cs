using FoodWasteReductionAppForRestaurants.Interfaces;
using FoodWasteReductionAppForRestaurants.Models.Foods;
using Spectre.Console;

namespace FoodWasteReductionAppForRestaurants.Display;

public class FoodMenu
{
    private readonly IFoodService foodService;

    public FoodMenu(IFoodService foodService)
    {
        this.foodService = foodService;
    }

    private async Task Add()
    {
        string name = AnsiConsole.Ask<string>("[blue]Name: [/]").Trim();
        string description = AnsiConsole.Ask<string>("[cyan1]Description: [/]").Trim();

        var food = new FoodCreationModel()
        {
            Name = name,
            Description = description,
        };

        var addedFood = await foodService.AddAsync(food);
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
            var food = await foodService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("Food", food);
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

        var food = new FoodUpdateModel()
        {
            Name = name,
            Description = description,
        };

        try
        {
            var updateFood = await foodService.UpdateAsync(id, food);
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
            bool isDeleted = await foodService.DeleteAsync(id);
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
        var foods = await foodService.GetAllAsync();
        var table = new SelectionMenu().DataTable("Foods", foods.ToArray());
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
