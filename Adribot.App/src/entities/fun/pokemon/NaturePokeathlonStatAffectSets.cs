using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record NaturePokeathlonStatAffectSets(
    // A list of natures and how they change the referenced Pokéathlon stat.
    [property: JsonPropertyName("increase")] List<NaturePokeathlonStatAffect> Increase,
    
    // A list of natures and how they change the referenced Pokéathlon stat.
    [property: JsonPropertyName("decrease")] List<NaturePokeathlonStatAffect> Decrease
);
