using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PalParkEncounterArea
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The base score given to the player when the referenced Pokémon is caught during a pal park run.
    [JsonPropertyName("base_score")]
    public int BaseScore { get; set; }

    // The base rate for encountering the referenced Pokémon in this pal park area.
    [JsonPropertyName("rate")]
    public int Rate { get; set; }

    [JsonPropertyName("area")]
    public NamedApiResource Area { get; set; }
}
