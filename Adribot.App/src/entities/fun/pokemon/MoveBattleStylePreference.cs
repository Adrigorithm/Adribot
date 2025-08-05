using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record MoveBattleStylePreference(
    [property: JsonPropertyName("low_hp_preference")] int LowHpPreference,
    [property: JsonPropertyName("high_hp_preference")] int HighHpPreference,
    
    // The move battle style.
    [property: JsonPropertyName("move_battle_style")] NamedApiResource MoveBattleStyle
);
