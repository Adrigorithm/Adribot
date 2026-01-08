using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class MoveCategory
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // A list of moves that fall into this category.
    [JsonPropertyName("moves")]
    public List<NamedApiResource> Moves { get; set; }

    // The description of this resource listed in different languages.
    [JsonPropertyName("descriptions")]
    public List<Description> Descriptions { get; set; }
}
