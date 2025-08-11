using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record BerryFlavourMap(
    // How powerful the referenced flavor is for this berry.
    [property: JsonPropertyName("potency")] int Potency,
    
    [property: JsonPropertyName("flavor")] NamedApiResource Flavour
);
