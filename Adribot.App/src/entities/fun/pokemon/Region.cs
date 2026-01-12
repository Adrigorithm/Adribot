using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class Region
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("locations")]
    public List<NamedApiResource> Locations { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // The generation this region was introduced in.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("main_generation")]
    public NamedApiResource MainGeneration { get; set; }

    // A list of pokédexes that catalogue Pokémon in this region.
    [JsonPropertyName("pokedexes")]
    public List<NamedApiResource> Pokedexes { get; set; }

    // A list of version groups where this region can be visited.
    [JsonPropertyName("version_groups")]
    public List<NamedApiResource> VersionGroups { get; set; }
}
