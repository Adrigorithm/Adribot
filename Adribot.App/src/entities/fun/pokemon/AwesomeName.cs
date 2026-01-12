using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class AwesomeName
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("awesome_name")]
    public string LocalisedAwesomeName { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
