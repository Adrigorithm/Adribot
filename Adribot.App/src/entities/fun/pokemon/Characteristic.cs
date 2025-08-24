using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Characteristic(
    [property: JsonPropertyName("id")] int Id,

    // The remainder of the highest stat/IV divided by 5.
    [property: JsonPropertyName("gene_modulo")] int GeneModulo,

    // The possible values of the highest stat that would result in a Pokémon recieving this characteristic when divided by 5.
    [property: JsonPropertyName("possible_values")] List<int> PossibleValues,

    // The stat which results in this characteristic.
    [property: JsonPropertyName("highest_stat")] NamedApiResource HighestStat,

    // The descriptions of this characteristic listed in different languages.
    [property: JsonPropertyName("descriptions")] List<Description> Descriptions
);
