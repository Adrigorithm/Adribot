using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class EvolutionDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("item")]
    public NamedApiResource Item { get; set; }

    [JsonPropertyName("trigger")]
    public NamedApiResource Trigger { get; set; }

    [JsonPropertyName("gender")]
    public int Gender { get; set; }

    [JsonPropertyName("held_item")]
    public NamedApiResource HeldItem { get; set; }

    // The move that must be known by the evolving Pokémon species during the evolution trigger event in order to evolve into this Pokémon species.
    [JsonPropertyName("known_move")]
    public NamedApiResource KnownMove { get; set; }

    // The evolving Pokémon species must know a move with this type during the evolution trigger event in order to evolve into this Pokémon species.
    [JsonPropertyName("known_move_type")]
    public NamedApiResource KnownMoveType { get; set; }

    [JsonPropertyName("location")]
    public NamedApiResource Location { get; set; }

    [JsonPropertyName("min_level")]
    public int MinLevel { get; set; }

    [JsonPropertyName("min_happiness")]
    public int MinHappiness { get; set; }

    [JsonPropertyName("min_beauty")]
    public int MinBeauty { get; set; }

    [JsonPropertyName("min_affection")]
    public int MinAffection { get; set; }

    [JsonPropertyName("needs_overworld_rain")]
    public bool NeedsOverworldRain { get; set; }

    // The Pokémon species that must be in the player's party in order for the evolving Pokémon species to evolve into this Pokémon species.
    [JsonPropertyName("party_species")]
    public NamedApiResource PartySpecies { get; set; }

    // The player must have a Pokémon of this type in their party during the evolution trigger event in order for the evolving Pokémon species to evolve into this Pokémon species.
    [JsonPropertyName("party_type")]
    public NamedApiResource PartyType { get; set; }

    // The required relation between the Pokémon's Attack and Defense stats.
    // 1 means Attack > Defense. 0 means Attack = Defense. -1 means Attack < Defense.
    [JsonPropertyName("relative_physical_stats")]
    public int RelativePhysicalStats { get; set; }

    [JsonPropertyName("time_of_day")]
    public string TimeOfDay { get; set; }

    // Pokémon species for which this one must be traded.
    [JsonPropertyName("trade_species")]
    public NamedApiResource TradeSpecies { get; set; }

    // Whether the 3DS needs to be turned upside-down as this Pokémon levels up.
    [JsonPropertyName("turn_upside_down")]
    public bool TurnUpsideDown { get; set; }
}
