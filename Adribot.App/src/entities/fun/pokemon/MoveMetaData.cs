using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class MoveMetaData
{
    // The status ailment this move inflicts on its target.
    [JsonPropertyName("ailment")]
    public NamedApiResource Ailment { get; set; }

    // The category of move this move falls under, e.g. damage or ailment.
    [JsonPropertyName("category")]
    public NamedApiResource Category { get; set; }

    // The minimum number of times this move hits. Null if it always only hits once.
    [JsonPropertyName("min_hits")]
    public int MinHits { get; set; }

    // The maximum number of times this move hits. Null if it always only hits once.
    [JsonPropertyName("max_hits")]
    public int MaxHits { get; set; }

    // The minimum number of turns this move continues to take effect. Null if it always only lasts one turn.
    [JsonPropertyName("min_turns")]
    public int MinTurns { get; set; }

    // The maximum number of turns this move continues to take effect. Null if it always only lasts one turn.
    [JsonPropertyName("max_turns")]
    public int MaxTurns { get; set; }

    // HP drain (if positive) or Recoil damage (if negative), in percent of damage done.
    [JsonPropertyName("drain")]
    public int Drain { get; set; }

    // The amount of hp gained by the attacking Pokemon, in percent of it's maximum HP.
    [JsonPropertyName("healing")]
    public int Healing { get; set; }

    // Critical hit rate bonus.
    [JsonPropertyName("crit_rate")]
    public int CritRate { get; set; }

    [JsonPropertyName("ailment_chance")]
    public int AilmentChance { get; set; }

    [JsonPropertyName("flinch_chance")]
    public int FlinchChance { get; set; }

    // The likelihood this attack will cause a stat change in the target Pokémon.
    [JsonPropertyName("stat_chance")]
    public int StatChance { get; set; }
}
