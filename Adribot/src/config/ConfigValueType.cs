using System.Text.Json.Serialization;

namespace Adribot.config
{
    public record ConfigValueType
    {
        [JsonPropertyName("botToken")]
        public string BotToken { get; init; }

        [JsonPropertyName("catToken")]
        public string CatToken { get; init; }

        [JsonPropertyName("sqlConnectionString")]
        public string SqlConnectionString { get; init; }
    }
}