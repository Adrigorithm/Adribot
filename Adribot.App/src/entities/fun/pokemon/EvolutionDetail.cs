using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record EvolutionDetail(
    [property: JsonPropertyName("item")] NamedApiResource Item,
    [property: JsonPropertyName("trigger")] NamedApiResource Trigger,
    [property: JsonPropertyName("gender")] int Gender,
    [property: JsonPropertyName("held_item")] NamedApiResource HeldItem,

    // The move that must be known by the evolving Pokémon species during the evolution trigger event in order to evolve into this Pokémon species.
    [property: JsonPropertyName("known_move")] NamedApiResource KnownMove,

    // The evolving Pokémon species must know a move with this type during the evolution trigger event in order to evolve into this Pokémon species.
    [property: JsonPropertyName("known_move_type")] NamedApiResource KnownMoveType,

    [property: JsonPropertyName("location")] NamedApiResource Location,
    [property: JsonPropertyName("min_level")] int MinLevel,
    [property: JsonPropertyName("min_happiness")] int MinHappiness,
    [property: JsonPropertyName("min_beauty")] int MinBeauty,
    [property: JsonPropertyName("min_affection")] int MinAffection,
    [property: JsonPropertyName("needs_overworld_rain")] bool NeedsOverworldRain,

    // The Pokémon species that must be in the players party in order for the evolving Pokémon species to evolve into this Pokémon species.
    [property: JsonPropertyName("party_species")] NamedApiResource PartySpecies,

    // The player must have a Pokémon of this type in their party during the evolution trigger event in order for the evolving Pokémon species to evolve into this Pokémon species.
    [property: JsonPropertyName("party_type")] NamedApiResource PartyType,

    // The required relation between the Pokémon's Attack and Defense stats. 1 means Attack > Defense. 0 means Attack = Defense. -1 means Attack < Defense.
    [property: JsonPropertyName("relative_physical_stats")] int RelativePhysicalStats,

    [property: JsonPropertyName("time_of_day")] string TimeOfDay,

    // Pokémon species for which this one must be traded.
    [property: JsonPropertyName("trade_species")] NamedApiResource TradeSpecies,

    // Whether the 3DS needs to be turned upside-down as this Pokémon levels up.
    [property: JsonPropertyName("turn_upside_down")] bool TurnUpsideDown
);
