using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class FlavourBerryMap
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // How powerful the referenced flavour is for this berry.
    [JsonPropertyName("potency")]
    public int Potency { get; set; }

    [JsonPropertyName("berry")]
    public NamedApiResource Berry { get; set; }
}
