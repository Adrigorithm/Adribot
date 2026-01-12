using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class Genus
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("genus")]
    public string LocalisedGenus { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("language")]
    public NamedApiResource Language { get; set; }
}
