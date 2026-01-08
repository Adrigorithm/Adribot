using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class EncounterVersionDetails
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The chance of an encounter to occur.
    [JsonPropertyName("rate")]
    public int Rate { get; set; }

    // The version of the game in which the encounter can occur with the given chance.
    [JsonPropertyName("version")]
    public NamedApiResource Version { get; set; }
}
