using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class AbilityFlavourText
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The localized name for an API resource in a specific language.
    [JsonPropertyName("flavor_text")]
    public string FlavourText { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
