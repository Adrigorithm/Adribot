using Adribot.src.entities.discord;
using Discord.WebSocket;

namespace Adribot.src.extensions;

public static class DiscordObjectExtensions
{
    /// <summary>
    /// Calling this method method individually will NOT reference to a DMembers in any way.
    /// </summary>
    /// <returns>The simplified DTO (DGuild) representation of a DiscordGuild</returns>
    public static DGuild ToDGuild(this SocketGuild guild) =>
        new()
        {
            GuildId = guild.Id,
        };

    /// <summary>
    /// Calling this method method individually will NOT reference to a DGuild in any way.
    /// </summary>
    /// <param name="member">The member to be converted</param>
    /// <returns>The simplified DTO (DMember) representation of a DiscordMember</returns>
    public static DMember ToDMember(this SocketGuildUser member, int dGuildId) =>
        new()
        {
            MemberId = member.Id,
            DGuildId = dGuildId,
            Mention = member.Mention
        };
}
