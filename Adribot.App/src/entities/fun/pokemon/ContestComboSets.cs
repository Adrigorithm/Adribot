using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record ContestComboSets(
    // A detail of moves this move can be used before or after, granting additional appeal points in contests.
    [property: JsonPropertyName("normal")] ContestComboDetail Normal,
    
    // A detail of moves this move can be used before or after, granting additional appeal points in super contests.
    [property: JsonPropertyName("super")] ContestComboDetail Super
);
