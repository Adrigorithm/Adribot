using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonMoveVersion(
    [property: JsonPropertyName("move_learn_method")] NamedApiResource MoveLearnMethod,
    [property: JsonPropertyName("version_group")] NamedApiResource VersionGroup,
    [property: JsonPropertyName("level_learned_at")] int LevelLearnedAt,

    // Order by which the pokemon will learn the move. A newly learnt move will replace the move with lowest order.
    [property: JsonPropertyName("order")] int Order
);
