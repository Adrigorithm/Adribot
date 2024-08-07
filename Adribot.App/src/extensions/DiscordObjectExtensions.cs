using System;
using Adribot.src.entities.discord;
using Discord;
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

    public static Color ToDiscordColour(this string colourString, int baseFormat = 16) =>
        string.IsNullOrWhiteSpace(colourString)
            ? throw new ArgumentException($"Cannot parse colour from an empty string")
            : colourString.Length switch
        {
            6 => new Color(Convert.ToUInt32(colourString, baseFormat)),
            8 => new Color(Convert.ToUInt32(colourString[2..], baseFormat)),
            _ => throw new ArgumentException($"Cannot parse colour: {colourString}")
        };
}
