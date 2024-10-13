using System.Text.Json.Serialization;
using Adribot.Parsers.Converters;
using Discord;

namespace Adribot.Config;

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

    public override string ToString()
        => $"Secrets:\nBot Token: {BotToken}\nCat Token: {CatToken}\nSQL Connection String: {SqlConnectionString}\nEmbed Colour: {EmbedColour}\nDev User Id: {DevUserId}";
}
