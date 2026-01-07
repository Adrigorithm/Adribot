using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Berry
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("growth_time")]
    public int GrowthTime { get; set; }

    // The maximum number of these berries that can grow on one tree in Generation IV.
    [JsonPropertyName("max_harvest")]
    public int MaxHarvest { get; set; }

    // The power of the move "Natural Gift" when used with this Berry.
    [JsonPropertyName("natural_gift_power")]
    public int NaturalGiftPower { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("smoothness")]
    public int Smoothness { get; set; }

    // The speed at which this Berry dries out the soil as it grows. A higher rate means the soil dries more quickly.
    [JsonPropertyName("soil_dryness")]
    public int SoilDryness { get; set; }

    [JsonPropertyName("firmness")]
    public NamedApiResource Firmness { get; set; }

    [JsonPropertyName("flavours")]
    public List<BerryFlavourMap> Flavours { get; set; }

    // Berries are actually items. This is a reference to the item specific data for this berry.
    [JsonPropertyName("item")]
    public NamedApiResource Item { get; set; }

    // The type inherited by "Natural Gift" when used with this Berry.
    [JsonPropertyName("natural_gift_type")]
    public NamedApiResource NaturalGiftType { get; set; }
}
