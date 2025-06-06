using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using Adribot.Data;
using Adribot.Entities.Discord;
using Discord;

namespace Adribot.Entities.Utilities;

public class Starboard : IDataStructure
{
    public int StarboardId { get; set; }

    public int DGuildId { get; set; }
    public DGuild DGuild { get; set; }

    public ulong ChannelId { get; set; }
    public int Threshold { get; set; }
    public List<string> EmojiStrings { get; set; } = [];
    public List<string> EmoteStrings { get; set; } = [];

    public List<MessageLink> MessageLinks { get; set; } = [];

    public EmbedBuilder GenerateEmbedBuilder()
    {
        var description = DGuild is null
            ? $"Guild with database id `{DGuildId}` has a starboard config for `{EmojiStrings.Count}` emojis and `{EmoteStrings.Count}` emotes triggered when reaction count `{Threshold}` is reached."
            : $"Guild with id `{DGuild.GuildId}` has a starboard config for `{EmojiStrings.Count}` emojis and `{EmoteStrings.Count}` emotes triggered when reaction count `{Threshold}` is reached.";

        return new()
        {
            Author = new EmbedAuthorBuilder { Name = "Adrigorithm" },
            Title = "Starboard",
            Description = description
        };
    }
}
