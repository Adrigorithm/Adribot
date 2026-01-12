using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class PokemonHeldItem
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("item")]
    public NamedApiResource Item { get; set; }

    [JsonPropertyName("version_details")]
    public List<PokemonHeldItemVersion> VersionDetails { get; set; }
}
