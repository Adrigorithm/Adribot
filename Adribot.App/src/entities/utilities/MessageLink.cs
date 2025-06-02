using Adribot.Data;
using Discord;

namespace Adribot.Entities.Utilities;

public class MessageLink : IDataStructure
{
    public int MessageLinkId { get; set; }

    public ulong OriginalMessageId { get; set; }
    public ulong ReferenceMessageId { get; set; }

    public int StarboardId { get; set; }
    public Starboard Starboard { get; set; }

    public EmbedBuilder GenerateEmbedBuilder() =>
        new()
        {
            Author = new EmbedAuthorBuilder { Name = "Adrigorithm" },
            Title = "",
            Description = $"Message with id `{OriginalMessageId}` is referenced by Message with id `{ReferenceMessageId}`"
        };
}
