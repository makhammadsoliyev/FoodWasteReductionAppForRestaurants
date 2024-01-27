using FoodWasteReductionAppForRestaurants.Configurations;
using FoodWasteReductionAppForRestaurants.Extensions;
using FoodWasteReductionAppForRestaurants.Helpers;
using FoodWasteReductionAppForRestaurants.Interfaces;
using FoodWasteReductionAppForRestaurants.Models.Shelters;

namespace FoodWasteReductionAppForRestaurants.Services;

public class ShelterService : IShelterService
{
    private List<Shelter> shelters;

    public async Task<ShelterViewModel> AddAsync(ShelterCreationModel model)
    {
        shelters = await FileIO.ReadAsync<Shelter>(Constants.SHELTERS_PATH);
        var shelter = model.ToMapMain();
        shelter.Id = CollectionExtension.GenerateId(shelters);

        shelters.Add(shelter);

        await FileIO.WriteAsync(Constants.SHELTERS_PATH, shelters);

        return shelter.ToMapView();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        shelters = await FileIO.ReadAsync<Shelter>(Constants.SHELTERS_PATH);
        var model = shelters.FirstOrDefault(s => s.Id == id && !s.IsDeleted)
            ?? throw new Exception($"Shelter was not found with this id={id}");

        model.IsDeleted = true;
        model.DelatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.SHELTERS_PATH, shelters);

        return true;
    }

    public async Task<IEnumerable<ShelterViewModel>> GetAllAsync()
    {
        shelters = await FileIO.ReadAsync<Shelter>(Constants.SHELTERS_PATH);
        var availableShelters = shelters.FindAll(s => !s.IsDeleted);

        return availableShelters.ToMap();
    }

    public async Task<ShelterViewModel> GetByIdAsync(long id)
    {
        shelters = await FileIO.ReadAsync<Shelter>(Constants.SHELTERS_PATH);

        var shelter = shelters.FirstOrDefault(s => s.Id == id && !s.IsDeleted)
            ?? throw new Exception($"Shelter was not found with this id={id}");

        return shelter.ToMapView();
    }

    public async Task<ShelterViewModel> UpdateAsync(long id, ShelterUpdateModel model)
    {
        shelters = await FileIO.ReadAsync<Shelter>(Constants.SHELTERS_PATH);

        var shelter = shelters.FirstOrDefault(s => s.Id == id && !s.IsDeleted)
            ?? throw new Exception($"Shelter was not found with this id={id}");

        id = shelter.Id;
        shelter.Name = model.Name;
        shelter.Location = model.Location;
        shelter.UpdatedAt = DateTime.UtcNow;
        shelter.Description = model.Description;
        shelter.NumberOfPeople = model.NumberOfPeople;

        await FileIO.WriteAsync(Constants.SHELTERS_PATH, shelters);

        return shelter.ToMapView();
    }
}
