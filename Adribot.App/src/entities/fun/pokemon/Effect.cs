using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class Effect
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("effect")]
    public string LocalisedEffect { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
