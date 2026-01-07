using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Generation
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("abilities")]
    public List<NamedApiResource> Abilities { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // The main region travelled in this generation.
    [JsonPropertyName("main_region")]
    public NamedApiResource MainRegion { get; set; }

    [JsonPropertyName("moves")]
    public List<NamedApiResource> Moves { get; set; }

    [JsonPropertyName("pokemon_species")]
    public List<NamedApiResource> PokemonSpecies { get; set; }

    [JsonPropertyName("types")]
    public List<NamedApiResource> Types { get; set; }

    // A list of version groups that were introduced in this generation.
    [JsonPropertyName("version_groups")]
    public List<NamedApiResource> VersionGroups { get; set; }
}
