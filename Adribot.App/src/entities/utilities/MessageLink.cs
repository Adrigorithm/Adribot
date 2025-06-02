using Adribot.Data;
using Discord;

namespace Adribot.Entities.Utilities;

public class MessageLink : IDataStructure
{
    public int MessageLinkId { get; set; }

    public ulong originalMessageId { get; set; }
    public ulong referenceMessageId { get; set; }

    public int StarboardId { get; set; }
    public Starboard Starboard { get; set; }

    public EmbedBuilder GenerateEmbedBuilder() =>
        new()
        {
            Author = new EmbedAuthorBuilder { Name = "Adrigorithm" },
            Title = "",
            Description = $"Message with id `{originalMessageId}` is referenced by Message with id `{referenceMessageId}`"
        };
}
