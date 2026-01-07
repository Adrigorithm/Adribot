using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ContestComboSets
{
    // A detail of moves this move can be used before or after, granting additional appeal points in contests.
    [JsonPropertyName("normal")]
    public ContestComboDetail Normal { get; set; }

    // A detail of moves this move can be used before or after, granting additional appeal points in super contests.
    [JsonPropertyName("super")]
    public ContestComboDetail Super { get; set; }
}
