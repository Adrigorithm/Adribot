using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class NaturePokeathlonStatAffect
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The maximum amount of change to the referenced Pokéathlon stat.
    [JsonPropertyName("max_change")]
    public int MaxChange { get; set; }

    // The nature causing the change.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("nature")]
    public NamedApiResource Nature { get; set; }
}
