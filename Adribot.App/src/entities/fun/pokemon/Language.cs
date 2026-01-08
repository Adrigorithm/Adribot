using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Language
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // Whether or not the games are published in this language.
    [JsonPropertyName("official")]
    public bool Official { get; set; }

    // The two-letter code of the country where this language is spoken. Note that it is not unique.
    [JsonPropertyName("iso639")]
    public string Iso639 { get; set; }

    // The two-letter code of the language. Note that it is not unique.
    [JsonPropertyName("iso3166")]
    public string Iso3166 { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }
}
