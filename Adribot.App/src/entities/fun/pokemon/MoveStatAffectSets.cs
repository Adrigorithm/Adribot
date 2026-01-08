using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class MoveStatAffectSets
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // A list of moves and how they change the referenced stat.
    [JsonPropertyName("increase")]
    public List<MoveStatAffect> Increase { get; set; }

    // A list of moves and how they change the referenced stat.
    [JsonPropertyName("decrease")]
    public List<MoveStatAffect> Decrease { get; set; }
}
