using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class VersionGroup
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // Order for sorting. Almost by date of release, except similar versions are grouped together.
    [JsonPropertyName("order")]
    public int Order { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("generation")]
    public NamedApiResource Generation { get; set; }

    // A list of methods in which Pokémon can learn moves in this version group.
    [JsonPropertyName("move_learn_methods")]
    public List<NamedApiResource> MoveLearnMethods { get; set; }

    [JsonPropertyName("pokedexes")]
    public List<NamedApiResource> Pokedexes { get; set; }

    // A list of regions that can be visited in this version group.
    [JsonPropertyName("regions")]
    public List<NamedApiResource> Regions { get; set; }

    [JsonPropertyName("versions")]
    public List<NamedApiResource> Versions { get; set; }
}
