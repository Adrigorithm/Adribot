using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Language(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // Whether or not the games are published in this language.
    [property: JsonPropertyName("official")] bool Official,

    // The two-letter code of the country where this language is spoken. Note that it is not unique.
    [property: JsonPropertyName("iso639")] string Iso639,

    // The two-letter code of the language. Note that it is not unique.
    [property: JsonPropertyName("iso3166")] string Iso3166,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names
);
