using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class MoveBattleStylePreference
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("low_hp_preference")]
    public int LowHpPreference { get; set; }

    [JsonPropertyName("high_hp_preference")]
    public int HighHpPreference { get; set; }

    // The move battle style.
    [JsonPropertyName("move_battle_style")]
    public NamedApiResource MoveBattleStyle { get; set; }
}
