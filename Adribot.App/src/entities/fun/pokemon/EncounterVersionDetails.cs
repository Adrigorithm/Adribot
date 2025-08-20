using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record EncounterVersionDetails(
    // The chance of an encounter to occur.
    [property: JsonPropertyName("rate")] int Rate,

    // The version of the game in which the encounter can occur with the given chance.
    [property: JsonPropertyName("version")] NamedApiResource Version
);
