using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ContestComboDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // A list of moves to use before this move.
    [JsonPropertyName("use_before")]
    public List<NamedApiResource> UseBefore { get; set; }

    // A list of moves to use after this move.
    [JsonPropertyName("use_after")]
    public List<NamedApiResource> UseAfter { get; set; }
}
