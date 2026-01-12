using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class Characteristic
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The remainder of the highest stat/IV divided by 5.
    [JsonPropertyName("gene_modulo")]
    public int GeneModulo { get; set; }

    // The possible values of the highest stat that would result in a Pokémon receiving this characteristic when divided by 5.
    [JsonPropertyName("possible_values")]
    public List<int> PossibleValues { get; set; }

    // The stat which results in this characteristic.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("highest_stat")]
    public NamedApiResource HighestStat { get; set; }

    // The descriptions of this characteristic listed in different languages.
    [JsonPropertyName("descriptions")]
    public List<Description> Descriptions { get; set; }
}
