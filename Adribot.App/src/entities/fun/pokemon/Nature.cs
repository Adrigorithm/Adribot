using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Nature
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("decreased_stat")]
    public NamedApiResource DecreasedStat { get; set; }

    [JsonPropertyName("increased_stat")]
    public NamedApiResource IncreasedStat { get; set; }

    [JsonPropertyName("hates_flavor")]
    public NamedApiResource HatesFlavour { get; set; }

    [JsonPropertyName("likes_flavor")]
    public NamedApiResource LikesFlavour { get; set; }

    // A list of Pokéathlon stats this nature effects and how much it effects them
    [JsonPropertyName("pokeathlon_stat_changes")]
    public List<NatureStatChange> PokeathlonStatChanges { get; set; }

    // A list of battle styles and how likely a Pokémon with this nature is to use them in the Battle Palace or Battle Tent.
    [JsonPropertyName("move_battle_style_preferences")]
    public List<MoveBattleStylePreference> MoveBattleStylePreferences { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }
}
