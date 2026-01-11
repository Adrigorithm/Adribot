using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class VersionGroupFlavourText
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }

    // The version group which uses this flavour text.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
