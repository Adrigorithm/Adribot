using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Stat
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // ID the games use for this stat.
    [JsonPropertyName("game_index")]
    public int GameIndex { get; set; }

    // Whether this stat only exists within a battle.
    [JsonPropertyName("is_battle_only")]
    public bool IsBattleOnly { get; set; }

    // A detail of moves which affect this stat positively or negatively.
    [JsonPropertyName("affecting_moves")]
    public MoveStatAffectSets AffectingMoves { get; set; }

    // A detail of natures which affect this stat positively or negatively.
    [JsonPropertyName("affecting_natures")]
    public NatureStatAffectSets AffectingNatures { get; set; }

    // A list of characteristics that are set on a Pokémon when its highest base stat is this stat.
    [JsonPropertyName("characteristics")]
    public List<ApiResource> Characteristics { get; set; }

    // The class of damage this stat is directly related to.
    [JsonPropertyName("move_damage_class")]
    public NamedApiResource MoveDamageClass { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }
}
