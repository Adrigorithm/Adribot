using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSharpPlus.Entities;

namespace Adribot.src.helpers;

public static class CLIDiscordBuilder
{
    public static string DiscordMessage(string channelName, string userName, string message) =>
        $"In #{channelName}, {userName} wrote:\n{message}";

    public static string DiscordChannels(ulong guildId, IEnumerable<DiscordChannel> channels)
    {
        var sb = new StringBuilder($"Channels in guild `{guildId}`:");

        channels.ToList().ForEach(c => sb.AppendLine($"#{c.Name}: {c.Id}"));

        return sb.ToString();
    }

    public static string DiscordMembers(ulong guildId, IEnumerable<DiscordMember> members)
    {
        var sb = new StringBuilder($"Members in guild `{guildId}`:");

        members.ToList().ForEach(m => sb.AppendLine($"{m.GlobalName} ({m.Mention}): {m.Id}"));

        return sb.ToString();
    }
}
