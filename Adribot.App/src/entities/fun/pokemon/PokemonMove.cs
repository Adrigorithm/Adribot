using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class PokemonMove
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("move")]
    public NamedApiResource Move { get; set; }

    [JsonPropertyName("version_group_details")]
    public List<PokemonMoveVersion> VersionGroupDetails { get; set; }
}
