using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PalParkEncounterSpecies(
    // The base score given to the player when this Pokémon is caught during a pal park run.
    [property: JsonPropertyName("base_score")] int BaseScore,

    // The base rate for encountering this Pokémon in this pal park area.
    [property: JsonPropertyName("rate")] int Rate,
    [property: JsonPropertyName("pokemon_species")] NamedApiResource PokemonSpecies
);
