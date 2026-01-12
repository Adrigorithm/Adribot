using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class Pokedex
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // Whether this Pokédex originated in the main series of the video games.
    [JsonPropertyName("is_main_series")]
    public bool IsMainSeries { get; set; }

    // The description of this resource listed in different languages.
    [JsonPropertyName("descriptions")]
    public List<Description> Descriptions { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    [JsonPropertyName("pokemon_entries")]
    public List<PokemonEntry> PokemonEntries { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("region")]
    public NamedApiResource Region { get; set; }

    // A list of version groups this Pokédex is relevant to.
    [JsonPropertyName("version_groups")]
    public List<NamedApiResource> VersionGroups { get; set; }
}
