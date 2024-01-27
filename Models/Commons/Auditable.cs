﻿namespace FoodWasteReductionAppForRestaurants.Models.Commons;

public abstract class Auditable
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
    public DateTime DelatedAt { get; set; }
}