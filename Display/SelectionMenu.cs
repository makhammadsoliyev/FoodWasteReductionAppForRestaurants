using FoodWasteReductionAppForRestaurants.Models.Donations;
using FoodWasteReductionAppForRestaurants.Models.Foods;
using FoodWasteReductionAppForRestaurants.Models.Restaurants;
using FoodWasteReductionAppForRestaurants.Models.Shelters;
using Spectre.Console;

namespace FoodWasteReductionAppForRestaurants.Display;

public class SelectionMenu
{
    public Table DataTable(string title, params DonationViewModel[] donations)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("Restaurant");
        table.AddColumn("Food");
        table.AddColumn("Quantity");
        table.AddColumn("Shelter");

        table.Border = TableBorder.Rounded;
        table.Centered();

        foreach (var donation in donations)
            table.AddRow(donation.Id.ToString(), donation.Restaurant.Name, donation.Food.Name, donation.Quantity.ToString(), donation.Shelter.Name);

        return table;
    }

    public Table DataTable(string title, params RestaurantViewModel[] restaurants)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Address");
        table.AddColumn("Latitude");
        table.AddColumn("Longitude");

        table.Border = TableBorder.Rounded;
        table.Centered();

        foreach (var restaurant in restaurants)
            table.AddRow(restaurant.Id.ToString(), restaurant.Name, restaurant.Address, restaurant.Latitude, restaurant.Longitude);

        return table;
    }

    public Table DataTable(string title, params ShelterViewModel[] shelters)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Description");
        table.AddColumn("NumberOfPeople");
        table.AddColumn("Address");
        table.AddColumn("Latitude");
        table.AddColumn("Longitude");

        table.Border = TableBorder.Rounded;
        table.Centered();

        foreach (var shelter in shelters)
            table.AddRow(shelter.Id.ToString(), shelter.Name, shelter.Description, shelter.NumberOfPeople.ToString(), shelter.Address, shelter.Latitude, shelter.Longitude);

        return table;
    }

    public Table DataTable(string title, params FoodViewModel[] foods)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Description");
        table.AddColumn("Quantity");

        table.Border = TableBorder.Rounded;
        table.Width = 100;
        table.Centered();

        foreach (var food in foods)
            table.AddRow(food.Id.ToString(), food.Name, food.Description, food.Quantity.ToString());

        return table;
    }

    public string ShowSelectionMenu(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(5) // Number of items visible at once
                .AddChoices(options)
                .HighlightStyle(new Style(foreground: Color.Cyan1, background: Color.Blue))
        );

        return selection;
    }
}
