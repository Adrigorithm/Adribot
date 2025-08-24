using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PalParkEncounterArea(
    // The base score given to the player when the referenced Pokémon is caught during a pal park run.
    [property: JsonPropertyName("base_score")] int BaseScore,

    // The base rate for encountering the referenced Pokémon in this pal park area.
    [property: JsonPropertyName("rate")] int Rate,
    [property: JsonPropertyName("area")] NamedApiResource Area
);
