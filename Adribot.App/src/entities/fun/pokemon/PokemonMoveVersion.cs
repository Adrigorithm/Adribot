using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonMoveVersion
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("move_learn_method")]
    public NamedApiResource MoveLearnMethod { get; set; }

    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }

    [JsonPropertyName("level_learned_at")]
    public int LevelLearnedAt { get; set; }

    // Order by which the pokemon will learn the move. A newly learnt move will replace the move with lowest order.
    [JsonPropertyName("order")]
    public int Order { get; set; }
}
