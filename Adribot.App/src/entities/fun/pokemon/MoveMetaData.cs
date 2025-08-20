using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record MoveMetaData(
    // The status ailment this move inflicts on its target.
    [property: JsonPropertyName("ailment")] NamedApiResource Ailment,

    // The category of move this move falls under, e.g. damage or ailment.
    [property: JsonPropertyName("category")] NamedApiResource Category,

    // The minimum number of times this move hits. Null if it always only hits once.
    [property: JsonPropertyName("min_hits")] int MinHits,

    // The maximum number of times this move hits. Null if it always only hits once.
    [property: JsonPropertyName("max_hits")] int MaxHits,

    // The minimum number of turns this move continues to take effect. Null if it always only lasts one turn.
    [property: JsonPropertyName("min_turns")] int MinTurns,

    // The maximum number of turns this move continues to take effect. Null if it always only lasts one turn.
    [property: JsonPropertyName("max_turns")] int MaxTurns,

    // HP drain (if positive) or Recoil damage (if negative), in percent of damage done.
    [property: JsonPropertyName("drain")] int Drain,

    // The amount of hp gained by the attacking Pokemon, in percent of it's maximum HP.
    [property: JsonPropertyName("healing")] int Healing,

    // Critical hit rate bonus.
    [property: JsonPropertyName("crit_rate")] int CritRate,
    [property: JsonPropertyName("ailment_chance")] int AilmentChance,
    [property: JsonPropertyName("flinch_chance")] int FlinchChance,

    // The likelihood this attack will cause a stat change in the target Pokémon.
    [property: JsonPropertyName("stat_chance")] int StatChance
);
