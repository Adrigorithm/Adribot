using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Berry(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("growth_time")] int GrowthTime,

    // The maximum number of these berries that can grow on one tree in Generation IV.
    [property: JsonPropertyName("max_harvest")] int MaxHarvest,

    // The power of the move "Natural Gift" when used with this Berry.
    [property: JsonPropertyName("natural_gift_power")] int NaturalGiftPower,
    [property: JsonPropertyName("size")] int Size,
    [property: JsonPropertyName("smoothness")] int Smoothness,

    // The speed at which this Berry dries out the soil as it grows. A higher rate means the soil dries more quickly.
    [property: JsonPropertyName("soil_dryness")] int SoilDryness,
    [property: JsonPropertyName("firmness")] NamedApiResource Firmness,
    [property: JsonPropertyName("flavours")] List<BerryFlavourMap> Flavours,

    // Berries are actually items. This is a reference to the item specific data for this berry.
    [property: JsonPropertyName("item")] NamedApiResource Item,

    // The type inherited by "Natural Gift" when used with this Berry.
    [property: JsonPropertyName("natural_gift_type")] NamedApiResource NaturalGiftType
);
