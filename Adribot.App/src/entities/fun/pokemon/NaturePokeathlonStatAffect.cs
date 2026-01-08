using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class NaturePokeathlonStatAffect
{
    // The maximum amount of change to the referenced Pokéathlon stat.
    [JsonPropertyName("max_change")]
    public int MaxChange { get; set; }

    // The nature causing the change.
    [JsonPropertyName("nature")]
    public NamedApiResource Nature { get; set; }
}
