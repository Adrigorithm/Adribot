using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class MoveFlavourText
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("flavor_text")]
    public string FlavourText { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }

    // The version group that uses this flavor text.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
