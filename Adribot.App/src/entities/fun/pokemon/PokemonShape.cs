using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonShape
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The "scientific" name of this Pokémon shape listed in different languages.
    [JsonPropertyName("awesome_names")]
    public List<AwesomeName> AwesomeNames { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // A list of the Pokémon species that have this shape.
    [JsonPropertyName("pokemon_species")]
    public List<NamedApiResource> PokemonSpecies { get; set; }
}
