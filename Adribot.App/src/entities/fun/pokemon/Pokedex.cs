using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Pokedex(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    
    // Whether this Pokédex originated in the main series of the video games.
    [property: JsonPropertyName("is_main_series")] bool IsMainSeries,
    
    // The description of this resource listed in different languages.
    [property: JsonPropertyName("descriptions")] List<Description> Descriptions,
    
    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,
    
    [property: JsonPropertyName("pokemon_entries")] List<PokemonEntry> PokemonEntries,
    [property: JsonPropertyName("region")] NamedApiResource Region,
    
    // A list of version groups this Pokédex is relevant to.
    [property: JsonPropertyName("version_groups")] List<NamedApiResource> VersionGroups
);
