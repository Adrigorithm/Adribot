using System.Text.Json.Serialization;

namespace Adribot.src.entities.fun.cat;

public record Breed(
    [property: JsonPropertyName("weight")] Weight Weight,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("cfa_url")] string CfaUrl,
    [property: JsonPropertyName("vetstreet_url")] string VetstreetUrl,
    [property: JsonPropertyName("vcahospitals_url")] string VcahospitalsUrl,
    [property: JsonPropertyName("temperament")] string Temperament,
    [property: JsonPropertyName("origin")] string Origin,
    [property: JsonPropertyName("country_codes")] string CountryCodes,
    [property: JsonPropertyName("country_code")] string CountryCode,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("life_span")] string LifeSpan,
    [property: JsonPropertyName("indoor")] int? Indoor,
    [property: JsonPropertyName("alt_names")] string AltNames,
    [property: JsonPropertyName("adaptability")] int? Adaptability,
    [property: JsonPropertyName("affection_level")] int? AffectionLevel,
    [property: JsonPropertyName("child_friendly")] int? ChildFriendly,
    [property: JsonPropertyName("dog_friendly")] int? DogFriendly,
    [property: JsonPropertyName("energy_level")] int? EnergyLevel,
    [property: JsonPropertyName("grooming")] int? Grooming,
    [property: JsonPropertyName("health_issues")] int? HealthIssues,
    [property: JsonPropertyName("intelligence")] int? Intelligence,
    [property: JsonPropertyName("shedding_level")] int? SheddingLevel,
    [property: JsonPropertyName("social_needs")] int? SocialNeeds,
    [property: JsonPropertyName("stranger_friendly")] int? StrangerFriendly,
    [property: JsonPropertyName("vocalisation")] int? Vocalisation,
    [property: JsonPropertyName("experimental")] int? Experimental,
    [property: JsonPropertyName("hairless")] int? Hairless,
    [property: JsonPropertyName("natural")] int? Natural,
    [property: JsonPropertyName("rare")] int? Rare,
    [property: JsonPropertyName("rex")] int? Rex,
    [property: JsonPropertyName("suppressed_tail")] int? SuppressedTail,
    [property: JsonPropertyName("short_legs")] int? ShortLegs,
    [property: JsonPropertyName("wikipedia_url")] string WikipediaUrl,
    [property: JsonPropertyName("hypoallergenic")] int? Hypoallergenic,
    [property: JsonPropertyName("reference_image_id")] string ReferenceImageId
);