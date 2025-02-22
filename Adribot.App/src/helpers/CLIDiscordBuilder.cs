using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord.WebSocket;

namespace Adribot.Helpers;

// ReSharper disable once InconsistentNaming
public static class CLIDiscordBuilder
{
    public static string DiscordMessage(string channelName, string userName, string message) =>
        $"In #{channelName}, {userName} wrote:\n{message}";

    public static string DiscordChannels(ulong guildId, IEnumerable<SocketGuildChannel> channels)
    {
        var sb = new StringBuilder($"Channels in guild `{guildId}`:");

        channels.ToList().ForEach(c => sb.AppendLine($"#{c.Name}: {c.Id}"));

        return sb.ToString();
    }

    public static string DiscordMembers(ulong guildId, IEnumerable<SocketGuildUser> members)
    {
        var sb = new StringBuilder($"Members in guild `{guildId}`:");

        members.ToList().ForEach(m => sb.AppendLine($"{m.GlobalName} ({m.Mention}): {m.Id}"));

        return sb.ToString();
    }
}
