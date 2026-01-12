using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class VerboseEffect
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("effect")]
    public string Effect { get; set; }

    [JsonPropertyName("short_effect")]
    public string ShortEffect { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
