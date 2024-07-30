using System.Text.Json.Serialization;
using Adribot.src.parsers.converts;
using Discord;

namespace Adribot.src.config;

public record ConfigValueType
{
    [JsonPropertyName("botToken")]
    public string BotToken { get; init; }

    [JsonPropertyName("catToken")]
    public string CatToken { get; init; }

    [JsonPropertyName("sqlConnectionString")]
    public string SqlConnectionString { get; init; }

    [JsonPropertyName("embedColour")]
    [JsonConverter(typeof(StringColourConverter))]
    public Color EmbedColour { get; init; }

    [JsonPropertyName("devUserId")]
    public ulong DevUserId { get; init; }
}
