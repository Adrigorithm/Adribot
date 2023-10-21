using System.Text.Json.Serialization;

namespace Adribot.src.config
{
    public record ConfigValueType
    {
        [JsonPropertyName("botToken")]
        public string BotToken { get; init; }

        [JsonPropertyName("catToken")]
        public string CatToken { get; init; }

        [JsonPropertyName("sqlConnectionString")]
        public string SqlConnectionString { get; init; }

        [JsonPropertyName("embedColour")]
        public string EmbedColour { get; init; }

        [JsonPropertyName("devUserId")]
        public ulong DevUserId { get; init; }
    }
}
