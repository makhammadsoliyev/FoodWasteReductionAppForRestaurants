using FoodWasteReductionAppForRestaurants.Models.Commons;

namespace FoodWasteReductionAppForRestaurants.Extensions;

public static class CollectionExtension
{
    public static long GenerateId<T>(this List<T> values) where T : Auditable
        => values.Count == 0 ? 1 : values.Last().Id + 1;
}
